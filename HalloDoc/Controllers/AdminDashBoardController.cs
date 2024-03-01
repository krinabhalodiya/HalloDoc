using System.Diagnostics;
using HalloDoc.Entity;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HalloDoc.Entity.Models.Constant;

namespace HalloDoc.Controllers
{
    public class AdminDashBoardController : Controller
    {
        private readonly IAdminDashBoardRepository _IAdminDashBoardRepository;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminDashBoardController> _logger;

        public AdminDashBoardController(ILogger<AdminDashBoardController> logger,IAdminDashBoardRepository IAdminDashBoardRepository, IComboboxRepository combobox)
        {
            _IAdminDashBoardRepository = IAdminDashBoardRepository;
            _combobox = combobox;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.CaseReasonComboBox = await _combobox.CaseReasonComboBox();
            CountStatusWiseRequestModel sm = _IAdminDashBoardRepository.Indexdata();
            return View(sm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _SearchResult(string Status)
        {
            if (Status == null)
            {
                Status = "1";
            }
            List<AdminDashboardList> contacts = _IAdminDashBoardRepository.GetRequests(Status);
            switch (Status)
            {
                case "1":
                    return PartialView("../AdminDashBoard/_New", contacts);
                    break;
                case "2":
                    return PartialView("../AdminDashBoard/_Pending", contacts);
                    break;
                case "4,5":
                    return PartialView("../AdminDashBoard/_Active", contacts);
                    break;
                case "6":
                    return PartialView("../AdminDashBoard/_Conclude", contacts);
                    break;
                case "3,7,8":
                    return PartialView("../AdminDashBoard/_ToClose", contacts);
                    break;
                case "9":
                    return PartialView("../AdminDashBoard/_UnPaid", contacts);
                    break;
                default:
                    break;
            }
            return PartialView("../AdminDashBoard/_New", contacts);
        }
    }
}
