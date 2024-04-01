using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IRoleAccessRepository
    {
        public Task<List<Role>> GetRoleAccessDetails();
    }
}
