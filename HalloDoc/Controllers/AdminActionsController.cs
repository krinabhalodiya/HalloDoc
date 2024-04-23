using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Entity.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Models;
using ViewAsPdf = Rotativa.AspNetCore.ViewAsPdf;
using HalloDoc.Entity.DataModels;
using HalloDoc.Controllers.Admin;
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
        [CheckProviderAccess("Admin,Provider")]
        public async Task<IActionResult> ViewCase(int id)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewCaseData sm = _IAdminDashBoardActionsRepository.GetRequestForViewCase(id);

            return View("../AdminActions/ViewCase", sm);
        }
        #endregion

        #region EditCase
        [CheckProviderAccess("Admin,Provider")]
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
        [CheckProviderAccess("Admin,Provider", "Dashboard")]
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
        [CheckProviderAccess("Admin")]
        public IActionResult ProviderbyRegion(int? Regionid)
        {
            var v = _combobox.ProviderbyRegion(Regionid);
            return Json(v);
        }
        #endregion

        #region _CancelCase
        [CheckProviderAccess("Admin")]
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
        [CheckProviderAccess("Admin")]
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
        [CheckProviderAccess("Admin")]
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
        [CheckProviderAccess("Admin")]
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
        [CheckProviderAccess("Admin,Provider")]
        public IActionResult ViewNotes(int id)
        {

            ViewNotesData sm = _IAdminDashBoardActionsRepository.getNotesByID(id);
            return View("../AdminActions/ViewNotes", sm);
        }
        #endregion

        #region Edit_Notes
        [CheckProviderAccess("Admin,Provider")]
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
                _notyf.Information("Notes can not be empty!!");
                TempData["Errormassage"] = "Notes can not be empty!!";
                return RedirectToAction("ViewNotes", new { id = RequestID });
            }
        }
        #endregion

        #region View_Upload
        [CheckProviderAccess("Admin,Provider")]
        public async Task<IActionResult> ViewUpload(int? id , ViewDocuments viewDocument)
        {
            if (id == null)
            {
                id= viewDocument.RequestID;
            }
            ViewDocuments v = await _IAdminDashBoardActionsRepository.GetDocumentByRequest(id, viewDocument);
            return View("../AdminActions/ViewUpload",v);
        }
        #endregion

        #region UploadDoc_Files
        [CheckProviderAccess("Admin,Provider")]
        public IActionResult UploadDoc(int Requestid, IFormFile file)
        {
            var uploader = "Patient";
            if(CV.role() == "Provider")
            {
                uploader = "Provider";
            }
            else if(CV.role() == "Admin")
            {
                uploader = "Admin";
            }
            if (_IAdminDashBoardActionsRepository.SaveDoc(Requestid, file,CV.UserID(),uploader))
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
        [CheckProviderAccess("Admin,Provider")]
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
        [CheckProviderAccess("Admin,Provider")]
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

        #region SendFileEmail
        [CheckProviderAccess("Admin,Provider")]
        public async Task<IActionResult> SendFileEmail(string mailids, int Requestid, string email)
        {
            if (await _IAdminDashBoardActionsRepository.SendFileEmail(mailids, Requestid, email))
            {

                _notyf.Success("Mail Send successfully");
            }
            else
            {
                _notyf.Error("Mail is not send successfully");
            }
            return RedirectToAction("ViewUpload", "AdminActions", new { id = Requestid });
        }
        #endregion

        #region order_action
        [CheckProviderAccess("Admin,Provider", "SendOrder")]
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
        [CheckProviderAccess("Admin,Provider", "SendOrder")]
        public Task<IActionResult> ProfessionalByType(int HealthprofessionalID)
        {
            var v = _combobox.ProfessionalByType(HealthprofessionalID);
            return Task.FromResult<IActionResult>(Json(v));
        }
        [CheckProviderAccess("Admin,Provider", "SendOrder")]
        public Task<IActionResult> SelectProfessionalByID(int VendorID)
        {
            var v = _IAdminDashBoardActionsRepository.SelectProfessionlByID(VendorID);
            return Task.FromResult<IActionResult>(Json(v));
        }
        [CheckProviderAccess("Admin,Provider", "SendOrder")]
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
        [CheckProviderAccess("Admin,Provider")]
        public async Task<IActionResult> SendAgreementmail(int requestid)
        {
            if (_IAdminDashBoardActionsRepository.SendAgreement(requestid))
            {
                _notyf.Success("Mail Send  Successfully..!");
            }
            return RedirectToAction("Index", "AdminDashboard");
        }
        #endregion

        #region CloseCase
        [CheckProviderAccess("Admin")]
        public async Task<IActionResult> CloseCase(int RequestID)
        {
            ViewCloseCaseModel vc = _IAdminDashBoardActionsRepository.CloseCaseData(RequestID);
            return View("../AdminActions/CloseCase", vc);
        }
        [CheckProviderAccess("Admin")]
        public IActionResult CloseCaseUnpaid(int id)
        {
            bool result = _IAdminDashBoardActionsRepository.CloseCase(id);
            if (result)
            {
                _notyf.Success("Case Closed...");
                _notyf.Information("You can see Closed case in unpaid State...");

            }
            else
            {
                _notyf.Error("there is some error in CloseCase...");
            }
            return RedirectToAction("Index", "AdminDashboard");
        }
        #endregion

        #region EditForCloseCase
        [CheckProviderAccess("Admin")]
        public IActionResult EditForCloseCase(ViewCloseCaseModel sm)
        {
            bool result = _IAdminDashBoardActionsRepository.EditForCloseCase(sm);

            if (result)
            {
                _notyf.Success("Case Edited Successfully..");
                return RedirectToAction("CloseCase", new { sm.RequestID });
            }
            else
            {
                _notyf.Error("Case Not Edited...");
                return RedirectToAction("CloseCase", new { sm.RequestID });
            }

        }
        #endregion

        #region Encounter_View
        [CheckProviderAccess("Admin,Provider")]
        public IActionResult Encounter(int id)
        {
            ViewEncounterData data = _IAdminDashBoardActionsRepository.GetEncounterDetails(id);
            return View("../AdminActions/EncounterForm", data);
        }
        #endregion

        #region EncounterEdit
        [CheckProviderAccess("Admin,Provider")]
        public IActionResult EncounterEdit(ViewEncounterData data)
        {
            if (_IAdminDashBoardActionsRepository.EditEncounterDetails(data, CV.ID()))
            {
                _notyf.Success("Encounter Changes Saved...");
            }
            else
            {
                _notyf.Error("Encounter Changes Not Saved...");
            }
            return RedirectToAction("Encounter", new { id = data.Requesid });
        }
        #endregion

        #region Finalize
        [CheckProviderAccess("Provider")]
        public IActionResult Finalize(ViewEncounterData model)
        {
            bool data = _IAdminDashBoardActionsRepository.EditEncounterDetails(model, CV.ID());
            if (data)
            {
                bool final = _IAdminDashBoardActionsRepository.CaseFinalized(model);
                if (final)
                {
                    _notyf.Success("Case Is Finalized...");
                    if (CV.role() == "Provider")
                    {
                        return Redirect("~/Physician/DashBoard");
                    }
                    return RedirectToAction("Index", "AdminDashBoard");
                }
                else
                {
                    _notyf.Error("Case Is Not Finalized Please Enter Valid Data...");
                    return RedirectToAction("Encounter", new { id = model.Requesid });
                }
            }
            else
            {
                _notyf.Error("Case Is Not Finalized...");
                return RedirectToAction("Encounter", new { id = model.Requesid });
            }

        }
        #endregion

        #region _Accept RequestProviderPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckProviderAccess("Provider")]
        public async Task<IActionResult> _AcceptRequestPost(int RequestId , string Note)
        {
            if (await _IAdminDashBoardActionsRepository.AcceptPhysician(RequestId, Note, Convert.ToInt32(CV.UserID())))
            {
                _notyf.Success("Case Accepted...");
            }
            else
            {
                _notyf.Success("Case Not Accepted...");
            }
            return Redirect("~/Physician/DashBoard");
        }
        #endregion

        #region __TransfertoAdminPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckProviderAccess("Provider")]
        public async Task<IActionResult> _TransfertoAdminPost(int RequestID, string Note)
        {
            if (await _IAdminDashBoardActionsRepository.TransfertoAdmin(RequestID, Note,Convert.ToInt32(CV.UserID())))
            {
                _notyf.Success("Request Successfully transfered to admin...");
            }

            return Redirect("~/Physician/DashBoard");
        }
        #endregion

        #region ContactAdmin
        [CheckProviderAccess("Provider")]
        public IActionResult ContactAdmin(string Note)
        {
            bool Contact = _IAdminDashBoardActionsRepository.ContactAdmin(Convert.ToInt32(CV.UserID()), Note);
            if (Contact)
            {
                _notyf.Success("Mail Send Succesfully Successfully");
            }
            else
            {
                _notyf.Error("Mail Not Send Succesfully");
            }
            return RedirectToAction("PhysicianProfile", "Providers", new { id = Convert.ToInt32(CV.UserID()) });
        }
        #endregion

        #region Housecall
        [CheckProviderAccess("Provider")]
        public IActionResult Housecall(int RequestId)
        {
            if (_IAdminDashBoardActionsRepository.Housecall(RequestId))
            {
                _notyf.Success("Case Accepted...");
            }
            else
            {
                _notyf.Error("Case Not Accepted...");
            }
            return Redirect("~/Physician/DashBoard");
        }
        #endregion

        #region Consult
        [CheckProviderAccess("Provider")]
        public IActionResult Consult(int RequestId)
        {
            if (_IAdminDashBoardActionsRepository.Consult(RequestId))
            {
                _notyf.Success("Case is in conclude state...");
            }
            else
            {
                _notyf.Error("Error...");
            }
            return Redirect("~/Physician/DashBoard");
        }
        #endregion

        #region generatePDF
        [CheckProviderAccess("Admin,Provider")]
        public IActionResult generatePDF(int id)
        {
            var FormDetails = _IAdminDashBoardActionsRepository.GetEncounterDetails(id);
            return new ViewAsPdf("../AdminActions/EncounterPdf", FormDetails);
        }
        #endregion

        #region View_Upload
        [CheckProviderAccess("Provider")]
        public async Task<IActionResult> ConcludeCare(int? id, ViewDocuments viewDocument)
		{
			if (id == null)
			{
				id = viewDocument.RequestID;
			}
			ViewDocuments v = await _IAdminDashBoardActionsRepository.GetDocumentByRequest(id, viewDocument);
			return View("../AdminActions/ConcludeCare", v);
		}
        #endregion

        #region UploadDoc_Files
        [CheckProviderAccess("Provider")]
        public IActionResult UploadDocProvider(int Requestid, IFormFile file)
        {
            var requesttype = "Provider";
            if (CV.role() == "Admin")
            {
                 requesttype = "Admin";
            }
            if (_IAdminDashBoardActionsRepository.SaveDoc(Requestid, file,CV.UserID(), requesttype))
            {
                _notyf.Success("File Uploaded Successfully");
            }
            else
            {
                _notyf.Error("File Not Uploaded");
            }
            return RedirectToAction("ConcludeCare", "AdminActions", new { id = Requestid });
        }
        #endregion

        #region ConcludecarePost
        [CheckProviderAccess("Provider")]
        public IActionResult ConcludeCarePost(int RequestId, string Notes)
        {
            if (_IAdminDashBoardActionsRepository.ConcludeCarePost(RequestId, Notes))
            {
                _notyf.Success("Case concluded...");
            }
            else
            {
                _notyf.Error("Error...");
            }
            return Redirect("~/Physician/DashBoard");
        }
        #endregion
    }
}
