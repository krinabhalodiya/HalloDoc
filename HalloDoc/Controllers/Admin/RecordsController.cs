using AspNetCoreHero.ToastNotification.Abstractions;
using HalloDoc.Entity.Models;
using HalloDoc.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class RecordsController : Controller
    {
        #region Configuration
        private readonly IRecords _IRecords;
        private readonly INotyfService _notyf;
        public RecordsController(INotyfService iNotyfService, IRecords iRecords)
        {
            _IRecords = iRecords;
            _notyf = iNotyfService;
        }
        #endregion Configuration

        #region SearchRecords
        public IActionResult SearchRecords(RecordsModel rm)
        {
            RecordsModel model = _IRecords.GetFilteredSearchRecords(rm);
            return View("../Admin/Records/SearchRecords", model);
        }
        #endregion SearchRecords

        #region DeleteRequestSearchRecords
        public IActionResult DeleteRequest(int? RequestId)
        {
            if (_IRecords.DeleteRequest(RequestId))
            {
                _notyf.Success("Request Deleted Successfully.");
            }
            else
            {
                _notyf.Error("Request not deleted");
            }
            return RedirectToAction("SearchRecords");
        }
        #endregion DeleteRequestSearchRecords

        #region PatientHistory
        public IActionResult PatientHistory(RecordsModel model)
        {
            RecordsModel rm = _IRecords.GetFilteredPatientHistory(model);
            return View("../Admin/Records/PatientHistory", rm);
        }
        #endregion PatientHistory

        #region PatientRecords
        public async Task<IActionResult> PatientRecords(PaginatedViewModel data, int UserId)
        {
            var r = await _IRecords.PatientRecord(UserId, data);
            return View("../Admin/Records/PatientRecords", r);
        }
        #endregion PatientRecords

        #region BlockHistory
        public IActionResult BlockHistory(RecordsModel rm)
        {
            RecordsModel r = _IRecords.BlockHistory(rm);
            return PartialView("../Admin/Records/BlockHistory", r);
        }
        #endregion BlockHistory

        #region Unblock
        public IActionResult Unblock(int RequestId)
        {
            if (_IRecords.Unblock(RequestId, CV.ID()))
            {
                _notyf.Success("Case Unblocked Successfully.");
            }
            else
            {
                _notyf.Error("Case remains blocked.");
            }

            return RedirectToAction("BlockHistory");
        }
        #endregion Unblock
    }
}
