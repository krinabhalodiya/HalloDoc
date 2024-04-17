using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using Org.BouncyCastle.Ocsp;
using System.Linq.Expressions;
using HalloDoc.Entity.Models;
using Twilio.TwiML.Messaging;
using Twilio.TwiML.Voice;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class AdminDashBoardRepository : IAdminDashBoardRepository
    {
        private readonly HalloDocContext _context;
        private readonly EmailConfiguration _emailConfig;
        public AdminDashBoardRepository(HalloDocContext context, EmailConfiguration emailConfiguration)
        {
            _context = context;
            _emailConfig = emailConfiguration;
        }
        public PaginatedViewModel Indexdata(int ProviderId)
        {
            if (ProviderId < 0)
            {
                return new PaginatedViewModel
                {
                    NewRequest = _context.Requests.Where(r => r.Status == 1 && r.Isdeleted == new BitArray(1)).Count(),
                    PendingRequest = _context.Requests.Where(r => r.Status == 2 && r.Isdeleted == new BitArray(1)).Count(),
                    ActiveRequest = _context.Requests.Where((r => (r.Status == 4 || r.Status == 5) && r.Isdeleted == new BitArray(1))).Count(),
                    ConcludeRequest = _context.Requests.Where(r => r.Status == 6 && r.Isdeleted == new BitArray(1)).Count(),
                    ToCloseRequest = _context.Requests.Where((r => (r.Status == 3 || r.Status == 7 || r.Status == 8) && r.Isdeleted == new BitArray(1))).Count(),
                    UnpaidRequest = _context.Requests.Where(r => r.Status == 9 && r.Isdeleted == new BitArray(1)).Count()
                };
            }
            return new PaginatedViewModel
            {
                NewRequest = _context.Requests.Where(r => r.Status == 1 && r.Physicianid == ProviderId && r.Isdeleted == new BitArray(1)).Count(),
                PendingRequest = _context.Requests.Where(r => r.Status == 2 && r.Physicianid == ProviderId && r.Isdeleted == new BitArray(1)).Count(),
                ActiveRequest = _context.Requests.Where((r => (r.Status == 4 || r.Status == 5) && r.Physicianid == ProviderId && r.Isdeleted == new BitArray(1))).Count(),
                ConcludeRequest = _context.Requests.Where(r => r.Status == 6 && r.Physicianid == ProviderId && r.Isdeleted == new BitArray(1)).Count(),
                ToCloseRequest = _context.Requests.Where((r => (r.Status == 3 || r.Status == 7 || r.Status == 8) && r.Physicianid == ProviderId && r.Isdeleted == new BitArray(1))).Count(),
                UnpaidRequest = _context.Requests.Where(r => r.Status == 9 && r.Physicianid == ProviderId && r.Isdeleted == new BitArray(1)).Count()
            };
        }
        #region GetRequestsforAdmin
        public PaginatedViewModel GetRequests(string Status, PaginatedViewModel data) {
            if(data.SearchInput != null)
            {
                data.SearchInput = data.SearchInput.Trim();
            }
            List<int> statusdata = Status.Split(',').Select(int.Parse).ToList();
            List<AdminDashboardList> allData = (from req in _context.Requests
                                                join reqClient in _context.Requestclients
                                                on req.Requestid equals reqClient.Requestid into reqClientGroup
                                                from rc in reqClientGroup.DefaultIfEmpty()
                                                join phys in _context.Physicians
                                                on req.Physicianid equals phys.Physicianid into physGroup
                                                from p in physGroup.DefaultIfEmpty()
                                                join reg in _context.Regions
                                                on rc.Regionid equals reg.Regionid into RegGroup
                                                from rg in RegGroup.DefaultIfEmpty()
                                                where statusdata.Contains(req.Status) && (req.Isdeleted == new BitArray(1)) && (data.SearchInput == null ||
                                                         rc.Firstname.Contains(data.SearchInput) || rc.Lastname.Contains(data.SearchInput) ||
                                                         req.Firstname.Contains(data.SearchInput) || req.Lastname.Contains(data.SearchInput) ||
                                                         rc.Email.Contains(data.SearchInput) || rc.Phonenumber.Contains(data.SearchInput) ||
                                                         rc.Address.Contains(data.SearchInput) || rc.Notes.Contains(data.SearchInput) ||
                                                         p.Firstname.Contains(data.SearchInput) || p.Lastname.Contains(data.SearchInput) ||
                                                         rg.Name.Contains(data.SearchInput)) && (data.RegionId == null || rc.Regionid == data.RegionId) 
                                                         && (data.RequestType == null || req.Requesttypeid == data.RequestType)
                                                   
                                                   select new AdminDashboardList
                                                   {
                                                       RequestID = req.Requestid,
                                                       RequestTypeID = req.Requesttypeid,
                                                       Requestor = req.Firstname + " " + req.Lastname,
                                                       PatientName = rc.Firstname + " " + rc.Lastname,
                                                       Dob = new DateOnly((int)rc.Intyear, DateTime.ParseExact(rc.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)rc.Intdate),
                                                       RequestedDate = req.Createddate,
                                                       Email = rc.Email,
                                                       Region = rg.Name,
                                                       ProviderName = p.Firstname + " " + p.Lastname,
                                                       PhoneNumber = rc.Phonenumber,
                                                       Address = rc.Address,
                                                       Notes = rc.Notes,
                                                       ProviderID = req.Physicianid,
                                                       RequestorPhoneNumber = req.Phonenumber
                                                   }).ToList();


            if (data.IsAscending == true)
            {
                allData = data.SortedColumn switch
                {
                    "PatientName" => allData.OrderBy(x => x.PatientName).ToList(),
                    "Requestor" => allData.OrderBy(x => x.Requestor).ToList(),
                    "Dob" => allData.OrderBy(x => x.Dob).ToList(),
                    "Address" => allData.OrderBy(x => x.Address).ToList(),
                    "RequestedDate" => allData.OrderBy(x => x.RequestedDate).ToList(),
                    "ProviderName" => allData.OrderBy(x => x.ProviderName).ToList(),
                    "Region" => allData.OrderBy(x => x.Region).ToList(),
                    _ => allData.OrderBy(x => x.RequestedDate).ToList()
                };
            }
            else
            {
                allData = data.SortedColumn switch
                {
                    "PatientName" => allData.OrderByDescending(x => x.PatientName).ToList(),
                    "Requestor" => allData.OrderByDescending(x => x.Requestor).ToList(),
                    "Dob" => allData.OrderByDescending(x => x.Dob).ToList(),
                    "Address" => allData.OrderByDescending(x => x.Address).ToList(),
                    "RequestedDate" => allData.OrderByDescending(x => x.RequestedDate).ToList(),
                    "ProviderName" => allData.OrderByDescending(x => x.ProviderName).ToList(),
                    "Region" => allData.OrderByDescending(x => x.Region).ToList(),
                    _ => allData.OrderByDescending(x => x.RequestedDate).ToList()
                };
            }
            
            int totalItemCount = allData.Count();
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)data.PageSize);
            List<AdminDashboardList> list1 = allData.Skip((data.CurrentPage - 1) * data.PageSize).Take(data.PageSize).ToList();
            PaginatedViewModel paginatedViewModel = new PaginatedViewModel
            {
                adl = list1,
                CurrentPage = data.CurrentPage,
                TotalPages = totalPages,
                PageSize = data.PageSize,
                SearchInput = data.SearchInput,
                SortedColumn = data.SortedColumn,
                IsAscending = data.IsAscending
            };
            return paginatedViewModel;
        }
        #endregion
        #region GetRequestsforProvider
        public PaginatedViewModel GetRequests(string Status, PaginatedViewModel data, int ProviderId)
        {
            if (data.SearchInput != null)
            {
                data.SearchInput = data.SearchInput.Trim();
            }
            List<int> statusdata = Status.Split(',').Select(int.Parse).ToList();
            List<AdminDashboardList> allData = (from req in _context.Requests
                                                join reqClient in _context.Requestclients
                                                on req.Requestid equals reqClient.Requestid into reqClientGroup
                                                from rc in reqClientGroup.DefaultIfEmpty()
                                                join phys in _context.Physicians
                                                on req.Physicianid equals phys.Physicianid into physGroup
                                                from p in physGroup.DefaultIfEmpty()
                                                join reg in _context.Regions
                                                on rc.Regionid equals reg.Regionid into RegGroup
                                                from rg in RegGroup.DefaultIfEmpty()
                                                where statusdata.Contains(req.Status) && (req.Isdeleted == new BitArray(1)) && (data.SearchInput == null ||
                                                         rc.Firstname.Contains(data.SearchInput) || rc.Lastname.Contains(data.SearchInput) ||
                                                         req.Firstname.Contains(data.SearchInput) || req.Lastname.Contains(data.SearchInput) ||
                                                         rc.Email.Contains(data.SearchInput) || rc.Phonenumber.Contains(data.SearchInput) ||
                                                         rc.Address.Contains(data.SearchInput) || rc.Notes.Contains(data.SearchInput) ||
                                                         p.Firstname.Contains(data.SearchInput) || p.Lastname.Contains(data.SearchInput) ||
                                                         rg.Name.Contains(data.SearchInput)) && (data.RegionId == null || rc.Regionid == data.RegionId)
                                                         && (data.RequestType == null || req.Requesttypeid == data.RequestType) && req.Physicianid == ProviderId

                                                select new AdminDashboardList
                                                {
                                                    RequestID = req.Requestid,
                                                    RequestTypeID = req.Requesttypeid,
                                                    Requestor = req.Firstname + " " + req.Lastname,
                                                    PatientName = rc.Firstname + " " + rc.Lastname,
                                                    Dob = new DateOnly((int)rc.Intyear, DateTime.ParseExact(rc.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)rc.Intdate),
                                                    RequestedDate = req.Createddate,
                                                    Email = rc.Email,
                                                    Region = rg.Name,
                                                    ProviderName = p.Firstname + " " + p.Lastname,
                                                    PhoneNumber = rc.Phonenumber,
                                                    Address = rc.Address,
                                                    Notes = rc.Notes,
                                                    ProviderID = req.Physicianid,
                                                    RequestorPhoneNumber = req.Phonenumber,
                                                    providerencounterstatus = req.Status,
                                                    IsFinalize = _context.Encounterforms.Any(ef => ef.Requestid == req.Requestid && ef.Isfinalize),
                                                }).ToList();


            if (data.IsAscending == true)
            {
                allData = data.SortedColumn switch
                {
                    "PatientName" => allData.OrderBy(x => x.PatientName).ToList(),
                    "Address" => allData.OrderBy(x => x.Address).ToList(),
                    _ => allData.OrderBy(x => x.PatientName).ToList()
                };
            }
            else
            {
                allData = data.SortedColumn switch
                {
                    "PatientName" => allData.OrderByDescending(x => x.PatientName).ToList(),
                    "Address" => allData.OrderByDescending(x => x.Address).ToList(),
                    _ => allData.OrderByDescending(x => x.PatientName).ToList()
                };
            }
            
            int totalItemCount = allData.Count();
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)data.PageSize);
            List<AdminDashboardList> list1 = allData.Skip((data.CurrentPage - 1) * data.PageSize).Take(data.PageSize).ToList();
            PaginatedViewModel paginatedViewModel = new PaginatedViewModel
            {
                adl = list1,
                CurrentPage = data.CurrentPage,
                TotalPages = totalPages,
                PageSize = data.PageSize,
                SearchInput = data.SearchInput,
                SortedColumn = data.SortedColumn,
                IsAscending = data.IsAscending
            };
            return paginatedViewModel;
        }
        #endregion
        #region Export
        public List<AdminDashboardList> Export(string status)
        {
            List<int> statusdata = status.Split(',').Select(int.Parse).ToList();
            List<AdminDashboardList> allData = (from req in _context.Requests
                                                join reqClient in _context.Requestclients
                                                on req.Requestid equals reqClient.Requestid into reqClientGroup
                                                from rc in reqClientGroup.DefaultIfEmpty()
                                                join phys in _context.Physicians
                                                on req.Physicianid equals phys.Physicianid into physGroup
                                                from p in physGroup.DefaultIfEmpty()
                                                join reg in _context.Regions
                                                on rc.Regionid equals reg.Regionid into RegGroup
                                                from rg in RegGroup.DefaultIfEmpty()
                                                where statusdata.Contains((int)req.Status)
                                                orderby req.Createddate descending
                                                select new AdminDashboardList
                                                {
                                                    RequestID = req.Requestid,
                                                    RequestTypeID = req.Requesttypeid,
                                                    Requestor = req.Firstname + " " + req.Lastname,
                                                    PatientName = rc.Firstname + " " + rc.Lastname,
                                                    Dob = new DateOnly((int)rc.Intyear, DateTime.ParseExact(rc.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)rc.Intdate),
                                                    RequestedDate = req.Createddate,
                                                    Email = rc.Email,
                                                    Region = rg.Name,
                                                    ProviderName = p.Firstname + " " + p.Lastname,
                                                    PhoneNumber = rc.Phonenumber,
                                                    Address = rc.Address,
                                                    Notes = rc.Notes,
                                                    ProviderID = req.Physicianid,
                                                    RequestorPhoneNumber = req.Phonenumber
                                                }).ToList();
            return allData;
        }
        #endregion
        #region SendLink
        public bool SendLink(String Email)
        {
            var agreementUrl = "https://localhost:44306/SubmitRequest/Index";
            _emailConfig.SendMail(Email,"ResubmitRequest", $"<a href='{agreementUrl}'>Agree/Disagree</a>");
            return true;
        }
        #endregion
        #region SendLink
        public bool SendSMS(String mobile)
        {
            var agreementUrl = "https://localhost:44306/SubmitRequest/Index";
            _emailConfig.SendSMS(mobile, $"<a href='{agreementUrl}'>Agree/Disagree</a>");
            return true;
        }
        #endregion
    }
}
