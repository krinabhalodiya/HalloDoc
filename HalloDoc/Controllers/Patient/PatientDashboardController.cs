using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Patient
{
    public class PatientDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("../Patient/PatientDashboard/Index");
        }
    }
}
