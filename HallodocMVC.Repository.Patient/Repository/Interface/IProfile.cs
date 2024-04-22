using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models.PatientModels;

namespace HallodocMVC.Repository.Patient.Repository.Interface
{
    public interface IProfile
    {
        public ViewDataUserProfile? ProviderProfile(string id);
        public Task<bool> Edit(ViewDataUserProfile userprofile, string id);
    }
}
