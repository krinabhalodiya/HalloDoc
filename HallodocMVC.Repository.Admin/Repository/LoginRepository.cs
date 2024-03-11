using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class LoginRepository : ILoginRepository
    {
        #region Constructor
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HalloDocContext _context;
        public LoginRepository(HalloDocContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        #endregion

        #region Constructor
        public async Task<UserInfo> CheckAccessLogin(Aspnetuser aspNetUser)
        {
            var user = await _context.Aspnetusers.FirstOrDefaultAsync(u => u.Email == aspNetUser.Email && u.Passwordhash == aspNetUser.Passwordhash);
            UserInfo admin = new UserInfo();
            if (user != null)
            {
                    var data = _context.Aspnetuserroles.FirstOrDefault(E => E.Userid == user.Id);
                    var datarole = _context.Aspnetroles.FirstOrDefault(e => e.Id == data.Roleid);
                    admin.Username = user.Username;
                    admin.FirstName = admin.FirstName ?? string.Empty;
                    admin.LastName = admin.LastName ?? string.Empty;
                    admin.Role = datarole.Name;
                    if (admin.Role == "Admin")
                    {
                        var admindata = _context.Admins.FirstOrDefault(u => u.Aspnetuserid == user.Id);
                        admin.UserId = admindata.Adminid;
                    }
                    else if (admin.Role == "Patient")
                    {
                        var admindata = _context.Users.FirstOrDefault(u => u.Aspnetuserid == user.Id);
                        admin.UserId = admindata.Userid;
                    }
                    else
                    {
                        var admindata = _context.Physicians.FirstOrDefault(u => u.Aspnetuserid == user.Id);
                        admin.UserId = admindata.Physicianid;
                    }
                    return admin;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
