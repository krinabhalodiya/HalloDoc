using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
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


    }
}
