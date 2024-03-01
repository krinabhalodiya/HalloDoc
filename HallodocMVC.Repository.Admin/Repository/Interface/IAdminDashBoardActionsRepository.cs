using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IAdminDashBoardActionsRepository
    {
        public bool EditCase(ViewCaseData model);
        public ViewCaseData GetRequestForViewCase(int id);
        public Task<bool> AssignProvider(int RequestId, int ProviderId, string notes);
        public bool CancelCase(int RequestID, string Note, string CaseTag);
        public bool BlockCase(int RequestID, string Note);
        public Task<bool> TransferProvider(int RequestId, int ProviderId, string notes);
        public bool ClearCase(int RequestID);
    }
}