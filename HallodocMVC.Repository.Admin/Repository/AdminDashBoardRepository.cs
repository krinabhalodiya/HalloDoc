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

namespace HallodocMVC.Repository.Admin.Repository
{
    public class AdminDashBoardRepository : IAdminDashBoardRepository
    {
        private readonly HalloDocContext _context;

        public AdminDashBoardRepository(HalloDocContext context)
        {
            _context = context;
        }
        public CountStatusWiseRequestModel Indexdata()
        {
            return new CountStatusWiseRequestModel
            {
                NewRequest = _context.Requests.Where(r => r.Status == 1).Count(),
                PendingRequest = _context.Requests.Where(r => r.Status == 2).Count(),
                ActiveRequest = _context.Requests.Where(r => (r.Status == 4 || r.Status == 5)).Count(),
                ConcludeRequest = _context.Requests.Where(r => r.Status == 6).Count(),
                ToCloseRequest = _context.Requests.Where(r => (r.Status == 3 || r.Status == 7 || r.Status == 8)).Count(),
                UnpaidRequest = _context.Requests.Where(r => r.Status == 9).Count()
            };
        }
        public List<AdminDashboardList> GetRequests(string Status) {
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
                                                   where statusdata.Contains(req.Status)
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
                                                       Address = rc.Address + "," + rc.Street + "," + rc.City + "," + rc.State + "," + rc.Zipcode,
                                                       Notes = rc.Notes,
                                                       ProviderID = req.Physicianid,
                                                       RequestorPhoneNumber = req.Phonenumber
                                                   }).ToList();
            return allData;
        }
    }
}
