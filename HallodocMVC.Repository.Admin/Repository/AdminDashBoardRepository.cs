using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models;
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

namespace HallodocMVC.Repository.Admin.Repository
{
    public class AdminDashBoardRepository : IAdminDashBoardRepository
    {
        private readonly HalloDocContext _context;

        public AdminDashBoardRepository(HalloDocContext context)
        {
            _context = context;
        }
        public PaginatedViewModel Indexdata()
        {
            return new PaginatedViewModel
            {
                NewRequest = _context.Requests.Where(r => r.Status == 1).Count(),
                PendingRequest = _context.Requests.Where(r => r.Status == 2).Count(),
                ActiveRequest = _context.Requests.Where(r => (r.Status == 4 || r.Status == 5)).Count(),
                ConcludeRequest = _context.Requests.Where(r => r.Status == 6).Count(),
                ToCloseRequest = _context.Requests.Where(r => (r.Status == 3 || r.Status == 7 || r.Status == 8)).Count(),
                UnpaidRequest = _context.Requests.Where(r => r.Status == 9).Count()
            };
        }
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
                                                where statusdata.Contains(req.Status) && (data.SearchInput == null ||
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

    //465464645
            if (data.IsAscending == true)
            {
                allData = data.SortedColumn switch
                {
                    "PatientName" => allData.OrderBy(x => x.PatientName).ToList(),
                    "Requestor" => allData.OrderBy(x => x.Requestor).ToList(),
                    "Dob" => allData.OrderBy(x => x.Dob).ToList(),
                    "Address" => allData.OrderBy(x => x.Address).ToList(),
                    "RequestedDate" => allData.OrderBy(x => x.RequestedDate).ToList(),
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
    }
}
