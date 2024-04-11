using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Controllers.Admin;
using HalloDoc.Entity.Models;
using HalloDoc.Entity.Models.PatientModels;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using HallodocMVC.Repository.Patient.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Patient
{
    public class PatientProfile : Controller
    {
        private readonly IProfile _IProfile;
        private readonly ILogger<AdminDashBoardController> _logger;
        private readonly INotyfService _notyf;

        public PatientProfile(ILogger<AdminDashBoardController> logger, IProfile iProfile, INotyfService iNotyfService)
        {
            _IProfile = iProfile;
            _logger = logger;
            _notyf = iNotyfService;
        }
        [CheckProviderAccess("Patient")]
        public IActionResult Index()
        {
            ViewDataUserProfile data = _IProfile.ProviderProfile(CV.ID());
            return View("../Patient/PatientProfile/Index", data);
        }
        public async Task<IActionResult> EditPatientProfile(ViewDataUserProfile data)
        {
            if (await _IProfile.Edit(data))
            {
                _notyf.Success("Information changed Successfully...");
            }
            else
            {
                _notyf.Error("Information not Changed...");
            }
            return RedirectToAction("Index");
        }
    }
}
