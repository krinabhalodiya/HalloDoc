using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Entity.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HalloDoc.Controllers
{
    public class AdminActionsController : Controller
    {
        private readonly IAdminDashBoardActionsRepository _IAdminDashBoardActionsRepository;
        private readonly IComboboxRepository _combobox;
        private readonly INotyfService _notyf;
        private readonly ILogger<AdminActionsController> _logger;

        public AdminActionsController(ILogger<AdminActionsController> logger,
                                      IComboboxRepository combobox,
                                      IAdminDashBoardActionsRepository actionrepo,
                                      INotyfService notyf)
        {
            _combobox = combobox;
            _IAdminDashBoardActionsRepository = actionrepo;
            _notyf = notyf;
            _logger = logger;
        }
        public async Task<IActionResult> ViewCase(int id)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.CaseReasonComboBox = await _combobox.CaseReasonComboBox();
            ViewCaseData sm = _IAdminDashBoardActionsRepository.GetRequestForViewCase(id);

            return View("../AdminActions/ViewCase", sm);
        }
        public IActionResult EditCase(ViewCaseData vcd)
        {
            bool result = _IAdminDashBoardActionsRepository.EditCase(vcd);
            if (result)
            {
                return RedirectToAction("ViewCase", new { id = vcd.RequestID });
            }
            else
            {
                return View("../AdminActions/ViewCase", vcd);
            }
        }
        #region AssignProvider
        public async Task<IActionResult> AssignProvider(int requestid, int ProviderId, string Notes)
        {
            if (await _IAdminDashBoardActionsRepository.AssignProvider(requestid, ProviderId, Notes))
            {
                _notyf.Success("Physician Assigned successfully...");
            }
            else
            {
                _notyf.Error("Physician Not Assigned...");
            }

            return RedirectToAction("Index", "AdminDashBoard");
        }
        #endregion
        #region providerbyregion
        public IActionResult ProviderbyRegion(int? Regionid)
        {
            var v = _combobox.ProviderbyRegion(Regionid);
            return Json(v);
        }
        #endregion
    }
}
