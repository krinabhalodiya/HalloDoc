using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProviderLocationController : Controller
    {
        [CheckAdminAccess]
        [CheckProviderAccess]
        public IActionResult Index()
        {
            return View("../Admin/ProviderLocation/Index");
        }
    }
}
