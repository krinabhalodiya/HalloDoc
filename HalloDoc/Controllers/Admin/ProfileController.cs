using DocumentFormat.OpenXml.Office2010.Excel;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProfileController : Controller
    {
        private readonly IMyProfileRepository _IMyProfileRepository;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminDashBoardController> _logger;

        public ProfileController(ILogger<AdminDashBoardController> logger, IMyProfileRepository IMyProfileRepository, IComboboxRepository combobox)
        {
            _IMyProfileRepository = IMyProfileRepository;
            _combobox = combobox;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int? id)
        {
            ViewAdminProfile p = await _IMyProfileRepository.GetProfileDetails((id != null ? (int)id : Convert.ToInt32(CV.UserID())));
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.UserRoleComboBox();
            return View("../Admin/Profile/Index",p);
        }
    }
}
