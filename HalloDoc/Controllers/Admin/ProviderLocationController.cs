using AspNetCoreHero.ToastNotification.Abstractions;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProviderLocationController : Controller
    {
        #region Constructor
        private readonly IProviderRepository _IProviderRepository;
        private readonly INotyfService _notyf;
        private readonly ILogger<AdminActionsController> _logger;

        public ProviderLocationController(ILogger<AdminActionsController> logger,
                                      IProviderRepository IProviderRepository,
                                      INotyfService notyf)
        {
            _IProviderRepository = IProviderRepository;
            _notyf = notyf;
            _logger = logger;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            ViewBag.Log = await _IProviderRepository.FindPhysicianLocation();
            return View("../Admin/ProviderLocation/Index");
        }
    }
}
