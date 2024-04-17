using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using Microsoft.AspNetCore.Http;

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
        public ViewNotesData getNotesByID(int id);
        public bool EditViewNotes(string? adminnotes, string? physiciannotes, int RequestID);
        public Task<ViewDocuments> GetDocumentByRequest(int? id, ViewDocuments viewDocument);
        public Boolean SaveDoc(int Requestid, IFormFile file);
        public Task<bool> DeleteDocumentByRequest(string ids);
        public Task<bool> SendFileEmail(string ids, int Requestid, string email);
        public Healthprofessional SelectProfessionlByID(int VendorID);
        public bool SendOrder(ViewSendOrderData data);
        public Boolean SendAgreement(int requestid);
        public Boolean SendAgreement_accept(int RequestID);
        public Boolean SendAgreement_Reject(int RequestID, string Notes);
        public ViewCloseCaseModel CloseCaseData(int RequestID);
        public bool EditForCloseCase(ViewCloseCaseModel model);
        public bool CloseCase(int RequestID);
        public ViewEncounterData GetEncounterDetails(int RequestID);
        public bool EditEncounterDetails(ViewEncounterData Data, string id);
        public Task<bool> AcceptPhysician(int requestid, string note, int ProviderId);
        public Task<bool> TransfertoAdmin(int RequestID, string Note, int ProviderId);
        public bool ContactAdmin(int ProviderId, string Notes);
    }
}