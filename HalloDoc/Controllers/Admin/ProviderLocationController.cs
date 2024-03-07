using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProviderLocationController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/ProviderLocation/Index");
        }
    }
}
