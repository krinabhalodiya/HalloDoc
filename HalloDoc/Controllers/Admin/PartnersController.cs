using AspNetCoreHero.ToastNotification.Abstractions;
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
        public async Task<IActionResult> Index(int? regionId)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            List<Healthprofessional> data = await _IPartnersRepository.GetPartnersByProfession(regionId);
            return View("../Admin/Partners/Index", data);
        }
    }
}
