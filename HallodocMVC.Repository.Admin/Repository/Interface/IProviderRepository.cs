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
        public Task<PhysiciansData> GetPhysicianById(int id);
        public Task<bool> EditAccountInfo(PhysiciansData vm);
        public Task<bool> ChangePasswordAsync(string password, int Physicianid);
        public Task<bool> EditPhysicianInfo(PhysiciansData vm, string aspnetid);
        public Task<bool> EditMailBillingInfo(PhysiciansData vm, string AdminId);
        public Task<bool> EditProviderProfile(PhysiciansData vm, string AdminId);
        public Task<bool> EditProviderOnbording(PhysiciansData vm, string AdminId);
        public Task<bool> DeletePhysician(int PhysicianID, string AdminID);
        public Task<List<ProviderPayrateModel>> GetPayrateDetails(int PhysicianId);
        public Task<bool> EditPayrate(int ProviderPayrateId, decimal Payrate, string id);
    }
}
