using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class AccessController : Controller
    {
        #region Constructor
        private readonly IRoleAccessRepository _IRoleAccessRepository;
        private readonly INotyfService _notyf;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly EmailConfiguration _emailConfig;

        public AccessController(ILogger<AdminActionsController> logger,IRoleAccessRepository IRoleAccessRepository,INotyfService notyf, IComboboxRepository combobox, EmailConfiguration emailConfiguration)
        {
            _IRoleAccessRepository = IRoleAccessRepository;
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
        public async Task<IActionResult> UserAccess(int? User)
        {
            List<ViewUserAcces> data = await _IRoleAccessRepository.GetAllUserDetails(User);
            if (User != null)
            {
                return Json(data);
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
					_notyf.Success("Role Add Successfully...");
				}
				else
				{
					_notyf.Error("Role not Add...");
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

		#region DeletePhysician
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
				_notyf.Success("Role not deleted successfully...");
				return RedirectToAction("Index");
			}
		}
		#endregion
	}
}
