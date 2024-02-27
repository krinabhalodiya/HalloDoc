using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using HalloDoc.Entity.Models;

namespace HalloDoc.Controllers
{
    public class AdminActionsController : Controller
    {
        private readonly IAdminDashBoardActionsRepository _IAdminDashBoardActionsRepository;

        public AdminActionsController(IAdminDashBoardActionsRepository iAdminDashBoardActionsRepository)
        {
            _IAdminDashBoardActionsRepository = iAdminDashBoardActionsRepository;
        }
        public async Task<IActionResult> ViewCase(int id)
        {
            //ViewBag.RegionComboBox = await _combobox.RegionComboBox();
            //CaseReasonComboBox = await _combobox.CaseReasonComboBox();
            ViewCaseData sm = _IAdminDashBoardActionsRepository.GetRequestForViewCase(id);

            return View("../AdminActions/ViewCase", sm);
        }
        public IActionResult EditCase(ViewCaseData vcd)
        {
            bool result = _IAdminDashBoardActionsRepository.EditCase(vcd);
            if (result)
            {
                return RedirectToAction("ViewCase", new { id = vcd.RequestID });
            }
            else
            {
                return View("../AdminActions/ViewCase", vcd);
            }
        }

    }
}
