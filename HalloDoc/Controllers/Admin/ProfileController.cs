using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.Excel;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    [CheckProviderAccess("Admin", "MyProfile")]
    public class ProfileController : Controller
    {
        #region Constructor
        private readonly IMyProfileRepository _IMyProfileRepository;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminDashBoardController> _logger;
        private readonly INotyfService _notyf;
        public ProfileController(ILogger<AdminDashBoardController> logger, IMyProfileRepository IMyProfileRepository, IComboboxRepository combobox , INotyfService INotyfService)
        {
            _IMyProfileRepository = IMyProfileRepository;
            _combobox = combobox;
            _logger = logger;
            _notyf = INotyfService;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(int? id)
        {
            ViewAdminProfile p = await _IMyProfileRepository.GetProfileDetails((id != null ? (int)id : Convert.ToInt32(CV.UserID())));
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.AdminRoleComboBox();
            return View("../Admin/Profile/Index",p);
        }
        #endregion

        #region EditPassword
        public async Task<IActionResult> EditPassword(string password) 
        {
            if (await _IMyProfileRepository.EditPassword(password, Convert.ToInt32(CV.UserID())))
            {
                _notyf.Success("Password changed Successfully...");
            }
            else
            {
                _notyf.Error("Password not Changed...");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region EditAdministratorInfo
        [HttpPost]
        public async Task<IActionResult> EditAdministratorInfo(ViewAdminProfile _viewAdminProfile)
        {
            if (await _IMyProfileRepository.EditAdministratorInfo(_viewAdminProfile,CV.ID()))
            {
                _notyf.Success("Information changed Successfully...");
            }
            else
            {
                _notyf.Error("Information not Changed...");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region BillingInfoEdit
        [HttpPost]
        public async Task<IActionResult> BillingInfoEdit(ViewAdminProfile _viewAdminProfile)
        {
            if (await _IMyProfileRepository.BillingInfoEdit(_viewAdminProfile))
            {
                _notyf.Success("Information changed Successfully...");
            }
            else
            {
                _notyf.Error("Information not Changed...");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
