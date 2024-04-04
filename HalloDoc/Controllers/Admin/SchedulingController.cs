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
            return View("../Admin/Scheduling/Index", modal);

        }
        public IActionResult GetPhysicianByRegion(int regionid)
        {
            var PhysiciansByRegion = _combobox.ProviderbyRegion(regionid);

            return Json(PhysiciansByRegion);
        }
        #region
        //public IActionResult Scheduling()
        //{
        //    ViewBag.Adminname = HttpContext.Session.GetString("Adminname");
        //    ViewBag.RegionCombobox = _adminFunction.RegionComboBox();
        //    ViewBag.PhysiciansByRegion = new SelectList(Enumerable.Empty<SelectListItem>());
        //    SchedulingModel modal = new SchedulingModel();
        //    modal.regions = _context.Regions.ToList();
        //    return View(modal);
        //}

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
                        shiftdetails = _context.Shiftdetails.Include(u => u.Shift).ToList()
                    };
                    return PartialView("../Admin/Scheduling/_DayWise", day);

                case "_WeekWise":
                    WeekWiseScheduling week = new WeekWiseScheduling
                    {
                        date = currentDate,
                        physicians = physician,
                        shiftdetails = _context.Shiftdetails.Include(u => u.Shift).ThenInclude(u => u.Physician).ToList()
                    };
                    return PartialView("../Admin/Scheduling/_WeekWise", week);

                case "_MonthWise":
                    MonthWiseScheduling month = new MonthWiseScheduling
                    {
                        date = currentDate,
                        shiftdetails = _context.Shiftdetails.Include(u => u.Shift).ThenInclude(u => u.Physician).ToList()
                    };
                    return PartialView("../Admin/Scheduling/_MonthWise", month);

                default:
                    return PartialView("../Admin/Scheduling/_DayWise");
            }
        }


        public IActionResult AddShift(SchedulingModel model)
        {
            string adminId = CV.ID();
            var chk = Request.Form["repeatdays"].ToList();
            _schedulingRepository.AddShift(model, chk, adminId);
            return RedirectToAction("Index");

        }



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
    }
}
