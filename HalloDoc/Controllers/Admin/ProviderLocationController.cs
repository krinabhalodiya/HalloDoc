using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProviderLocationController : Controller
    {
        [CheckProviderAccess]
        public IActionResult Index()
        {
            return View("../Admin/ProviderLocation/Index");
        }
    }
}
