using HalloDoc.Entity.DataModels;
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
        public List<Physician> PhysicianAll();
        public DayWiseScheduling Daywise(int regionid, DateTime currentDate);
        public WeekWiseScheduling Weekwise(int regionid, DateTime currentDate);
        public MonthWiseScheduling Monthwise(int regionid, DateTime currentDate);
        public MonthWiseScheduling MonthwisePhysician(DateTime currentDate, int id);
        public void AddShift(SchedulingModel model, List<string?>? chk, string adminId);
        public SchedulingModel ViewShift(int shiftdetailid, SchedulingModel modal);
        public void ViewShiftreturn(SchedulingModel modal);
        public bool EditShiftSave(SchedulingModel modal, string id);
        public bool ViewShiftDelete(SchedulingModel modal, string id);
        public Task<List<PhysiciansData>> PhysicianOnCall(int? region);
        public Task<List<SchedulingModel>> GetAllNotApprovedShift(int? regionId);
        public Task<bool> DeleteShift(string s, string AdminID);
        public Task<bool> UpdateStatusShift(string s, string AdminID);
    }
}
