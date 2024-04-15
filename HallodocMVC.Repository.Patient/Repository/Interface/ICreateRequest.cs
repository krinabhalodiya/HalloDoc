using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models.PatientModels;

namespace HallodocMVC.Repository.Patient.Repository.Interface
{
    public interface ICreateRequest
    {
        Task<bool> PatientRequest(CreatePatientRequestModel createPatientRequest);
        Task<bool> FamilyFriendRequest(CreateFamilyFriendRequestModel createFamilyFriendRequest);
        Task<bool> ConciergeRequest(CreateConciergeRequestModel createConciergeRequest);
        Task<bool> BusinessRequest(CreateBusinessRequestModel createBusinessRequest);
    }
}
