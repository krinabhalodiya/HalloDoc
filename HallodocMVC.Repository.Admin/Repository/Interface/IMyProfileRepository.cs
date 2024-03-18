using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IMyProfileRepository
    {
        public Task<ViewAdminProfile> GetProfileDetails(int UserId);
    }
}
