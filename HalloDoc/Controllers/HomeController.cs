using System.Diagnostics;
using HalloDoc.Entity;
using HalloDoc.Entity.DataContext;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRequestRepository _IRequestRepository;

        public HomeController(IRequestRepository IRequestRepository)
        {
            _IRequestRepository = IRequestRepository;
        }

        public IActionResult Index()
        {
           var adminDashboardList = _IRequestRepository.NewRequestData();
            
            return View(adminDashboardList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
