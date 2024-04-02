using System.Diagnostics;
using System.Net.NetworkInformation;
using HalloDoc.Controllers.Admin;
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
        [CheckProviderAccess("Admin")]
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.CaseReasonComboBox = await _combobox.CaseReasonComboBox();
            PaginatedViewModel sm = _IAdminDashBoardRepository.Indexdata();
            return View(sm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _SearchResult(PaginatedViewModel data, String Status)
        {
            if (Status == null)
            {
            Status = CV.CurrentStatus();
            }
            Response.Cookies.Delete("Status");
            Response.Cookies.Append("Status", Status);

            
            PaginatedViewModel contacts = _IAdminDashBoardRepository.GetRequests(Status, data);
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
