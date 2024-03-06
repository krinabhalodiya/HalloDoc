using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class RecordsController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/Records/Index");
        }
    }
}
