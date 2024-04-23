using System.Collections;
using DocumentFormat.OpenXml.Bibliography;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers.Admin
{
    [CheckProviderAccess("Admin,Provider", "Scheduling")]
    public class SchedulingController : Controller
    {
        private readonly HalloDocContext _context;
        private readonly IComboboxRepository _combobox;
        private readonly ISchedulingRepository _schedulingRepository;
        public SchedulingController(IComboboxRepository comboboxRepository, HalloDocContext context, ISchedulingRepository schedulingRepository)
        {
            _combobox = comboboxRepository;
            _context = context;
            _schedulingRepository = schedulingRepository;
        }
        public async Task<IActionResult> Index(int? region)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.PhysiciansByRegion = new SelectList(Enumerable.Empty<SelectListItem>());
            SchedulingModel modal = new SchedulingModel();
            return View("../Admin/Providers/Scheduling/Index", modal);

        }
        public IActionResult GetPhysicianByRegion(int regionid)
        {
            var PhysiciansByRegion = _combobox.ProviderbyRegion(regionid);

            return Json(PhysiciansByRegion);
        }
        #region LoadSchedulingPartial
        public IActionResult LoadSchedulingPartial(string PartialName, string date, int regionid)
        {
            var currentDate = DateTime.Parse(date);
            List<Physician> physician = _context.Physicianregions.Include(u => u.Physician).Where(u => u.Regionid == regionid).Select(u => u.Physician).ToList();
            if (regionid == 0)
            {
                physician = _context.Physicians.ToList();
            }

            switch (PartialName)
            {
                case "_DayWise":
                    DayWiseScheduling day = new DayWiseScheduling
                    {
                        date = currentDate,
                        physicians = physician,
                        shiftdetails = _context.Shiftdetailregions.Include(u => u.Shiftdetail).ThenInclude(u => u.Shift).Where(u => u.Regionid == regionid && u.Isdeleted == new BitArray(new[] { false })).Select(u => u.Shiftdetail).ToList()
                    };
                    if (regionid == 0)
                    {
                        day.shiftdetails = _context.Shiftdetails.Include(u => u.Shift).Where(u => u.Isdeleted == new BitArray(new[] { false })).ToList();
                    }
                    return PartialView("../Admin/Providers/Scheduling/_DayWise", day);

                case "_WeekWise":
                    WeekWiseScheduling week = new WeekWiseScheduling
                    {
                        date = currentDate,
                        physicians = physician,
                        shiftdetails = _context.Shiftdetailregions.Include(u => u.Shiftdetail).ThenInclude(u => u.Shift).ThenInclude(u => u.Physician).Where(u => u.Isdeleted == new BitArray(new[] { false })).Where(u => u.Regionid == regionid).Select(u => u.Shiftdetail).ToList()
                    };
                    if (regionid == 0)
                    {
                        week.shiftdetails = _context.Shiftdetails.Include(u => u.Shift).ThenInclude(u => u.Physician).Where(u => u.Isdeleted == new BitArray(new[] { false })).ToList();
                    }
                    return PartialView("../Admin/Providers/Scheduling/_WeekWise", week);

                case "_MonthWise":
                    MonthWiseScheduling month = new MonthWiseScheduling
                    {
                        date = currentDate,
                        shiftdetails = _context.Shiftdetailregions.Include(u => u.Shiftdetail).ThenInclude(u => u.Shift).ThenInclude(u => u.Physician).Where(u => u.Isdeleted == new BitArray(new[] { false })).Where(u => u.Regionid == regionid).Select(u => u.Shiftdetail).ToList()
                    };
                    if (regionid == 0)
                    {
                        month.shiftdetails = _context.Shiftdetails.Include(u => u.Shift).ThenInclude(u => u.Physician).Where(u => u.Isdeleted == new BitArray(new[] { false })).ToList();
                    }
                    return PartialView("../Admin/Providers/Scheduling/_MonthWise", month);

                default:
                    return PartialView("../Admin/Providers/Scheduling/_DayWise");
            }
        }
        #endregion

        #region LoadSchedulingPartialforprovider
        public IActionResult LoadSchedulingPartialProivder(string date)
        {
            var currentDate = DateTime.Parse(date);
             MonthWiseScheduling month = new MonthWiseScheduling
             {
                date = currentDate,
                shiftdetails = _context.Shiftdetails.Include(u => u.Shift).Where(u => u.Isdeleted == new BitArray(new[] { false }) && u.Shift.Physicianid==Int32.Parse(CV.UserID())).ToList()
                };
            return PartialView("../Admin/Providers/Scheduling/_MonthWise", month);
        }
        #endregion

        #region AddShift
        public IActionResult AddShift(SchedulingModel model)
        {
            string adminId = CV.ID();
            var chk = Request.Form["repeatdays"].ToList();
            _schedulingRepository.AddShift(model, chk, adminId);
            return RedirectToAction("Index");

        }
        #endregion

        #region viewshift
        public SchedulingModel viewshift(int shiftdetailid)
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
            return modal;
        }
        #endregion

        #region ViewShiftreturn
        public IActionResult ViewShiftreturn(SchedulingModel modal)
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

            return RedirectToAction("Index");
        }
        #endregion

        #region EditShift
        public void EditShiftSave(SchedulingModel modal)
        {
            _schedulingRepository.EditShiftSave(modal, CV.ID());
        }
        #endregion

        #region DeleteShift
        public IActionResult ViewShiftDelete(SchedulingModel modal)
        {
            _schedulingRepository.ViewShiftDelete(modal, CV.ID());
            return RedirectToAction("Index");
        }
        #endregion

        #region Provider_on_call
        public async Task<IActionResult> MDSOnCall(int? regionId)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            List<PhysiciansData> v = await _schedulingRepository.PhysicianOnCall(regionId);
            if (regionId != null)
            {
                return Json(v);
            }
            return View("../Admin/Providers/Scheduling/MDSOnCall", v);
        }
        #endregion
        #region RequestedShift
        public async Task<IActionResult> RequestedShift(int? regionId)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            List<SchedulingModel> v = await _schedulingRepository.GetAllNotApprovedShift(regionId);
            
            return View("../Admin/Providers/Scheduling/ShiftForReview", v);
        }
        #endregion

        #region _ApprovedShifts

        public async Task<IActionResult> _ApprovedShifts(string shiftids)
        {
            if (await _schedulingRepository.UpdateStatusShift(shiftids, CV.ID()))
            {
                TempData["Status"] = "Approved Shifts Successfully..!";
            }


            return RedirectToAction("RequestedShift");
        }
        #endregion

        #region _DeleteShifts

        public async Task<IActionResult> _DeleteShifts(string shiftids)
        {
            if (await _schedulingRepository.DeleteShift(shiftids, CV.ID()))
            {
                TempData["Status"] = "Delete Shifts Successfully..!";
            }

            return RedirectToAction("RequestedShift");
        }
        #endregion
    }
}
