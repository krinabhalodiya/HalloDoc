using AspNetCoreHero.ToastNotification.Abstractions;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class SendAgreement : Controller
    {
        private readonly IAdminDashBoardActionsRepository _IAdminDashBoardActionsRepository;
        private readonly INotyfService _notyf;
        public SendAgreement(IAdminDashBoardActionsRepository actionrepo,INotyfService notyf)
        {
            _IAdminDashBoardActionsRepository = actionrepo;
            _notyf = notyf;
        }
        public IActionResult Index(int RequestID)
        {
            TempData["RequestID"] = " " + RequestID;
            TempData["PatientName"] = "krina bhalodiya";
            
            return View();
        }
        public IActionResult accept(int RequestID)
        {
            _IAdminDashBoardActionsRepository.SendAgreement_accept(RequestID);
            return RedirectToAction("Index", "AdminDashBoard");
        }

        public IActionResult Reject(int RequestID, string Notes)
        {
            _IAdminDashBoardActionsRepository.SendAgreement_Reject(RequestID, Notes);
            return RedirectToAction("Index", "AdminDashBoard");
        }
    }
}
