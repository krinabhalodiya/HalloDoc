using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class InvoicingController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly INotyfService _notyf;

        public InvoicingController(IInvoiceRepository invoiceRepository, INotyfService notyf)
        {
            _invoiceRepository = invoiceRepository;
            _notyf = notyf;
        }
        [CheckProviderAccess("Admin,Provider", "Invoicing")]
        [Route("Physician/Invoicing")]

        public IActionResult Index()
        {
            return View("../Admin/Invoicing/Index");
        }
        [Route("/Admin/Invoicing")]
        public IActionResult IndexAdmin()
        {
            ViewBag.GetAllPhysicians = _invoiceRepository.GetAllPhysicians();
            return View("../Admin/Invoicing/IndexAdmin");
        }
        #region IsFinalizeSheet

        public IActionResult IsFinalizeSheet(int PhysicianId, DateOnly StartDate)
        {
            bool x = _invoiceRepository.isFinalizeTimesheet(PhysicianId, StartDate);
            return Json(new { x });
        }
        public IActionResult IsApproveSheet(int PhysicianId, DateOnly StartDate)
        {
            var x = _invoiceRepository.GetPendingTimesheet(PhysicianId, StartDate);

            return PartialView("../Admin/Invoicing/_PendingApprove", x);
        }
        #endregion

        #region TimeSheetDetailsAddEdit_PageData

        public async Task<IActionResult> Timesheet(int PhysicianId, DateOnly StartDate)
        {
            if (CV.role() == "Provider" && _invoiceRepository.isFinalizeTimesheet(PhysicianId, StartDate))
            {
                _notyf.Error("Sheet Is Already Finalize");
                return RedirectToAction("Index");
            }
            int AfterDays = StartDate.Day == 1 ? 14 : DateTime.DaysInMonth(StartDate.Year, StartDate.Month) - 14; ;
            var TimeSheetDetails = _invoiceRepository.PostTimesheetDetails(PhysicianId, StartDate, AfterDays, CV.ID());
            List<ViewTimesheetdetailreimbursementsdata> h = await _invoiceRepository.GetTimesheetBills(TimeSheetDetails);
            var Timesheet = _invoiceRepository.GetTimesheetDetails(TimeSheetDetails, h, PhysicianId);
            Timesheet.PhysicianId = PhysicianId;
            return View("../Admin/Invoicing/Timesheet", Timesheet);
        }
        #endregion

        public async Task<IActionResult> GetTimesheetDetailsData(int PhysicianId, DateOnly StartDate)
        {
            var Timesheet = new ViewTimeSheet();
            if (StartDate == DateOnly.MinValue)
            {
                Timesheet.ViewTimesheetdetails = new List<ViewTimesheetdetails> { };
                Timesheet.ViewTimesheetdetailreimbursements = new List<ViewTimesheetdetailreimbursementsdata> { };
            }
            else
            {
                List<Timesheetdetail> x = _invoiceRepository.PostTimesheetDetails(PhysicianId, StartDate, 0, CV.ID());
                List<ViewTimesheetdetailreimbursementsdata> h = await _invoiceRepository.GetTimesheetBills(x);
                Timesheet = _invoiceRepository.GetTimesheetDetails(x, h, PhysicianId);
            }
            if (Timesheet == null)
            {
                var Timesheets = new ViewTimeSheet();
                Timesheets.ViewTimesheetdetails = new List<ViewTimesheetdetails> { };
                Timesheets.ViewTimesheetdetailreimbursements = new List<ViewTimesheetdetailreimbursementsdata> { };
                return PartialView("../Admin/Invoicing/_TimesheetTable", Timesheets);
            }


            return PartialView("../Admin/Invoicing/_TimesheetTable", Timesheet);
        }
        public IActionResult TimeSheetDetailsEdit(ViewTimeSheet viewTimeSheet, int PhysicianId)
        {
            if (_invoiceRepository.PutTimesheetDetails(viewTimeSheet.ViewTimesheetdetails, CV.ID()))
            {
                _notyf.Success("Edit  TimeSheet  Successfully..!");
            }

            return RedirectToAction("Timesheet", new { PhysicianId, StartDate = viewTimeSheet.ViewTimesheetdetails[0].Timesheetdate });
        }
        public IActionResult TimeSheetBillAddEdit(int? Trid, DateOnly Timesheetdate, IFormFile file, int Timesheetdetailid, int Amount, string Item, int PhysicianId, DateOnly StartDate)
        {
            ViewTimesheetdetailreimbursementsdata timesheetdetailreimbursement = new ViewTimesheetdetailreimbursementsdata();
            timesheetdetailreimbursement.Timesheetdetailid = Timesheetdetailid;
            timesheetdetailreimbursement.Timesheetdetailreimbursementid = Trid;
            timesheetdetailreimbursement.Amount = Amount;
            timesheetdetailreimbursement.Billfile = file;
            timesheetdetailreimbursement.Itemname = Item;
            if (_invoiceRepository.TimeSheetBillAddEdit(timesheetdetailreimbursement, CV.ID()))
            {
                _notyf.Success("Bill Change Successfully..!");
            }
            return RedirectToAction("Timesheet", new { PhysicianId = PhysicianId, StartDate = StartDate });
        }
        #region TimeSheetBill_Delete
        public IActionResult TimeSheetBillRemove(int? Trid, int PhysicianId, DateOnly StartDate)
        {
            ViewTimesheetdetailreimbursementsdata timesheetdetailreimbursement = new ViewTimesheetdetailreimbursementsdata();
            timesheetdetailreimbursement.Timesheetdetailreimbursementid = Trid;
            if (_invoiceRepository.TimeSheetBillRemove(timesheetdetailreimbursement, CV.ID()))
            {
                _notyf.Success("Bill deleted Successfully..!");
            }
            return RedirectToAction("Timesheet", new { PhysicianId = PhysicianId, StartDate = StartDate });
        }
        #endregion
        public IActionResult SetToFinalize(int timesheetid)
        {
            if (_invoiceRepository.SetToFinalize(timesheetid, CV.ID()))
            {
                _notyf.Success("Sheet Is Finalize Successfully..!");
            }
            return RedirectToAction("Index");
        }
    }
}
