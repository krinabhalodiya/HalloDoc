using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface ILoginRepository
    {
        Task<UserInfo> CheckAccessLogin(Aspnetuser aspNetUser);
        public Task<bool> CheckregisterdAsync(string email);
        public Task<bool> SavePass(string email, string Password);
        public bool isAccessGranted(int roleId, string menuName);
        public Task<bool> CreatNewAccont(string Email, string Password);
    }
}
