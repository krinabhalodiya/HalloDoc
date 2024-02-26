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
    public class AdminController : Controller
    {
        private readonly IAdminDashBoardRepository _IAdminDashBoardRepository;

        public AdminController(IAdminDashBoardRepository IAdminDashBoardRepository)
        {
            _IAdminDashBoardRepository = IAdminDashBoardRepository;
        }
        public IActionResult Index()
        {
            CountStatusWiseRequestModel sm = _IAdminDashBoardRepository.Indexdata();
            
            return View(sm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _SearchResult(string Status)
        {
            if(Status == null)
            {
                Status = "1";
            }
            List<AdminDashboardList> contacts = _IAdminDashBoardRepository.GetRequests(Status);
            
            switch (Status)
            {
                case "1":
                    return PartialView("../Admin/_New", contacts);

                    break;
                case "2":

                    return PartialView("../Admin/_Pending", contacts);
                    break;
                case "4,5":

                    return PartialView("../Admin/_Active", contacts);
                    break;
                case "6":

                    return PartialView("../Admin/_Conclude", contacts);
                    break;
                case "3,7,8":

                    return PartialView("../Admin/_ToClose", contacts);
                    break;
                case "9":

                    return PartialView("../Admin/_UnPaid", contacts);
                    break;
            }


            return PartialView("../Admin/_New", contacts);
        }

    }
}
