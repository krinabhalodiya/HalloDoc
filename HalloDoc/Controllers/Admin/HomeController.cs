using System.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.Spreadsheet;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace HalloDoc.Controllers.Admin
{
    public class HomeController : Controller
    {
        private readonly IAdminDashBoardActionsRepository _IAdminDashBoardActionsRepository;
        private readonly IComboboxRepository _combobox;
        private readonly INotyfService _notyf;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly ILoginRepository _loginRepository;
        private readonly IJwtService _jwtService;

        public HomeController(ILogger<AdminActionsController> logger,
                                      IComboboxRepository combobox,
                                      IAdminDashBoardActionsRepository actionrepo,
                                      INotyfService notyf,
                                      ILoginRepository loginRepository,
                                      IJwtService jwtService)
        {
            _combobox = combobox;
            _IAdminDashBoardActionsRepository = actionrepo;
            _notyf = notyf;
            _logger = logger;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
        }
        public IActionResult Index()
        {
            return View("../Admin/Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(Aspnetuser aspNetUser)
        {
            Entity.Models.UserInfo u = await _loginRepository.CheckAccessLogin(aspNetUser);

            if (u != null)
            {
                var jwttoken = _jwtService.GenerateJWTAuthetication(u);
                Response.Cookies.Append("jwt", jwttoken);
                Response.Cookies.Append("Status", "1");
                return RedirectToAction("Index", "AdminDashBoard");
            }
            else
            {
                ViewData["error"] = "Invalid Id Pass";
                return View("../Admin/Home/Index");
            }
        }
        #region end_session
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index", "Home");
        }
        #endregion
        public IActionResult AuthError()
        {
            return View("../Admin/Home/AuthError");
        }
    }
}
