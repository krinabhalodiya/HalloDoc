using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models.PatientModels;

namespace HallodocMVC.Repository.Patient.Repository.Interface
{
    public interface IPatientDashboard
    {
        public PatientDashList GetPatientRequest(string id, PatientDashList listdata);
        public CreatePatientRequestModel RequestForMe(string userid);
        public Task<bool> CreateRequestForMe(CreatePatientRequestModel patientRequest);
        public Task<bool> CreateRequestForSomeoneElse(CreatePatientRequestModel patientRequest, string userid);
    }
}
