using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class AccessController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/Access/Index");
        }
    }
}
