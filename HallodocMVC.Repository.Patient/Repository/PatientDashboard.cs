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
                allData = listdata.SortedColumn switch
                {
                    "createdDate" => allData.OrderBy(x => x.createdDate).ToList(),
                    "Status" => allData.OrderBy(x => x.Status).ToList(),
                    _ => allData.OrderBy(x => x.createdDate).ToList()
                };
            }
            else
            {
                allData = listdata.SortedColumn switch
                {
                    "createdDate" => allData.OrderByDescending(x => x.createdDate).ToList(),
                    "Status" => allData.OrderByDescending(x => x.Status).ToList(),
                    _ => allData.OrderByDescending(x => x.createdDate).ToList()
                };
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
               SortedColumn  = listdata.SortedColumn,
               UserId = Int32.Parse(id)
            };
            return Data;
        }
    }
}
