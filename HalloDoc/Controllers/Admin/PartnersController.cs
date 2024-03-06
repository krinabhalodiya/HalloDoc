using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers.Admin
{
    public class PartnersController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/Partners/Index");
        }
    }
}
