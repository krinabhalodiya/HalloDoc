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
using HalloDoc.Entity.DataModels;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class AdminDashBoardActionsRepository : IAdminDashBoardActionsRepository
    {
        private readonly HalloDocContext _context;

        public AdminDashBoardActionsRepository(HalloDocContext context)
        {
            _context = context;
        }
        public bool EditCase(ViewCaseData model)
        {
            try{
                int monthnum = model.Dob.Month;
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthnum);
                int date = model.Dob.Day;
                int year = model.Dob.Year;
                Requestclient client = _context.Requestclients.FirstOrDefault(E => E.Requestid == model.RequestID);
                if (client != null)
                {
                    client.Firstname = model.FirstName;
                    client.Lastname = model.LastName;
                    client.Phonenumber = model.PhoneNumber;
                    client.Intdate = date;
                    client.Intyear = year;
                    client.Strmonth = monthName;
                    client.Notes = model.PatientNotes;
                    client.Phonenumber = model.PhoneNumber;
                    //client.RegionId = model.RegionId;
                    List<string> location = model.Address.Split(',').ToList();
                    client.Street = location[0];
                    client.City = location[1];
                    client.State = location[2];
                    client.Address = model.Room;
                    _context.Requestclients.Update(client);
                    _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ViewCaseData GetRequestForViewCase(int id)
        {
            var n = _context.Requests.FirstOrDefault(E => E.Requestid == id);

            var l = _context.Requestclients.FirstOrDefault(E => E.Requestid == id);

            var region = _context.Regions.FirstOrDefault(E => E.Regionid == l.Regionid);

            ViewCaseData requestforviewcase = new ViewCaseData
            {
                RequestID = id,
                Region = region.Name,
                FirstName = l.Firstname,
                LastName = l.Lastname,
                PhoneNumber = l.Phonenumber,
                PatientNotes = l.Notes,
                Email = l.Email,
                RequestTypeID = n.Requesttypeid,
                Address = l.Street + "," + l.City + "," + l.State,
                Room = l.Address,
                ConfirmationNumber = n.Confirmationnumber,
                Dob = new DateTime((int)l.Intyear, DateTime.ParseExact(l.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)l.Intdate)
            };
            return requestforviewcase;
        }

        public async Task<bool> AssignProvider(int RequestId, int ProviderId, string notes)
        {

            var request = await _context.Requests.FirstOrDefaultAsync(req => req.Requestid == RequestId);
            request.Physicianid = ProviderId;
            request.Status = 2;
            _context.Requests.Update(request);
            _context.SaveChanges();

            Requeststatuslog rsl = new Requeststatuslog();
            rsl.Requestid = RequestId;
            rsl.Physicianid = ProviderId;
            rsl.Notes = notes;

            rsl.Createddate = DateTime.Now;
            rsl.Status = 2;
            _context.Requeststatuslogs.Update(rsl);
            _context.SaveChanges();

            return true;


        }
    }
}
