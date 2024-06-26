﻿using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.Models.PatientModels;
using HallodocMVC.Repository.Admin.Repository.Interface;
using HallodocMVC.Repository.Patient.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers
{
    public class SubmitRequestController : Controller
    {
        
        #region Configuration
        private readonly HalloDocContext _context;
        private readonly ICreateRequest _ICreateRequest;
        private readonly INotyfService _INotyfService;
        private readonly IComboboxRepository _combobox;
        public SubmitRequestController(HalloDocContext context, ICreateRequest iActions, INotyfService iNotyfService, IComboboxRepository iComboboxRepository)
        {
            _context = context;
            _ICreateRequest = iActions;
            _INotyfService = iNotyfService;
            _combobox = iComboboxRepository;
        }
        #endregion Configuration
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #region CheckEmail
        [HttpPost]
        public async Task<IActionResult> CheckEmailAsync(string email)
        {
            var aspnetuser = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Email == email);
            return Json(new
            {
                isAspnetuser = aspnetuser == null
            });
        }
        #endregion CheckEmail

        #region PatientRequest
        public async Task<IActionResult> PatientRequestAsync()
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            return View();
        }
        #endregion PatientRequest 

        #region CreatePatientRequest
        public async Task<IActionResult> CreatePatientRequest(CreatePatientRequestModel model)
        {
            if (await _ICreateRequest.PatientRequest(model))
            {
                _INotyfService.Success("Request has been created successfully.");
            }
            else
            {
                _INotyfService.Error("Request has not been created successfully.");
            }
            return RedirectToAction("Index", "SubmitRequest");
        }
        #endregion CreatePatientRequest

        #region FamilyFriendRequest
        public async Task<IActionResult> FamilyFriendRequestAsync()
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            return View();
        }
        #endregion FamilyFriendRequest 

        #region CreateFamilyFriendRequest
        public async Task<IActionResult> CreateFamilyFriendRequest(CreateFamilyFriendRequestModel model)
        {
            if (await _ICreateRequest.FamilyFriendRequest(model))
            {
                _INotyfService.Success("Request has been created successfully.");
            }
            else
            {
                _INotyfService.Error("Request has not been created successfully.");
            }
            return RedirectToAction("Index", "SubmitRequest");
        }
        #endregion CreateFamilyFriendRequest

        #region ConciergeRequest
        public async Task<IActionResult> ConciergeRequestAsync()
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            return View();
        }
        #endregion ConciergeRequest 

        #region CreateConciergeRequest
        public async Task<IActionResult> CreateConciergeRequest(CreateConciergeRequestModel model)
        {
            if (await _ICreateRequest.ConciergeRequest(model))
            {
                _INotyfService.Success("Request has been created successfully.");
            }
            else
            {
                _INotyfService.Error("Request has not been created successfully.");
            }
            return RedirectToAction("Index", "SubmitRequest");
        }
        #endregion CreateConciergeRequest

        #region BusinessRequest
        public async Task<IActionResult> BusinessRequestAsync()
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            return View();
        }
        #endregion BusinessRequest 

        #region CreateBusinessRequest
        public async Task<IActionResult> CreateBusinessRequest(CreateBusinessRequestModel model)
        {
            if (await _ICreateRequest.BusinessRequest(model))
            {
                _INotyfService.Success("Request has been created successfully.");
            }
            else
            {
                _INotyfService.Error("Request has not been created successfully.");
            }
            return RedirectToAction("Index", "SubmitRequest");
        }
        #endregion CreateConciergeRequest
    }
}
