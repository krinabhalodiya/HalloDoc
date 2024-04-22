using System.Collections;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Org.BouncyCastle.Asn1.Ocsp;

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
        private readonly EmailConfiguration _emailConfig;

        public HomeController(ILogger<AdminActionsController> logger,
                                      IComboboxRepository combobox,
                                      IAdminDashBoardActionsRepository actionrepo,
                                      INotyfService notyf,
                                      ILoginRepository loginRepository,
                                      IJwtService jwtService, EmailConfiguration emailConfiguration)
        {
            _combobox = combobox;
            _IAdminDashBoardActionsRepository = actionrepo;
            _notyf = notyf;
            _logger = logger;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
            _emailConfig = emailConfiguration;
        }
        public IActionResult LandingPage()
        {
            return View("../Admin/Home/LandingPage");
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

            if (aspNetUser.Email!=null && aspNetUser.Passwordhash != null && u != null  )
            {
                var jwttoken = _jwtService.GenerateJWTAuthetication(u);
                Response.Cookies.Append("jwt", jwttoken);
                Response.Cookies.Append("Status", "1");
                if(u.Role == "Patient")
                {
                    return RedirectToAction("Index", "PatientDashboard");
                }
                else if(u.Role == "Provider")
                {
                    return Redirect("~/Physician/DashBoard");
                }
                return Redirect("~/Admin/DashBoard");
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
        public IActionResult ForgotPass()
        {
            return View("../Admin/Home/ForgotPassword");
        }
        #region SendMail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassEmail(string Email)
        {
            if (await _loginRepository.CheckregisterdAsync(Email))
            {
                var Subject = "Change PassWord";
                var agreementUrl = " https://localhost:44306/Home/ResetPassWord?Datetime=" + _emailConfig.Encode(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")) + "&email=" + _emailConfig.Encode(Email);
               
                _emailConfig.SendMail(Email, Subject, $"<a href='{agreementUrl}'>ResetPass</a>");
            }
            else
            {
                ViewData["EmailCheck"] = "Your Email Is not registered";
                return View("../Admin/Home/ForgotPassword");
            }
            _notyf.Success("Mail Send successfully");
             return View("../Admin/Home/Index");
        }
        #endregion

        #region Reset_Password
            #region ResetPassWord
            public async Task<IActionResult> ResetPassword(string? Datetime, string? email)
            {
                string Decodee = _emailConfig.Decode(email);
                DateTime s = DateTime.ParseExact(_emailConfig.Decode(Datetime), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                TimeSpan dif = s - DateTime.Now;
                if (dif.Hours < 24)
                {
                    ViewBag.email = Decodee;
                    return View("../Admin/Home/ResetPassword");
                }
                else
                {
                    ViewBag.TotalHours = "Url is expaier";
                }
                return View("../Admin/Home/ResetPassword");
            }
            #endregion

        #region ResetPassWord_SavePassAsync
        public async Task<IActionResult> SavePassAsync(string ConfirmPassword, string Password, string Email)
        {
            if (Password != null)
            {
                if (ConfirmPassword != Password)
                {
                    return View("ResetPassWord");
                }
                try
                {
                    if (Email == null)
                    {
                        _notyf.Error("Password is not Saved successfully");
                        return View("../Admin/Home/ResetPassword");
                    }
                    else
                    {
                        if (await _loginRepository.SavePass(Email, Password))
                        {
                            _notyf.Success("Password Saved successfully");
                            return View("../Admin/Home/ResetPassword");
                        }
                        else
                        {
                            _notyf.Error("Password is not Saved successfully");
                            return View("../Admin/Home/ResetPassword");
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return View("../Admin/Home/ResetPassword");
        }
        #endregion
        #endregion
        public async Task<IActionResult> CreatNewAccontPost(string Email, string Password)
        {
            if (await _loginRepository.CreatNewAccont(Email, Password))
            {
                _notyf.Success("User Created Successfully");
            }
            else
            {
                _notyf.Error("You can not create account with this id twice");
                return View("../Admin/Home/CreateAccount");
            }
            return View("../Admin/Home/Index");
        }
        #region ResetPassWord
        public async Task<IActionResult> CreatNewAccont()
        {
            return View("../Admin/Home/CreateAccount");
        }
        #endregion
    }
}
