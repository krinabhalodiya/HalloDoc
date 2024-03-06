using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProvidersController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/Providers/Index");
        }
    }
}
