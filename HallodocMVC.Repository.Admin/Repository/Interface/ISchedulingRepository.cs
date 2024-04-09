using HalloDoc.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface ISchedulingRepository
    {
        public void AddShift(SchedulingModel model, List<string?>? chk, string adminId);
        public void ViewShift(int shiftdetailid);
        public void ViewShiftreturn(SchedulingModel modal);
        public bool EditShiftSave(SchedulingModel modal, string id);
        public bool ViewShiftDelete(SchedulingModel modal, string id);
        public Task<List<PhysiciansData>> PhysicianOnCall(int? region);
        public Task<List<SchedulingModel>> GetAllNotApprovedShift(int? regionId);
        public Task<bool> DeleteShift(string s, string AdminID);
        public Task<bool> UpdateStatusShift(string s, string AdminID);
    }
}
