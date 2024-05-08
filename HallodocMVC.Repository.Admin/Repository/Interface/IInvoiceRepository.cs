using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
	public interface IInvoiceRepository
	{
        bool isFinalizeTimesheet(int PhysicianId, DateOnly StartDate);
        bool isApprovedTimesheet(int PhysicianId, DateOnly StartDate);
        List<Timesheetdetail> PostTimesheetDetails(int PhysicianId, DateOnly StartDate, int AfterDays, string AdminId);
        ViewTimeSheet GetTimesheetDetails(List<Timesheetdetail> td, List<Timesheetdetailreimbursement> tr, int PhysicianId);
        bool PutTimesheetDetails(List<ViewTimesheetdetails> tds, string AdminId);
        bool TimeSheetBillAddEdit(ViewTimesheetdetailreimbursements trb, string AdminId);
        Task<List<Timesheetdetailreimbursement>> GetTimesheetBills(List<Timesheetdetail> TimeSheetDetails);
        bool SetToFinalize(int timesheetid, string AdminId);
        bool TimeSheetBillRemove(ViewTimesheetdetailreimbursements trb, string AdminId);
        Task<bool> SetToApprove(ViewTimeSheet vts, string AdminId);
        List<PhysiciansData> GetAllPhysicians();
        public List<Timesheet> GetPendingTimesheet(int PhysicianId, DateOnly StartDate);
    }
}
