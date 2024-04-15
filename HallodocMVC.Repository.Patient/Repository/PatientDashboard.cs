using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Entity.Models.PatientModels;
using HallodocMVC.Repository.Patient.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static HalloDoc.Entity.Models.Constant;
using static HalloDoc.Entity.Models.ViewDocuments;

namespace HallodocMVC.Repository.Patient.Repository
{
    public class PatientDashboard : IPatientDashboard
    {
        private readonly HalloDocContext _context;

        public PatientDashboard(HalloDocContext context)
        {
            _context = context;
        }
        public PatientDashList GetPatientRequest(string id, PatientDashList listdata)
        {
            List<PatientDashList> allData = _context.Requests.Include(x => x.Requestwisefiles).Where(x => x.Userid == Int32.Parse(id)).Select(x => new PatientDashList
            {
                createdDate = x.Createddate,
                Status = x.Status,
                RequestId = x.Requestid,
                DocumentCount = x.Requestwisefiles.Count()
            }).ToList();
            if (listdata.IsAscending == true)
            {
                allData = allData.OrderBy(x => x.createdDate).ToList();
            }
            else
            {
                allData = allData.OrderByDescending(x => x.createdDate).ToList();
            }

            int totalItemCount = allData.Count();
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)listdata.PageSize);
            List<PatientDashList> list1 = allData.Skip((listdata.CurrentPage - 1) * listdata.PageSize).Take(listdata.PageSize).ToList();
            PatientDashList Data = new PatientDashList
            {
               patientdata = list1,
               CurrentPage = listdata.CurrentPage,
               TotalPages = totalPages,
               PageSize = listdata.PageSize,
               IsAscending = listdata.IsAscending,
               UserId = Int32.Parse(id)
            };
            return Data;
        }
        #region RequestForMe
        public CreatePatientRequestModel RequestForMe(string userid)
        {
            var patientRequest = _context.Users
                               .Where(r => r.Userid == Convert.ToInt32(userid))
                               .Select(r => new CreatePatientRequestModel
                               {
                                   FirstName = r.Firstname,
                                   LastName = r.Lastname,
                                   Email = r.Email,
                                   PhoneNumber = r.Mobile,
                                   DateOfBirth = new DateTime((int)r.Intyear, DateTime.ParseExact(r.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)r.Intdate)
                               })
                               .FirstOrDefault();
            return patientRequest;
        }
        #endregion RequestForMe

        #region CreateRequestForMe
        public async Task<bool> CreateRequestForMe(CreatePatientRequestModel patientRequest)
        {
            var isExist = _context.Users.FirstOrDefault(x => x.Email == patientRequest.Email);
            var request = new Request()
            {
                Isdeleted = new BitArray(1),
                Requesttypeid = 2,
                Userid = isExist.Userid,
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Email = patientRequest.Email,
                Status = 1,
                Phonenumber = patientRequest.PhoneNumber,
                Isurgentemailsent = new BitArray(1),
                Createddate = DateTime.Now
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requestClient = new Requestclient()
            {
                Requestid = request.Requestid,
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Address = patientRequest.Street + " " + patientRequest.City + " " + patientRequest.State + " " + patientRequest.ZipCode,
                Notes = patientRequest.Symptoms,
                Email = patientRequest.Email,
                Strmonth = patientRequest.DateOfBirth.ToString("MMMM"),
                Intdate = patientRequest.DateOfBirth.Day,
                Intyear = patientRequest.DateOfBirth.Year,
                Phonenumber = patientRequest.PhoneNumber,
                Street = patientRequest.Street,
                City = patientRequest.City,
                State = patientRequest.State,
                Zipcode = patientRequest.ZipCode
            };
            _context.Requestclients.Add(requestClient);
            await _context.SaveChangesAsync();


            if (patientRequest.UploadFile != null)
            {
                string upload = FileSave.UploadDoc(patientRequest.UploadFile, request.Requestid);

                var requestwisefile = new Requestwisefile
                {
                    Isdeleted = new BitArray(1),
                    Requestid = request.Requestid,
                    Filename = upload,
                    Createddate = DateTime.Now,
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }
            return true;
        }
        #endregion CreateRequestForMe

        #region CreateRequestForSomeoneElse
        public async Task<bool> CreateRequestForSomeoneElse(CreatePatientRequestModel patientRequest, string userid)
        {
            var request = new Request()
            {
                Isdeleted = new BitArray(1),
                Requesttypeid = 2,
                Userid = Convert.ToInt32(userid),
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Email = patientRequest.Email,
                Status = 1,
                Phonenumber = patientRequest.PhoneNumber,
                Relationname = patientRequest.FF_RelationWithPatient,
                Isurgentemailsent = new BitArray(1),
                Createddate = DateTime.Now
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var Requestclient = new Requestclient()
            {
                Requestid = request.Requestid,
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Address = patientRequest.Street + " " + patientRequest.City + " " + patientRequest.State + " " + patientRequest.ZipCode,
                Email = patientRequest.Email,
                Phonenumber = patientRequest.PhoneNumber,
                Notes = patientRequest.Symptoms,
                Strmonth = patientRequest.DateOfBirth.ToString("MMMM"),
                Intdate = patientRequest.DateOfBirth.Day,
                Intyear = patientRequest.DateOfBirth.Year,
                Street = patientRequest.Street,
                City = patientRequest.City,
                State = patientRequest.State,
                Zipcode = patientRequest.ZipCode
            };
            _context.Requestclients.Add(Requestclient);
            await _context.SaveChangesAsync();


            if (patientRequest.UploadFile != null)
            {
                string upload = FileSave.UploadDoc(patientRequest.UploadFile, request.Requestid);

                var requestwisefile = new Requestwisefile
                {
                    Isdeleted = new BitArray(1),
                    Requestid = request.Requestid,
                    Filename = upload,
                    Createddate = DateTime.Now,
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }
            return true;
        }
        #endregion CreateRequestForSomeoneElse
    }
}
