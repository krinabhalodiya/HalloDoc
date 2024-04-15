using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IRecords
    {
        RecordsModel GetFilteredSearchRecords(RecordsModel rm);
        bool DeleteRequest(int? RequestId);
        RecordsModel GetFilteredPatientHistory(RecordsModel rm);
        Task<PaginatedViewModel> PatientRecord(int UserId, PaginatedViewModel data);
        RecordsModel BlockHistory(RecordsModel rm);
        public bool block(int RequestId, string id);
        bool Unblock(int RequestId, string id);
        public RecordsModel GetFilteredSMSLogs(RecordsModel rm);
        public RecordsModel GetFilteredEmailLogs(RecordsModel rm);
    }
}
