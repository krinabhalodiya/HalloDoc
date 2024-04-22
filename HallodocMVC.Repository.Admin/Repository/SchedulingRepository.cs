using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class SchedulingRepository : ISchedulingRepository
    {
        private readonly HalloDocContext _context;

        public SchedulingRepository(HalloDocContext context)
        {
            _context = context;
        }
        #region Scheduling
        public void AddShift(SchedulingModel model, List<string?>? chk, string adminId)
        {
            var shiftid = _context.Shifts.Where(u => u.Physicianid == model.physicianid).Select(u => u.Shiftid).ToList();
            if (shiftid.Count() > 0)
            {
                foreach (var obj in shiftid)
                {
                    var shiftdetailchk = _context.Shiftdetails.Where(u => u.Shiftid == obj && u.Shiftdate == model.shiftdate).ToList();
                    if (shiftdetailchk.Count() > 0)
                    {
                        foreach (var item in shiftdetailchk)
                        {
                            if ((model.starttime >= item.Starttime && model.starttime <= item.Endtime) || (model.endtime >= item.Starttime && model.endtime <= item.Endtime))
                            {
                                //TempData["error"] = "Shift is already assigned in this time";
                                return;
                            }
                        }
                    }
                }
            }
            Shift shift = new Shift
            {
                Physicianid = model.physicianid,
                Startdate = DateOnly.FromDateTime(model.shiftdate),
                Repeatupto = model.repeatcount,
                Createddate = DateTime.Now,
                Createdby = adminId,
            };
            foreach (var obj in chk)
            {
                shift.Weekdays += obj;
            }
            if (model.repeatcount > 0)
            {
                shift.Isrepeat = new BitArray(new[] { true });
            }
            else
            {
                shift.Isrepeat = new BitArray(new[] { false });
            }
            _context.Shifts.Add(shift);
            _context.SaveChanges();

            DateTime curdate = model.shiftdate;
            Shiftdetail shiftdetail = new Shiftdetail();
            shiftdetail.Shiftid = shift.Shiftid;
            shiftdetail.Shiftdate = curdate;
            shiftdetail.Regionid = model.regionid;
            shiftdetail.Starttime = model.starttime;
            shiftdetail.Endtime = model.endtime;
            shiftdetail.Isdeleted = new BitArray(new[] { false });
            _context.Shiftdetails.Add(shiftdetail);
            _context.SaveChanges();
            Shiftdetailregion shiftregionnews = new Shiftdetailregion
            {
                Shiftdetailid = shiftdetail.Shiftdetailid,
                Regionid = model.regionid,
                Isdeleted = new BitArray(new[] { false })
            };
            _context.Shiftdetailregions.Add(shiftregionnews);
            _context.SaveChanges();
            var dayofweek = model.shiftdate.DayOfWeek.ToString();
            int valueforweek;
            if (dayofweek == "Sunday")
            {
                valueforweek = 0;
            }
            else if (dayofweek == "Monday")
            {
                valueforweek = 1;
            }
            else if (dayofweek == "Tuesday")
            {
                valueforweek = 2;
            }
            else if (dayofweek == "Wednesday")
            {
                valueforweek = 3;
            }
            else if (dayofweek == "Thursday")
            {
                valueforweek = 4;
            }
            else if (dayofweek == "Friday")
            {
                valueforweek = 5;
            }
            else
            {
                valueforweek = 6;
            }

            if (shift.Isrepeat[0] == true)
            {
                for (int j = 0; j < shift.Weekdays.Count(); j++)
                {
                    var z = shift.Weekdays;
                    var p = shift.Weekdays.ElementAt(j).ToString();
                    int ele = Int32.Parse(p);
                    int x;
                    if (valueforweek > ele)
                    {
                        x = 6 - valueforweek + 1 + ele;
                    }
                    else
                    {
                        x = ele - valueforweek;
                    }
                    if (x == 0)
                    {
                        x = 7;
                    }
                    DateTime newcurdate = model.shiftdate.AddDays(x);
                    for (int i = 0; i < model.repeatcount; i++)
                    {
                        Shiftdetail shiftdetailnew = new Shiftdetail
                        {
                            Shiftid = shift.Shiftid,
                            Shiftdate = newcurdate,
                            Regionid = model.regionid,
                            Starttime = model.starttime,
                            Endtime = model.endtime,

                            Isdeleted = new BitArray(new[] { false })
                        };
                        _context.Shiftdetails.Add(shiftdetailnew);
                        _context.SaveChanges();
                        Shiftdetailregion shiftregionnew = new Shiftdetailregion
                        {
                            Shiftdetailid = shiftdetailnew.Shiftdetailid,
                            Regionid = model.regionid,
                            Isdeleted = new BitArray(new[] { false })
                        };
                        _context.Shiftdetailregions.Add(shiftregionnew);
                        _context.SaveChanges();
                        newcurdate = newcurdate.AddDays(7);
                    }
                }
            }
        }
        public void ViewShift(int shiftdetailid)
        {
            SchedulingModel modal = new SchedulingModel();
            var shiftdetail = _context.Shiftdetails.FirstOrDefault(u => u.Shiftdetailid == shiftdetailid);
            if (shiftdetail != null)
            {
                _context.Entry(shiftdetail)
                    .Reference(s => s.Shift)
                    .Query()
                    .Include(s => s.Physician)
                    .Load();
            }
            modal.regionid = (int)shiftdetail.Regionid;
            modal.physicianname = shiftdetail.Shift.Physician.Firstname + " " + shiftdetail.Shift.Physician.Lastname;
            modal.modaldate = shiftdetail.Shiftdate.ToString("yyyy-MM-dd");
            modal.starttime = shiftdetail.Starttime;
            modal.endtime = shiftdetail.Endtime;
            modal.shiftdetailid = shiftdetailid;
        }
        public void ViewShiftreturn(SchedulingModel modal)
        {
            var shiftdetail = _context.Shiftdetails.FirstOrDefault(u => u.Shiftdetailid == modal.shiftdetailid);
            if (shiftdetail.Status == 0)
            {
                shiftdetail.Status = 1;
            }
            else
            {
                shiftdetail.Status = 0;
            }
            _context.Shiftdetails.Update(shiftdetail);
            _context.SaveChanges();
        }
        #endregion

        #region EditShiftSave
        public bool EditShiftSave(SchedulingModel modal, string id)
        {
            var shiftdetail = _context.Shiftdetails.FirstOrDefault(u => u.Shiftdetailid == modal.shiftdetailid);
            if (shiftdetail != null)
            {
                shiftdetail.Shiftdate = modal.shiftdate;
                shiftdetail.Starttime = modal.starttime;
                shiftdetail.Endtime = modal.endtime;
                shiftdetail.Modifiedby = id;
                shiftdetail.Modifieddate = DateTime.Now;
                _context.Shiftdetails.Update(shiftdetail);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region DeleteShiftSave
        public bool ViewShiftDelete(SchedulingModel modal, string id)
        {
            var shiftdetail = _context.Shiftdetails.FirstOrDefault(u => u.Shiftdetailid == modal.shiftdetailid);
            var shiftdetailRegion = _context.Shiftdetailregions.FirstOrDefault(u => u.Shiftdetailid == modal.shiftdetailid);
            string adminname = id;
            shiftdetail.Isdeleted = new BitArray(new[] { true });
            shiftdetail.Modifieddate = DateTime.Now;
            shiftdetail.Modifiedby = adminname;
            _context.Shiftdetails.Update(shiftdetail);
            _context.SaveChanges();
            shiftdetailRegion.Isdeleted = new BitArray(new[] { true });
            _context.Shiftdetailregions.Update(shiftdetailRegion);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region PhysicianOnCall
        public async Task<List<PhysiciansData>> PhysicianOnCall(int? region)
        {
            DateTime currentDateTime = DateTime.Now;
            TimeOnly currentTimeOfDay = TimeOnly.FromDateTime(DateTime.Now);
            List<PhysiciansData> pl = await (from r in _context.Physicians
                                         where r.Isdeleted == new BitArray(1)
                                         select new PhysiciansData
                                         {
                                             Createddate = r.Createddate,
                                             Physicianid = r.Physicianid,
                                             Address1 = r.Address1,
                                             Address2 = r.Address2,
                                             Adminnotes = r.Adminnotes,
                                             Altphone = r.Altphone,
                                             Businessname = r.Businessname,
                                             Businesswebsite = r.Businesswebsite,
                                             City = r.City,
                                             Firstname = r.Firstname,
                                             Lastname = r.Lastname,
                                             Status = r.Status,
                                             Email = r.Email,
                                             Photo = r.Photo

                                         }).ToListAsync();
            if (region != null)
            {
                pl = await (
                                        from pr in _context.Physicianregions

                                        join ph in _context.Physicians
                                         on pr.Physicianid equals ph.Physicianid into rGroup
                                        from r in rGroup.DefaultIfEmpty()
                                        where pr.Regionid == region && r.Isdeleted == new BitArray(1)
                                        select new PhysiciansData
                                        {
                                            Createddate = r.Createddate,
                                            Physicianid = r.Physicianid,
                                            Address1 = r.Address1,
                                            Address2 = r.Address2,
                                            Adminnotes = r.Adminnotes,
                                            Altphone = r.Altphone,
                                            Businessname = r.Businessname,
                                            Businesswebsite = r.Businesswebsite,
                                            City = r.City,
                                            Firstname = r.Firstname,
                                            Lastname = r.Lastname,
                                            Status = r.Status,
                                            Email = r.Email,
                                            Photo = r.Photo

                                        })
                                        .ToListAsync();
            }
            foreach (var item in pl)
            {
                List<int> shiftIds = await (from s in _context.Shifts
                                            where s.Physicianid == item.Physicianid
                                            select s.Shiftid).ToListAsync();
                foreach (var shift in shiftIds)
                {
                    var shiftDetail = (from sd in _context.Shiftdetails
                                       where sd.Shiftid == shift &&
                                             sd.Shiftdate.Date == currentDateTime.Date &&
                                             sd.Starttime <= currentTimeOfDay &&
                                             currentTimeOfDay <= sd.Endtime
                                       select sd).FirstOrDefault();
                    if (shiftDetail != null)
                    {
                        item.onCallStatus = 1;
                    }
                }
            }
            return pl;
        }
        #endregion

        #region GetAllNotApprovedShift
        public async Task<List<SchedulingModel>> GetAllNotApprovedShift(int? regionId)
        {

            List<SchedulingModel> ss = await (from s in _context.Shifts
                                       join pd in _context.Physicians
                                       on s.Physicianid equals pd.Physicianid
                                       join sd in _context.Shiftdetails
                                       on s.Shiftid equals sd.Shiftid into shiftGroup
                                       from sd in shiftGroup.DefaultIfEmpty()
                                       join rg in _context.Regions
                                       on sd.Regionid equals rg.Regionid
                                       where (regionId == null || regionId == -1 || sd.Regionid == regionId) && sd.Status == 0 && sd.Isdeleted == new BitArray(1)
                                       select new SchedulingModel
                                       {
                                           regionid = (int)sd.Regionid,
                                           RegionName = rg.Name,
                                           shiftdetailid = sd.Shiftdetailid,
                                           status = sd.Status,
                                           starttime = sd.Starttime,
                                           endtime = sd.Endtime,
                                           physicianid = s.Physicianid,
                                           physicianname = pd.Firstname + ' ' + pd.Lastname,
                                           shiftdate = sd.Shiftdate
                                       })
                                .ToListAsync();
            return ss;
        }
        #endregion

        #region DeleteShift
        public async Task<bool> DeleteShift(string s, string AdminID)
        {
            List<int> shidtID = s.Split(',').Select(int.Parse).ToList();
            try
            {
                foreach (int i in shidtID)
                {
                    Shiftdetail sd = _context.Shiftdetails.FirstOrDefault(sd => sd.Shiftdetailid == i);
                    if (sd != null)
                    {
                        sd.Isdeleted[0] = true;
                        sd.Modifiedby = AdminID;
                        sd.Modifieddate = DateTime.Now;
                        _context.Shiftdetails.Update(sd);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region UpdateStatusShift
        public async Task<bool> UpdateStatusShift(string s, string AdminID)
        {
            List<int> shidtID = s.Split(',').Select(int.Parse).ToList();
            try
            {
                foreach (int i in shidtID)
                {
                    Shiftdetail sd = _context.Shiftdetails.FirstOrDefault(sd => sd.Shiftdetailid == i);
                    if (sd != null)
                    {
                        sd.Status = (short)(sd.Status == 1 ? 0 : 1);
                        sd.Modifiedby = AdminID;
                        sd.Modifieddate = DateTime.Now;
                        _context.Shiftdetails.Update(sd);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
