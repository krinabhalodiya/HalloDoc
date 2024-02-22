using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models;
using HalloDoc.Entity.DataContext;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly HalloDocContext _context;

        public RequestRepository(HalloDocContext context)
        {
            _context = context;
        }
        public List<AdminDashboardList> NewRequestData()
        {
            var list = _context.Requests.Join
                        (_context.Requestclients,
                        requestclients => requestclients.Requestid, requests => requests.Requestid,
                        (requests, requestclients) => new { Request = requests, Requestclient = requestclients }
                        )
                        .Where(req => req.Request.Status == 1)
                        .Select(req => new AdminDashboardList()
                        {
                            RequestId = req.Request.Requestid,
                            PatientName = req.Requestclient.Firstname + " " + req.Requestclient.Lastname,
                            Email = req.Requestclient.Email,
                            //DateOfBirth = new DateTime((int)req.Requestclient.Intyear, Convert.ToInt32(req.Requestclient.Strmonth.Trim()), (int)req.Requestclient.Intdate),
                            RequestTypeId = req.Request.Requesttypeid,
                            Requestor = req.Request.Firstname + " " + req.Request.Lastname,
                            RequestedDate = req.Request.Createddate,
                            PatientPhoneNumber = req.Requestclient.Phonenumber,
                            RequestorPhoneNumber = req.Request.Phonenumber,
                            Notes = req.Requestclient.Notes,
                            Address = req.Requestclient.Address + " " + req.Requestclient.Street + " " + req.Requestclient.City + " " + req.Requestclient.State + " " + req.Requestclient.Zipcode
                        })
                        .OrderByDescending(req => req.RequestedDate)
                        .ToList();
            return list;
        }
    }
}
