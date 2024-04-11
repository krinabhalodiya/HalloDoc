using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var user = await _context.Aspnetusers.FirstOrDefaultAsync(u => u.Email == aspNetUser.Email);
            UserInfo admin = new UserInfo();
            
            if (user != null)
            {
                    var hasher = new PasswordHasher<string>();
                    PasswordVerificationResult result = hasher.VerifyHashedPassword(null, user.Passwordhash, aspNetUser.Passwordhash);

                if (result != PasswordVerificationResult.Success)
                {
                    return null;
                }
                else
                {
                    var data = _context.Aspnetuserroles.FirstOrDefault(E => E.Userid == user.Id);
                    var datarole = _context.Aspnetroles.FirstOrDefault(e => e.Id == data.Roleid);
                    admin.Username = user.Username;
                    admin.FirstName = admin.FirstName ?? string.Empty;
                    admin.LastName = admin.LastName ?? string.Empty;
                    admin.Role = datarole.Name;
                   
                    admin.AspNetUserID = user.Id;
                    if (admin.Role == "Admin")
                    {
                        var admindata = _context.Admins.FirstOrDefault(u => u.Aspnetuserid == user.Id);
                        admin.UserId = admindata.Adminid;
                        admin.RoleID = (int)admindata.Roleid;

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
                        admin.RoleID=(int)admindata.Roleid;
                    }
                    return admin;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region Help_functions
        public async Task<bool> CheckregisterdAsync(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, pattern))
            {

                var U = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Email == email);
                if (U != null)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
        #region SavePass
        public async Task<bool> SavePass(string email, string Password)
        {
            try
            {
                Aspnetuser U = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Email == email);
                var hasher = new PasswordHasher<string>();
                U.Passwordhash = hasher.HashPassword(null, Password); ;
                _context.Update(U);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
        #endregion

        public bool isAccessGranted(int roleId, string menuName)
        {
            // Get the list of menu IDs associated with the role
            IQueryable<int> menuIds = _context.Rolemenus
                                            .Where(e => e.Roleid == roleId)
                                            .Select(e => e.Menuid);

            // Check if any menu with the given name exists in the list of menu IDs
            bool accessGranted = _context.Menus
                                         .Any(e => menuIds.Contains(e.Menuid) && e.Name == menuName);

            return accessGranted;
        }

    }
}
