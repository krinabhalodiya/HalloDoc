using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HalloDoc.Controllers.Admin
{
    public class ProvidersController : Controller
    {
        #region Constructor
        private readonly IProviderRepository _IProviderRepository;
        private readonly INotyfService _notyf;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly EmailConfiguration _emailConfig;

        public ProvidersController(ILogger<AdminActionsController> logger,
                                      IProviderRepository IProviderRepository,
                                      INotyfService notyf, IComboboxRepository combobox, EmailConfiguration emailConfiguration)
        {
            _IProviderRepository = IProviderRepository;
            _notyf = notyf;
            _logger = logger;
            _combobox = combobox;
            _emailConfig = emailConfiguration;
        }
		#endregion
		#region Index
		public async Task<IActionResult> IndexAsync(int? region)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            var v = await _IProviderRepository.PhysicianAll();
            if (region == null)
            {
                v = await _IProviderRepository.PhysicianAll();
            }
            else 
            {
                v = await _IProviderRepository.PhysicianByRegion(region);
                return Json(v);
            }
            return View("../Admin/Providers/Index", v);
        }
        #endregion

        #region ChangeNotificationPhysician
        public async Task<IActionResult> ChangeNotificationPhysician(string changedValues)
		{
			Dictionary<int, bool> changedValuesDict = JsonConvert.DeserializeObject<Dictionary<int, bool>>(changedValues);
			_IProviderRepository.ChangeNotificationPhysician(changedValuesDict);
			return RedirectToAction("Index");
		}
        #endregion

        #region SendMessage
        public async Task<IActionResult> SendMessage( string? email, int? way, string? msg)
        {
            bool result;
            if (way == 1)
            {
                result = await _emailConfig.SendMail(email, "Check massage", "Hello " + msg);
            }
            else if (way == 2)
            {
                result = await _emailConfig.SendMail(email, "Check massage", "Hello " + msg);
            }
            else
            {
                result = await _emailConfig.SendMail(email, "Check massage", "Hello " + msg);
            }
            if (result)
            {
                _notyf.Success("Massage Send Successfully..!");
            }
            else
            {
                _notyf.Error("Massage Not Send..!");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
