using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/Profile/Index");
        }
    }
}
