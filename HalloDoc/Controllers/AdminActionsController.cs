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
        #region _CancelCase
        public IActionResult CancelCase(int RequestID, string Note, string CaseTag)
        {
            bool CancelCase = _IAdminDashBoardActionsRepository.CancelCase(RequestID, Note, CaseTag);
            if (CancelCase)
            {
                _notyf.Success("Case Canceled Successfully");

            }
            else
            {
                _notyf.Error("Case Not Canceled");

            }
            return RedirectToAction("Index", "AdminDashBoard");
        }
        #endregion
        #region _BlockCase
        public IActionResult BlockCase(int RequestID, string Note)
        {
            bool BlockCase = _IAdminDashBoardActionsRepository.BlockCase(RequestID, Note);
            if (BlockCase)
            {
                _notyf.Success("Case Blocked Successfully");
            }
            else
            {
                _notyf.Error("Case Not Blocked");
            }
            return RedirectToAction("Index", "AdminDashBoard");
        }
        #endregion
        #region AssignProvider
        public async Task<IActionResult> TransferProvider(int requestid, int ProviderId, string Notes)
        {
            if (await _IAdminDashBoardActionsRepository.TransferProvider(requestid, ProviderId, Notes))
            {
                _notyf.Success("Physician Transfered successfully...");
            }
            else
            {
                _notyf.Error("Physician Not Transfered...");
            }

            return RedirectToAction("Index", "AdminDashBoard", new { Status = "2" });
        }
        #endregion
        #region Clear_case
        public IActionResult ClearCase(int RequestID)
        {
            bool cc = _IAdminDashBoardActionsRepository.ClearCase(RequestID);
            if (cc)
            {
                _notyf.Success("Case Cleared...");
                _notyf.Warning("You can not show Cleared Case ...");
            }
            else
            {
                _notyf.Error("there is some error in deletion...");
            }
            return RedirectToAction("Index", "AdminDashBoard", new { Status = "2" });
        }
        #endregion
        #region View_Notes
        public IActionResult ViewNotes(int id)
        {

            ViewNotesData sm = _IAdminDashBoardActionsRepository.getNotesByID(id);
            return View("../AdminActions/ViewNotes", sm);
        }
        #endregion
        #region Edit_Notes
        public IActionResult ChangeNotes(int RequestID, string? adminnotes, string? physiciannotes)
        {
            if (adminnotes != null || physiciannotes != null)
            {
                bool result = _IAdminDashBoardActionsRepository.EditViewNotes(adminnotes, physiciannotes, RequestID);
                if (result)
                {
                    _notyf.Success("Notes Updated successfully...");
                    return RedirectToAction("ViewNotes", new { id = RequestID });
                }
                else
                {
                    _notyf.Error("Notes Note Updated");
                    return View("../AdminSite/Action/ViewNotes");
                }
            }
            else
            {
                _notyf.Information("Please Select one of the note!!");
                TempData["Errormassage"] = "Please Select one of the note!!";
                return RedirectToAction("ViewNotes", new { id = RequestID });
            }
        }
        #endregion
    }
}
