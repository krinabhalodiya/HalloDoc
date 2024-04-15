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
using OfficeOpenXml;
using HallodocMVC.Repository.Admin.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using HalloDoc.Entity.Models.PatientModels;
using HallodocMVC.Repository.Patient.Repository.Interface;
namespace HalloDoc.Controllers
{
    public class AdminDashBoardController : Controller
    {
        private readonly IAdminDashBoardRepository _IAdminDashBoardRepository;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminDashBoardController> _logger;
        private readonly INotyfService _notyf;
        private readonly ICreateRequest _ICreateRequest;

        public AdminDashBoardController(ILogger<AdminDashBoardController> logger,IAdminDashBoardRepository IAdminDashBoardRepository, IComboboxRepository combobox, INotyfService iNotyfService, ICreateRequest iCreateRequest)
        {
            _IAdminDashBoardRepository = IAdminDashBoardRepository;
            _combobox = combobox;
            _notyf = iNotyfService;
            _logger = logger;
            _ICreateRequest = iCreateRequest;
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
        #region Export
        public IActionResult Export(string status)
        {
            var requestData = _IAdminDashBoardRepository.Export(status);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("RequestData");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Requestor";
                worksheet.Cells[1, 3].Value = "Request Date";
                worksheet.Cells[1, 4].Value = "Phone";
                worksheet.Cells[1, 5].Value = "Address";
                worksheet.Cells[1, 6].Value = "Notes";
                worksheet.Cells[1, 7].Value = "Physician";
                worksheet.Cells[1, 8].Value = "Birth Date";
                worksheet.Cells[1, 9].Value = "RequestTypeId";
                worksheet.Cells[1, 10].Value = "Email";
                worksheet.Cells[1, 11].Value = "RequestId";

                for (int i = 0; i < requestData.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = requestData[i].PatientName;
                    worksheet.Cells[i + 2, 2].Value = requestData[i].Requestor;
                    worksheet.Cells[i + 2, 3].Value = requestData[i].RequestedDate;
                    worksheet.Cells[i + 2, 4].Value = requestData[i].PhoneNumber;
                    worksheet.Cells[i + 2, 5].Value = requestData[i].Address;
                    worksheet.Cells[i + 2, 6].Value = requestData[i].Notes;
                    worksheet.Cells[i + 2, 7].Value = requestData[i].ProviderName;
                    worksheet.Cells[i + 2, 8].Value = requestData[i].Dob;
                    worksheet.Cells[i + 2, 9].Value = requestData[i].RequestTypeID;
                    worksheet.Cells[i + 2, 10].Value = requestData[i].Email;
                    worksheet.Cells[i + 2, 11].Value = requestData[i].RequestID;
                }

                byte[] excelBytes = package.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
        #endregion
        #region SendLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendLink(string Firstname, string Lastname, string Mobile, string Email)
        {
            if (_IAdminDashBoardRepository.SendLink(Email))
            {
                _notyf.Success("Mail Send  Successfully..!");
            }
            if (_IAdminDashBoardRepository.SendSMS(Mobile))
            {
                _notyf.Success("Message Send  Successfully..!");
            }
            return RedirectToAction("Index", "AdminDashboard");
        }
        #endregion
        #region PatientRequest
        public IActionResult PatientRequest()
        {
            return View("../SubmitRequest/PatientRequest");
        }
        #endregion PatientRequest 

        #region CreatePatientRequest
        public async Task<IActionResult> CreatePatientRequest(CreatePatientRequestModel model)
        {
            if (await _ICreateRequest.PatientRequest(model))
            {
                _notyf.Success("Request has been created successfully.");
            }
            else
            {
                _notyf.Error("Request has not been created successfully.");
            }
            return RedirectToAction("Index", "AdminDashboard");
        }
        #endregion CreatePatientRequest
    }
}
