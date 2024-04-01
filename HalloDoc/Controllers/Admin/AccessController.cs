using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class AccessController : Controller
    {
        #region Constructor
        private readonly IRoleAccessRepository _IRoleAccessRepository;
        private readonly INotyfService _notyf;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly EmailConfiguration _emailConfig;

        public AccessController(ILogger<AdminActionsController> logger,IRoleAccessRepository IRoleAccessRepository,INotyfService notyf, IComboboxRepository combobox, EmailConfiguration emailConfiguration)
        {
            _IRoleAccessRepository = IRoleAccessRepository;
            _notyf = notyf;
            _logger = logger;
            _combobox = combobox;
            _emailConfig = emailConfiguration;
        }
        #endregion
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Role> v = await _IRoleAccessRepository.GetRoleAccessDetails();
            return View("../Admin/Access/Index",v);
        }
        #endregion
    }
}
