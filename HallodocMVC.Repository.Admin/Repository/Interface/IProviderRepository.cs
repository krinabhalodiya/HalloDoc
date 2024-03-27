using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IProviderRepository
    {
        public Task<List<PhysicianLocation>> FindPhysicianLocation();
        public Task<List<PhysiciansData>> PhysicianAll();
        public Task<List<PhysiciansData>> PhysicianByRegion(int? region);
        public Task<bool> ChangeNotificationPhysician(Dictionary<int, bool> changedValuesDict);
        public Task<bool> PhysicianAddEdit(PhysiciansData physiciandata, string AdminId);

    }
}
