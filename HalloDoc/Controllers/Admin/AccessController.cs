using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    [CheckProviderAccess("Admin", "Role")]
    public class AccessController : Controller
    {
        
        #region Constructor
        private readonly IRoleAccessRepository _IRoleAccessRepository;
        private readonly IProviderRepository _IProviderRepository;
        private readonly IMyProfileRepository _IMyProfileRepository;
        private readonly INotyfService _notyf;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly EmailConfiguration _emailConfig;

        public AccessController(ILogger<AdminActionsController> logger,IRoleAccessRepository IRoleAccessRepository,INotyfService notyf, IComboboxRepository combobox, EmailConfiguration emailConfiguration,IProviderRepository IProviderRepository,IMyProfileRepository IMyProfileRepository)
        {
            _IRoleAccessRepository = IRoleAccessRepository;
            _IProviderRepository = IProviderRepository;
            _IMyProfileRepository = IMyProfileRepository;
            _notyf = notyf;
            _logger = logger;
            _combobox = combobox;
            _emailConfig = emailConfiguration;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Role> v = await _IRoleAccessRepository.GetRoleAccessDetails();
            return View("../Admin/Access/Index",v);
        }
        #endregion

        #region User_Access
        public async Task<IActionResult> UserAccess(int? role)
        {
            List<ViewUserAcces> data = await _IRoleAccessRepository.GetAllUserDetails(role);
            if (role != null)
            {
                data = await _IRoleAccessRepository.GetAllUserDetails(role); 
            }
            return View("../Admin/Access/UserAccess", data);
        }
        #endregion

        #region Create_Role_Access-ADDEdit
        public async Task<IActionResult> CreateRoleAccess(int? id)
        {
            if (id != null)
            {
                ViewData["RolesAddEdit"] = "Edit";
                ViewRoleByMenu v = await _IRoleAccessRepository.GetRoleByMenus((int)id);
                return View("../Admin/Access/CreateRoleAccess", v);
            }
            ViewData["RolesAddEdit"] = "Create";

            return View("../Admin/Access/CreateRoleAccess");
        }
        #endregion

        #region GetMenusByAccount
        public async Task<IActionResult> GetMenusByAccount(short Accounttype, int roleid)
        {
            if(Accounttype == 0)
            {
                List<Menu> data = await _IRoleAccessRepository.GetMenusByAccount(1);
                return Json(data);
            }
            List<Menu> v = await _IRoleAccessRepository.GetMenusByAccount(Accounttype);

            if (roleid != null)
            {
                List<ViewRoleByMenu.Menu> vm = new List<ViewRoleByMenu.Menu>();
                List<int> rm = await _IRoleAccessRepository.CheckMenuByRole(roleid);
                foreach (var item in v)
                {
                    ViewRoleByMenu.Menu menu = new ViewRoleByMenu.Menu();
                    menu.Name = item.Name;
                    menu.Menuid = item.Menuid;

                    if (rm.Contains(item.Menuid))
                    {
                        menu.checekd = "checked";
                        vm.Add(menu);
                    }
                    else
                    {
                        vm.Add(menu);
                    }
                }
                return Json(vm);
            }

            return Json(v);
        }
        #endregion

        #region PostRoleMenu
        public async Task<IActionResult> PostRoleMenu(ViewRoleByMenu role, string Menusid)
        {
            if (role.Roleid == 0)
            {
				if (await _IRoleAccessRepository.PostRoleMenu(role, Menusid, CV.ID()))
				{
					_notyf.Success("Role Added Successfully...");
				}
				else
				{
					_notyf.Error("Role not Added Successfully...");
				}
			}
            else
            {
				if (await _IRoleAccessRepository.PutRoleMenu(role, Menusid, CV.ID()))
				{
					_notyf.Success("Role Modified Successfully...");
				}
				else
				{
					_notyf.Error("Role not Modified...");
				}
			}
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteRole
        public async Task<IActionResult> DeleteRole(int roleid)
		{
			bool data = await _IRoleAccessRepository.DeleteRoles(roleid, CV.ID());
			if (data)
			{
				_notyf.Success("Role deleted successfully...");
				return RedirectToAction("Index");
			}
			else
			{
				_notyf.Error("Role not deleted successfully...");
				return RedirectToAction("Index");
			}
		}
        #endregion

        #region AddEdit_Profile
        public async Task<IActionResult> PhysicianAddEdit(int? id)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.PhysicianRoleComboBox();
            if (id == null)
            {
                ViewData["PhysicianAccount"] = "Add";
            }
            else
            {
                ViewData["PhysicianAccount"] = "Edit";
                PhysiciansData v = await _IProviderRepository.GetPhysicianById((int)id);
                return View("../Admin/Providers/AddEditProvider", v);
            }
            return View("../Admin/Providers/AddEditProvider");
        }
        #endregion

        #region AdminAdd

        public async Task<IActionResult> AdminAddEdit(int? id)
        {
            //TempData["Status"] = TempData["Status"];
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.AdminRoleComboBox();
            if (id == null)
            {
                ViewData["AdminAccount"] = "Add Admin";
            }
            return View("../Admin/Access/AdminAddEdit");
        }
        #endregion

        #region AdminEdit
        public async Task<IActionResult> AdminEdit(int? id)
        {
            ViewAdminProfile p = await _IMyProfileRepository.GetProfileDetails((id != null ? (int)id : Convert.ToInt32(CV.UserID())));
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.AdminRoleComboBox();
            return View("../Admin/Profile/Index", p);
        }
        #endregion

        #region Create_Admin
        [HttpPost]
        public async Task<IActionResult> AdminAdd(ViewAdminProfile vm)
        {
            //TempData["Status"] = TempData["Status"];
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.AdminRoleComboBox();
            // bool b = physicians.Isagreementdoc[0];

            /*if (ModelState.IsValid)
            {*/
              if(  await _IMyProfileRepository.AdminPost(vm, CV.ID()))
            {
                _notyf.Success("Admin Added Successfully..!");
            }
            else
            {
                _notyf.Error("You can not add admin with existing email address..!");
                return View("../Admin/Access/AdminAddEdit", vm);
            }
            /*else
            {
                return View("../Admin/Access/AdminAddEdit", vm);
            }*/
            return RedirectToAction("UserAccess");
        }
        #endregion

        #region SaveAdminInfo
        public async Task<IActionResult> SaveAdminInfo(ViewAdminProfile vm)
        {
            bool data = await _IMyProfileRepository.SaveAdminInfo(vm);
            if (data)
            {
				_notyf.Success("Admin Information Changed successfully...");
            }
            else
            {
				_notyf.Error("Admin Information not Changed successfully...");
            }
            return RedirectToAction("AdminEdit", new { id = vm.AdminId });
        }
        #endregion

        #region SaveAdministrationinfo
        public async Task<IActionResult> EditAdministratorInfo(ViewAdminProfile vm)
        {
            bool data = await _IMyProfileRepository.EditAdministratorInfo(vm,CV.ID());
            if (data)
            {
				_notyf.Success("Administration Information Changed successfully...");
            }
            else
            {
				_notyf.Error("Administration Information not Changed successfully...");
            }
            return RedirectToAction("AdminEdit", new { id = vm.AdminId });
        }
        #endregion

        #region EditBillingInfo
        public async Task<IActionResult> BillingInfoEdit(ViewAdminProfile vm)
        {
            bool data = await _IMyProfileRepository.BillingInfoEdit(vm);
            if (data)
            {
                _notyf.Success("Billing Information Changed successfully...");
            }
            else
            {
                _notyf.Error("Billing Information not Changed successfully...");
            }
            return RedirectToAction("AdminEdit", new { id = vm.AdminId });
        }
        #endregion

        #region ResetPassAdmin
        public async Task<IActionResult> EditPassword(string password, int AdminId)
        {
            bool data = await _IMyProfileRepository.EditPassword(password, AdminId);
            if (data)
            {
                _notyf.Success("Password changed Successfully...");
            }
            else
            {
                _notyf.Error("Password not Changed Successfully...");
            }
            return RedirectToAction("AdminEdit", new { id = AdminId });
        }
        #endregion
    }
}