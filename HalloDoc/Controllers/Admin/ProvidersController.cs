using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataModels;
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

        public ProvidersController(ILogger<AdminActionsController> logger,
                                      IProviderRepository IProviderRepository,
                                      INotyfService notyf, IComboboxRepository combobox)
        {
            _IProviderRepository = IProviderRepository;
            _notyf = notyf;
            _logger = logger;
            _combobox = combobox;
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
            }
            return View("../Admin/Providers/Index",v);
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
	}
}
