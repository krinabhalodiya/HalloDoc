using System.Collections;
using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly IComboboxRepository _combobox;
        private readonly ISchedulingRepository _schedulingRepository;
        private readonly INotyfService _notyf;
        public SchedulingController(IComboboxRepository comboboxRepository,  ISchedulingRepository schedulingRepository, INotyfService INotyfService)
        {
            _combobox = comboboxRepository;
            _schedulingRepository = schedulingRepository;
            _notyf = INotyfService;
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
            List<Physician> physician = _schedulingRepository.PhysicianAll();

            switch (PartialName)
            {
                case "_DayWise":
                    return PartialView("../Admin/Providers/Scheduling/_DayWise", _schedulingRepository.Daywise(regionid, currentDate));

                case "_WeekWise":
                    return PartialView("../Admin/Providers/Scheduling/_WeekWise", _schedulingRepository.Weekwise(regionid, currentDate));

                case "_MonthWise":
                    return PartialView("../Admin/Providers/Scheduling/_MonthWise", _schedulingRepository.Monthwise(regionid, currentDate));

                default:
                    return PartialView("../Admin/Providers/Scheduling/_MonthWise", _schedulingRepository.Monthwise(regionid, currentDate));
            }
        }
        #endregion

        #region LoadSchedulingPartialforprovider
        public IActionResult LoadSchedulingPartialProivder(string date)
        {
            var currentDate = DateTime.Parse(date);
            return PartialView("../Admin/Providers/Scheduling/_MonthWise", _schedulingRepository.MonthwisePhysician(currentDate, Int32.Parse(CV.UserID())));
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
            ViewBag.RegionCombobox = _combobox.RegionComboBox();
            SchedulingModel model = new SchedulingModel();
             model = _schedulingRepository.ViewShift(shiftdetailid,model);
            return model;
        }
        #endregion

        #region ViewShiftreturn
        public IActionResult ViewShiftreturn(SchedulingModel modal)
        {
            if (modal.shiftdate.Date < DateTime.Today.Date)
            {
                _notyf.Warning("Cant edit old shifts");
                return RedirectToAction("Index");
            }
            _schedulingRepository.ViewShiftreturn(modal);
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
