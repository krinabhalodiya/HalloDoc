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
    }
}
