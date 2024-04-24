using System.Diagnostics;
using Assignment.Entity.DataModels;
using Assignment.Entity.Model;
using Assignment.Models;
using Assignment.Repository.Repository;
using Assignment.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeDataRepository _IEmployeeDataRepository;
        public HomeController(ILogger<HomeController> logger , IEmployeeDataRepository iEmployeeDataRepository)
        {
            _logger = logger;
            _IEmployeeDataRepository = iEmployeeDataRepository;
        }
        public IActionResult Index(PaginatedViewModel data)
        {
            data = _IEmployeeDataRepository.GetData(data);
            return View(data);
        }

        #region AddEmployee
        public async Task<IActionResult> AddEmployee(PaginatedViewModel data)
        {
            await _IEmployeeDataRepository.AddEmployee(data);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region AddEmployee
        public async Task<IActionResult> EditEmployee(PaginatedViewModel data)
        {
            await _IEmployeeDataRepository.EditEmployee(data);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region AddEmployee
        public async Task<IActionResult> DeleteEmployee(int employeeid)
        {
            await _IEmployeeDataRepository.DeleteEmployee(employeeid);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
