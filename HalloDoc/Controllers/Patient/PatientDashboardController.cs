using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
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
    public class PatientDashboardController : Controller
    {
        private readonly IPatientDashboard _IPatientDashboard;
        private readonly ILogger<AdminDashBoardController> _logger;
        private readonly IAdminDashBoardActionsRepository _IAdminDashBoardActionsRepository;
        private readonly INotyfService _notyf;

        public PatientDashboardController(ILogger<AdminDashBoardController> logger, IPatientDashboard iPatientDashboard, IAdminDashBoardActionsRepository iAdminDashBoardActionsRepository, INotyfService iNotyfService)
        {
            _IPatientDashboard = iPatientDashboard;
            _logger = logger;
            _IAdminDashBoardActionsRepository = iAdminDashBoardActionsRepository;
            _notyf = iNotyfService;
        }
        [CheckProviderAccess("Patient")]
        public IActionResult Index(PatientDashList listdata)
        {
            PatientDashList data = _IPatientDashboard.GetPatientRequest(CV.UserID(), listdata);
            return View("../Patient/PatientDashboard/Index",data);
        }
        public async Task<IActionResult> ViewUploads(int? id, ViewDocuments viewDocument)
        {
            if (id == null)
            {
                id = viewDocument.RequestID;
            }
            ViewDocuments data = await _IAdminDashBoardActionsRepository.GetDocumentByRequest(id, viewDocument);
            return View("../Patient/PatientDashboard/ViewUploads", data);
        }
        public IActionResult UploadDoc(int Requestid, IFormFile file)
        {
            if (_IAdminDashBoardActionsRepository.SaveDoc(Requestid, file))
            {
                _notyf.Success("File Uploaded Successfully");
            }
            else
            {
                _notyf.Error("File Not Uploaded");
            }
            return RedirectToAction("ViewUploads", "PatientDashboard", new { id = Requestid });
        }
        #region RequestForMe
        public IActionResult RequestForMe()
        {
            CreatePatientRequestModel model = _IPatientDashboard.RequestForMe(CV.UserID());
            return View("../Patient/PatientDashboard/RequestForMe", model);
        }
        #endregion RequestForMe

        #region CreateRequestForMe
        public async Task<IActionResult> CreateRequestForMe(CreatePatientRequestModel model)
        {
            if (await _IPatientDashboard.CreateRequestForMe(model))
            {
                _notyf.Success("Request has been created successfully");
            }
            else
            {
                _notyf.Error("Request has not been created successfully");
            }
            return RedirectToAction("Index", "PatientDashboard");
        }
        #endregion CreateRequestForMe

        #region RequestForSomeoneElse
        public IActionResult RequestForSomeoneElse()
        {
            return View("../Patient/PatientDashboard/RequestForSomeoneElse");
        }
        #endregion RequestForSomeoneElse

        #region CreateRequestForSomeoneElse
        public async Task<IActionResult> CreateRequestForSomeoneElse(CreatePatientRequestModel model)
        {
            if (await _IPatientDashboard.CreateRequestForSomeoneElse(model,CV.UserID()))
            {
                _notyf.Success("Request has been created successfully");
            }
            else
            {
                _notyf.Error("Request has not been created successfully");
            }
            return RedirectToAction("Index", "PatientDashboard");
        }
        #endregion CreateRequestForSomeoneElse
    }
}
