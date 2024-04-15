using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class PartnersController : Controller
    {
        #region Constructor
        private readonly IPartnersRepository _IPartnersRepository;
        private readonly IProviderRepository _IProviderRepository;
        private readonly IMyProfileRepository _IMyProfileRepository;
        private readonly INotyfService _notyf;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly EmailConfiguration _emailConfig;

        public PartnersController(ILogger<AdminActionsController> logger, IPartnersRepository IPartnersRepository, INotyfService notyf, IComboboxRepository combobox, EmailConfiguration emailConfiguration)
        {
            _IPartnersRepository = IPartnersRepository;
            _notyf = notyf;
            _logger = logger;
            _combobox = combobox;
            _emailConfig = emailConfiguration;
        }
        #endregion
        public async Task<IActionResult> Index(VendorsPagination FormData)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.ProfessionType = await _combobox.healthprofessionaltype();
            VendorsPagination data = _IPartnersRepository.PartnersByProfession(FormData);
            return View("../Admin/Partners/Index", data);
        }
        #region AddEditBusiness
        public async Task<IActionResult> AddEditBusiness(int? VendorId)
        {
            List<HealthProfessionalTypeComboBox> hpt = await _combobox.healthprofessionaltype();
            ViewBag.ProfessionType = hpt;
            if (VendorId == null)
            {
                ViewData["Status"] = "Add";
            }
            else
            {
                ViewData["Status"] = "Edit";
                VendorsData data = await _IPartnersRepository.BusinessById(VendorId);
                return View("../Admin/Partners/AddEditPartners", data);
            }
            return View("../Admin/Partners/AddEditPartners");
        }
        #endregion AddEditBusiness

        #region AddBusiness
        public IActionResult AddBusiness(VendorsData data)
        {
            if (_IPartnersRepository.AddEditBusiness(data))
            {
                if (data.VendorId != 0)
                {
                    _notyf.Success("Business Edited Successfully.");
                }
                else
                {
                    _notyf.Success("Business Added Successfully.");
                }
            }
            else
            {
                if (data.VendorId != 0)
                {
                    _notyf.Error("Business not edited.");
                }
                else
                {
                    _notyf.Error("Business not added.");
                }
            }
            return RedirectToAction("Index");
        }
        #endregion AddBusiness

        #region DeleteVendor
        public IActionResult DeleteVendor(int? VendorId)
        {
            if (_IPartnersRepository.DeleteVendor(VendorId))
            {
                _notyf.Success("Vendor Deleted Successfully.");
            }
            else
            {
                _notyf.Error("Vendor not deleted");
            }
            return RedirectToAction("Index");
        }
        #endregion DeleteVendor
    }
}
