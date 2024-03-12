using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Entity.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HalloDoc.Controllers
{
    public class AdminActionsController : Controller
    {
        #region Constructor
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
        #endregion

        #region ViewCase
        public async Task<IActionResult> ViewCase(int id)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewCaseData sm = _IAdminDashBoardActionsRepository.GetRequestForViewCase(id);

            return View("../AdminActions/ViewCase", sm);
        }
        #endregion

        #region EditCase
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
        #endregion

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

        #region TransferProvider
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
                    _notyf.Error("Notes Not Updated");
                    return View("../AdminActions/ViewNotes");
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

        #region View_Upload
        public async Task<IActionResult> ViewUpload(int? id)
        {
            ViewDocuments v = await _IAdminDashBoardActionsRepository.GetDocumentByRequest(id);
            return View("../AdminActions/ViewUpload",v);
        }
        #endregion

        #region UploadDoc_Files
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
            return RedirectToAction("ViewUpload", "AdminActions", new { id = Requestid });
        }
        #endregion

        #region DeleteOnesFile
        public async Task<IActionResult> DeleteFile(int? id, int Requestid)
        {
            if (await _IAdminDashBoardActionsRepository.DeleteDocumentByRequest(id.ToString()))
            {
                _notyf.Success("File Deleted Successfully");
            }
            else
            {
                _notyf.Error("File Not Deleted");
            }
            return RedirectToAction("ViewUpload", "AdminActions", new { id = Requestid });
        }
        #endregion

        #region AllFilesDelete
        public async Task<IActionResult> AllFilesDelete(string deleteids, int Requestid)
        {
            if (await _IAdminDashBoardActionsRepository.DeleteDocumentByRequest(deleteids))
            {
                _notyf.Success("All Selected File Deleted Successfully");
            }
            else
            {
                _notyf.Error("All Selected File Not Deleted");
            }
            return RedirectToAction("ViewUpload", "AdminActions", new { id = Requestid });
        }
        #endregion

        #region order_action
        public async Task<IActionResult> Order(int id)
        {
            List<HealthProfessionalTypeComboBox> cs = await _combobox.healthprofessionaltype();
            ViewBag.ProfessionType = cs;
            ViewSendOrderData data = new ViewSendOrderData
            {
                RequestID = id
            };
            return View("../AdminActions/SendOrders", data);
        }
        public Task<IActionResult> ProfessionalByType(int HealthprofessionalID)
        {
            var v = _combobox.ProfessionalByType(HealthprofessionalID);
            return Task.FromResult<IActionResult>(Json(v));
        }
        public Task<IActionResult> SelectProfessionalByID(int VendorID)
        {
            var v = _IAdminDashBoardActionsRepository.SelectProfessionlByID(VendorID);
            return Task.FromResult<IActionResult>(Json(v));
        }
        public IActionResult SendOrder(ViewSendOrderData sm)
        {
            if (ModelState.IsValid)
            {
                bool data = _IAdminDashBoardActionsRepository.SendOrder(sm);
                if (data)
                {
                    _notyf.Success("Order Created  successfully...");
                    _notyf.Information("Mail is sended to Vendor successfully...");
                    return RedirectToAction("Index", "AdminDashBoard");
                }
                else
                {
                    _notyf.Error("Order Not Created...");
                    return View("../AdminActions/SendOrders", sm);
                }
            }
            else
            {
                return View("../AdminActions/SendOrders", sm);
            }
        }
        #endregion

        #region SendAgreement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAgreementmail(int requestid)
        {
            if (_IAdminDashBoardActionsRepository.SendAgreement(requestid))
            {
                _notyf.Success("Mail Send  Successfully..!");
            }
            return RedirectToAction("Index", "AdminDashboard");
        }
        #endregion
    }
}
