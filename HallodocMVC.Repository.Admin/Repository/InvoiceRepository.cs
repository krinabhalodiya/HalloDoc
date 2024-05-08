﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        #region Constructor
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly EmailConfiguration _emailConfig;
        private readonly HalloDocContext _context;
        public InvoiceRepository(HalloDocContext context, EmailConfiguration emailConfig, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _emailConfig = emailConfig;
            this.httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Timesheet_Approved_Or_Not
        /// <summary>
        /// Check TimeSheet Is Approved Or Not
        /// </summary>
        /// <param name="PhysicianId"></param>
        /// <param name="StartDate"></param>
        /// <returns></returns>
        public bool isApprovedTimesheet(int PhysicianId, DateOnly StartDate)
        {
            var data = _context.Timesheets.Where(e => e.Physicianid == PhysicianId && e.Startdate == StartDate).FirstOrDefault();
            if (data.Isapproved == false)
            {

                return false;
            }
            return true;
        }
        #endregion

        #region Timesheet_Finalize_Or_Not
        /// <summary>
        /// Check TimeSheet Is Finalize Or Not
        /// </summary>
        /// <param name="PhysicianId"></param>
        /// <param name="StartDate"></param>
        /// <returns></returns>
        public bool isFinalizeTimesheet(int PhysicianId, DateOnly StartDate)
        {
            var data = _context.Timesheets.Where(e => e.Physicianid == PhysicianId && e.Startdate == StartDate).FirstOrDefault();
            if (data == null)
            {
                return false;
            }
            else if (data.Isfinalize == false)
            {

                return false;
            }
            return true;
        }
        #endregion

        #region Set_To_Sheet_Finalize
        /// <summary>
        /// Provider Set TimeSheet To Finalize
        /// </summary>
        /// <param name="timesheetid"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public bool SetToFinalize(int timesheetid, string AdminId)
        {
            try
            {
                var data = _context.Timesheets.Where(e => e.Timesheetid == timesheetid).FirstOrDefault();
                if (data != null)
                {
                    data.Isfinalize = true;
                    _context.Timesheets.Update(data);
                    _context.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        #endregion

        #region Set_To_Sheet_Approve
        /// <summary>
        /// Admin Approve provider Timesheet
        /// </summary>
        /// <param name="vts"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public async Task<bool> SetToApprove(ViewTimeSheet vts, string AdminId)
        {
            try
            {
                var data = _context.Timesheets.Where(e => e.Timesheetid == vts.Timesheeid).FirstOrDefault();
                if (data != null)
                {
                    data.Isapproved = true;
                    data.Bonusamount = vts.Bonus;
                    data.Adminnotes = vts.AdminNotes;
                    _context.Timesheets.Update(data);
                    _context.SaveChanges();
                }
                var d = httpContextAccessor.HttpContext.Request.Host;
                //var res = _context.Requestclients.FirstOrDefault(e => e.Requestid == v.RequestID);
                string emailContent = @"
                                <!DOCTYPE html>
                                <html lang=""en"">
                                <head>
                                 <meta charset=""UTF-8"">
                                 <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                 <title>Provider</title>
                                </head>
                                <body>
                                 <div style=""background-color: #f5f5f5; padding: 20px;"">
                                 <h2>Welcome to Our Healthcare Platform!</h2>
                                <p>Dear Provider ,</p>
                                <ol>
                                    <li>Your TimeSheet Startwith" + data.Startdate + @""" and End With " + data.Enddate + @""" Is Approve</li>
                                </ol>
                                <p>If you have any questions or need further assistance, please don't hesitate to contact us.</p>
                                <p>Thank you,</p>
                                <p>The Healthcare Team</p>
                                </div>
                                </body>
                                </html>
                                ";
                _emailConfig.SendMail("dasete8625@haislot.com", "New Order arrived", emailContent);
                Emaillog log = new()
                {
                    Emailtemplate = emailContent,
                    Subjectname = "New Order arrived",
                    Emailid = "dasete8625@haislot.com",
                    Createdate = DateTime.Now,
                    Sentdate = DateTime.Now,
                    Physicianid = data.Physicianid,
                    Action = 12,
                    Isemailsent = new BitArray(new[] { true }),
                    Senttries = 1,
                    Roleid = 3,
                };
                _context.Emaillogs.Add(log);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Timesheet_Add
        public List<Timesheetdetail> PostTimesheetDetails(int PhysicianId, DateOnly StartDate, int AfterDays, string AdminId)
        {
            var Timesheet = new Timesheet();
            var data = _context.Timesheets.Where(e => e.Physicianid == PhysicianId && e.Startdate == StartDate).FirstOrDefault();
            if (data == null && AfterDays != 0)
            {
                DateOnly EndDate = StartDate.AddDays(AfterDays - 1);
                Timesheet.Startdate = StartDate;
                Timesheet.Physicianid = PhysicianId;
                Timesheet.Isfinalize = false;
                Timesheet.Enddate = EndDate;
                Timesheet.Createddate = DateTime.Now;
                Timesheet.Createdby = AdminId;
                _context.Timesheets.Add(Timesheet);
                _context.SaveChanges();

                for (DateOnly i = StartDate; i <= EndDate; i = i.AddDays(1))
                {
                    var Timesheetdetail = new Timesheetdetail
                    {
                        Timesheetid = Timesheet.Timesheetid,
                        Timesheetdate = i,
                        Numberofhousecall = 0,
                        Numberofphonecall = 0,
                        Totalhours = 0
                    };
                    _context.Timesheetdetails.Add(Timesheetdetail);
                    _context.SaveChanges();
                }

                return _context.Timesheetdetails.Where(e => e.Timesheetid == Timesheet.Timesheetid).OrderBy(r => r.Timesheetdate).ToList();
            }
            else if (data == null && AfterDays == 0)
            {
                return null;

            }
            else
            {
                return _context.Timesheetdetails.Where(e => e.Timesheetid == data.Timesheetid).OrderBy(r => r.Timesheetdate).ToList();
            }

        }
        #endregion

        #region Timesheet_Edit
        /// <summary>
        /// Edit TimeSheetDetails By Admin Or Provider 
        /// </summary>
        /// <param name="tds"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public bool PutTimesheetDetails(List<ViewTimesheetdetails> tds, string AdminId)
        {
            try
            {
                foreach (var item in tds)
                {
                    var td = _context.Timesheetdetails.Where(r => r.Timesheetdetailid == item.Timesheetdetailid).FirstOrDefault();
                    td.Totalhours = item.Totalhours;
                    td.Numberofhousecall = item.Numberofhousecall;
                    td.Numberofphonecall = item.Numberofphonecall;
                    td.Isweekend = item.Isweekend;
                    td.Modifiedby = AdminId;
                    td.Modifieddate = DateTime.Now;
                    _context.Timesheetdetails.Update(td);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        #endregion

        #region Timesheet_Get
        /// <summary>
        /// Get Full Of information That Particuler time Sheet
        /// </summary>
        /// <param name="td"></param>
        /// <param name="tr"></param>
        /// <param name="PhysicianId"></param>
        /// <returns></returns>
        public ViewTimeSheet GetTimesheetDetails(List<Timesheetdetail> td, List<Timesheetdetailreimbursement> tr, int PhysicianId)
        {
            try
            {
                var TimeSheet = new ViewTimeSheet();

                TimeSheet.ViewTimesheetdetails = td.Select(e => new ViewTimesheetdetails
                {
                    Isweekend = e.Isweekend == null || e.Isweekend == false ? false : true,
                    Modifiedby = e.Modifiedby,
                    Modifieddate = e.Modifieddate,
                    Numberofhousecall = e.Numberofhousecall,
                    Numberofphonecall = e.Numberofphonecall,
                    OnCallhours = FindOnCallProvider(PhysicianId, e.Timesheetdate),
                    Timesheetdate = e.Timesheetdate,
                    Timesheetdetailid = e.Timesheetdetailid,
                    Totalhours = e.Totalhours,
                    Timesheetid = e.Timesheetid
                }).OrderBy(r => r.Timesheetdate).ToList();

                TimeSheet.ViewTimesheetdetailreimbursements = tr.Select(e => new ViewTimesheetdetailreimbursements
                {
                    Amount = e.Amount,
                    Timesheetdetailreimbursementid = e.Timesheetdetailreimbursementid,
                    Isdeleted = e.Isdeleted,
                    Itemname = e.Itemname,
                    Bill = e.Bill,
                    Createddate = e.Createddate,
                    Timesheetdate = _context.Timesheetdetails.Where(r => r.Timesheetdetailid == e.Timesheetdetailid).FirstOrDefault().Timesheetdate,
                    Timesheetid = _context.Timesheetdetails.Where(r => r.Timesheetdetailid == e.Timesheetdetailid).FirstOrDefault().Timesheetid,
                    Modifiedby = e.Modifiedby,
                    Timesheetdetailid = e.Timesheetdetailid,
                }).OrderBy(r => r.Timesheetdetailid).ToList();

                TimeSheet.PayrateWithProvider = _context.Payratebyproviders.Where(r => r.Physicianid == PhysicianId).ToList();
                if (td.Count > 0)
                {
                    TimeSheet.Timesheeid = TimeSheet.ViewTimesheetdetails[0].Timesheetid;
                }
                TimeSheet.PhysicianId = PhysicianId;
                return TimeSheet;
            }
            catch (Exception e)
            {
                return null;
            }


        }
        #endregion

        #region FindOnCallProvider
        /// <summary>
        /// Find Provider Shift By date
        /// </summary>
        /// <param name="PhysicianId"></param>
        /// <param name="Timesheetdate"></param>
        /// <returns></returns>
        public int FindOnCallProvider(int PhysicianId, DateOnly Timesheetdate)
        {
            int i = 0;
            var s = _context.Shifts.Where(r => r.Physicianid == PhysicianId).ToList();
            foreach (var item in s)
            {
                i += _context.Shiftdetails.Where(r => r.Shiftid == item.Shiftid && DateOnly.FromDateTime(r.Shiftdate) == Timesheetdate).Count();
            }
            return i;
        }
        #endregion

        #region Timesheet_Bill_Get
        /// <summary>
        /// Get Bill Of TimeSheet
        /// </summary>
        /// <param name="TimeSheetDetails"></param>
        /// <returns>List Of TimeSheetBill</returns>
        public async Task<List<Timesheetdetailreimbursement>> GetTimesheetBills(List<Timesheetdetail> TimeSheetDetails)
        {
            try
            {
                var TimeSheetBills = await _context.Timesheetdetailreimbursements
                                     .Where(e => TimeSheetDetails.Contains(e.Timesheetdetail) && e.Isdeleted == false)
                                     .OrderBy(r => r.Timesheetdetailid)
                                     .ToListAsync();

                return TimeSheetBills;
            }
            catch (Exception e)
            {
                return null;
            }


        }
        #endregion

        #region TimeSheet_Bill_AddEdit
        /// <summary>
        /// add Bill Or Edit ONli Item Or Amount Of The Bill
        /// </summary>
        /// <param name="trb"></param>
        /// <param name="AdminId"></param>
        /// <returns> true Or false If Add Or Upadted </returns>
        public bool TimeSheetBillAddEdit(ViewTimesheetdetailreimbursements trb, string AdminId)
        {
            Timesheetdetail data = _context.Timesheetdetails.Where(e => e.Timesheetdetailid == trb.Timesheetdetailid).FirstOrDefault();
            if (data != null && trb.Timesheetdetailreimbursementid == null)
            {
                Timesheetdetailreimbursement timesheetdetailreimbursement = new Timesheetdetailreimbursement();
                timesheetdetailreimbursement.Timesheetdetailid = trb.Timesheetdetailid;
                timesheetdetailreimbursement.Amount = (int)trb.Amount;
                timesheetdetailreimbursement.Bill = FileSave.UploadTimesheetDoc(trb.Billfile, data.Timesheetid);
                timesheetdetailreimbursement.Itemname = trb.Itemname;
                timesheetdetailreimbursement.Createddate = DateTime.Now;
                timesheetdetailreimbursement.Createdby = AdminId;
                timesheetdetailreimbursement.Isdeleted = false;
                _context.Timesheetdetailreimbursements.Add(timesheetdetailreimbursement);
                _context.SaveChanges();


                return true;
            }
            else if (data != null && trb.Timesheetdetailreimbursementid != null)
            {
                Timesheetdetailreimbursement timesheetdetailreimbursement = _context.Timesheetdetailreimbursements.Where(r => r.Timesheetdetailreimbursementid == trb.Timesheetdetailreimbursementid).FirstOrDefault(); ;
                timesheetdetailreimbursement.Amount = (int)trb.Amount;

                timesheetdetailreimbursement.Itemname = trb.Itemname;
                timesheetdetailreimbursement.Modifieddate = DateTime.Now;
                timesheetdetailreimbursement.Modifiedby = AdminId;
                _context.Timesheetdetailreimbursements.Update(timesheetdetailreimbursement);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region TimeSheetBill_Delete
        /// <summary>
        /// Deleted Bill OF TimeSheet By Provider
        /// </summary>
        /// <param name="trb"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public bool TimeSheetBillRemove(ViewTimesheetdetailreimbursements trb, string AdminId)
        {
            Timesheetdetailreimbursement data = _context.Timesheetdetailreimbursements.Where(e => e.Timesheetdetailreimbursementid == trb.Timesheetdetailreimbursementid).FirstOrDefault();
            if (data != null)
            {

                data.Modifieddate = DateTime.Now;
                data.Modifiedby = AdminId;
                data.Isdeleted = true;
                _context.Timesheetdetailreimbursements.Update(data);
                _context.SaveChanges();
                return true;
            }
            return false;

        }
        #endregion
        public List<PhysiciansData> GetAllPhysicians()
        {
            var result = new List<PhysiciansData>();
            result = (from py in _context.Physicians
                      select new PhysiciansData
                      {
                          UserName = py.Firstname + " " + py.Lastname,
                          Physicianid = py.Physicianid,
                      }).ToList();
            return result;
        }
        public List<Timesheet> GetPendingTimesheet(int PhysicianId, DateOnly StartDate)
        {
            var result = new List<Timesheet>();
            result = (from timesheet in _context.Timesheets
                      where (timesheet.Isapproved == false && timesheet.Physicianid == PhysicianId && timesheet.Startdate == StartDate)
                      select timesheet).ToList();

            return result;
        }
    }
}
