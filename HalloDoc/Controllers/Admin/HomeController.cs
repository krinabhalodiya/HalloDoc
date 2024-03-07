using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace HalloDoc.Controllers.Admin
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("../Admin/Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(string Email, string Passwordhash)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Database=HalloDoc;User Id=postgres;Password=Krina@2483;Include Error Detail=True");
            string Query = "select * from aspnetusers au inner join aspnetuserroles aur on au.id = aur.userid inner join aspnetroles roles on aur.roleid = roles.id where email=@Email and passwordhash=@Passwordhash";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Passwordhash", Passwordhash);
            NpgsqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            int numRows = dataTable.Rows.Count;
            if (numRows > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    HttpContext.Session.SetString("UserName", row["username"].ToString());
                    HttpContext.Session.SetString("UserID", row["Id"].ToString());
                    HttpContext.Session.SetString("RoleId", row["roleid"].ToString());
                }
                return RedirectToAction("Index", "AdminDashBoard");
            }
            else
            {
                ViewData["error"] = "Invalid Id Pass";
                return View("../Admin/Home/Index");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AuthError()
        {
            return View("../Admin/Home/AuthError");
        }
    }
}
