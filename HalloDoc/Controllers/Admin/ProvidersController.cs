using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.Office2010.Excel;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HalloDoc.Controllers.Admin
{
    public class ProvidersController : Controller
    {
        #region Constructor
        private readonly IProviderRepository _IProviderRepository;
        private readonly INotyfService _notyf;
        private readonly IComboboxRepository _combobox;
        private readonly ILogger<AdminActionsController> _logger;
        private readonly EmailConfiguration _emailConfig;

        public ProvidersController(ILogger<AdminActionsController> logger,
                                      IProviderRepository IProviderRepository,
                                      INotyfService notyf, IComboboxRepository combobox, EmailConfiguration emailConfiguration)
        {
            _IProviderRepository = IProviderRepository;
            _notyf = notyf;
            _logger = logger;
            _combobox = combobox;
            _emailConfig = emailConfiguration;
        }
		#endregion
		#region Index
		public async Task<IActionResult> IndexAsync(int? region)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            var v = await _IProviderRepository.PhysicianAll();
            if (region == null)
            {
                v = await _IProviderRepository.PhysicianAll();
            }
            else 
            {
                v = await _IProviderRepository.PhysicianByRegion(region);
                /*return Json(v);*/
            }
            return View("../Admin/Providers/Index", v);
        }
        #endregion

        #region ChangeNotificationPhysician
        public async Task<IActionResult> ChangeNotificationPhysician(string changedValues)
		{
			Dictionary<int, bool> changedValuesDict = JsonConvert.DeserializeObject<Dictionary<int, bool>>(changedValues);
			_IProviderRepository.ChangeNotificationPhysician(changedValuesDict);
			return RedirectToAction("Index");
		}
        #endregion

        #region SendMessage
        public async Task<IActionResult> SendMessage( string? email, int? way, string? msg)
        {
            bool result;
            if (way == 1)
            {
                result = await _emailConfig.SendMail(email, "Check massage", "Hello " + msg);
            }
            else if (way == 2)
            {
                result = await _emailConfig.SendMail(email, "Check massage", "Hello " + msg);
            }
            else
            {
                result = await _emailConfig.SendMail(email, "Check massage", "Hello " + msg);
            }
            if (result)
            {
                _notyf.Success("Massage Send Successfully..!");
            }
            else
            {
                _notyf.Error("Massage Not Send..!");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit_Profile
        public async Task<IActionResult> PhysicianProfile(int? id)
        {

            //TempData["Status"] = TempData["Status"];
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.PhysicianRoleComboBox();
            if (id == null)
            {
                ViewData["PhysicianAccount"] = "Add";
            }
            else
            {
                ViewData["PhysicianAccount"] = "Edit";
                PhysiciansData v = await _IProviderRepository.GetPhysicianById((int)id);
                return View("../Admin/Providers/AddEditProvider",v);
            }
            return View("../Admin/Providers/AddEditProvider");
        }
        #endregion
        #region Physician_Add
        [HttpPost]
        public async Task<IActionResult> PhysicianAdd(PhysiciansData physicians)
        {
            ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            ViewBag.userrolecombobox = await _combobox.PhysicianRoleComboBox();
            // bool b = physicians.Isagreementdoc[0];

            /*if (ModelState.IsValid)
            {*/
                
			/*}
            else
            {
                return View("../Admin/Providers/AddEditProvider", physicians);
            }*/
			if (await _IProviderRepository.PhysicianAddEdit(physicians, CV.ID()))
			{
				_notyf.Success("Physician added Successfully..!");
			}
			else
			{
				_notyf.Error("Physician not added Successfully..!");
			}
			return RedirectToAction("Index");
        }
        #endregion
        #region Update_Physician_Profile
        public async Task<IActionResult> EditAccountInfo(PhysiciansData data)
        {
            string actionName = RouteData.Values["action"].ToString();
            string actionNameaq = ControllerContext.ActionDescriptor.ActionName; // Get the current action name
            if (await _IProviderRepository.EditAccountInfo(data))
            {
                _notyf.Success("Account Information Changed Successfully..!");
            }
            else
            {
                _notyf.Error("Account Information not Changed Successfully..!");
            }
            return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
        }
        #endregion
        #region ResetPassAdmin
        public async Task<IActionResult> ResetPassAdmin(string password, int Physicianid)
        {
            if (await _IProviderRepository.ChangePasswordAsync(password, Physicianid))
            {
                _notyf.Success("Password Information Changed Successfully..!");
            }
            else
            {
                _notyf.Error("Password not Changed properly Successfully..!");
            }
            return RedirectToAction("PhysicianProfile", new { id = Physicianid });
        }
        #endregion
        #region EditPhysicianInfo
        public async Task<IActionResult> EditPhysicianInfo(PhysiciansData data)
        {
            if (await _IProviderRepository.EditPhysicianInfo(data))
            {
                _notyf.Success("Administrator Information Changed Successfully..!");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
            else
            {
                _notyf.Error("Administrator Information not Changed Successfully..!");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
        }
        #endregion

        #region EditMailBillingInfo
        public async Task<IActionResult> EditMailBillingInfo(PhysiciansData data)
        {
            if (await _IProviderRepository.EditMailBillingInfo(data, CV.ID()))
            {
                _notyf.Success("mail and billing Information Changed Successfully...") ;
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
            else
            {
                _notyf.Error("mail and billing Information not Changed Successfully...");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
        }
        #endregion

        #region EditProviderProfile
        public async Task<IActionResult> EditProviderProfile(PhysiciansData data)
        {
            if (await _IProviderRepository.EditProviderProfile(data, CV.ID()))
            {
                _notyf.Success("Profile Changed Successfully...");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
            else
            {
                _notyf.Error("Profile not Changed Successfully...");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
        }
        #endregion
        #region EditProviderOnbording
        public async Task<IActionResult> EditProviderOnbording(PhysiciansData data)
        {
            if (await _IProviderRepository.EditProviderOnbording(data, CV.ID()))
            {
                _notyf.Success("Provider Onbording Changed Successfully...");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
            else
            {
                _notyf.Error("Provider Onbording not Changed Successfully...");
                return RedirectToAction("PhysicianProfile", new { id = data.Physicianid });
            }
        }
        #endregion
        #region DeletePhysician
        public async Task<IActionResult> DeletePhysician(int PhysicianID)
        {
            bool data = await _IProviderRepository.DeletePhysician(PhysicianID, CV.ID());
            if (data)
            {
                _notyf.Success("Physician deleted successfully...");
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Success("Physician not deleted successfully...");
                return RedirectToAction("PhysicianAll");
            }
        }
        #endregion
    }
}
