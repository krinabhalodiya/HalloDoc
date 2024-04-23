using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IRoleAccessRepository
    {
        public Task<List<Role>> GetRoleAccessDetails();
        public Task<List<ViewUserAcces>> GetAllUserDetails(int? User);
        public Task<ViewRoleByMenu> GetRoleByMenus(int roleid);
        public List<string> getManuByID(int RoleID);
		public Task<List<HalloDoc.Entity.DataModels.Menu>> GetMenusByAccount(short Accounttype);
        public Task<List<int>> CheckMenuByRole(int roleid);
        public Task<bool> PostRoleMenu(ViewRoleByMenu role, string Menusid, string ID);
        public Task<bool> PutRoleMenu(ViewRoleByMenu role, string Menusid, string ID);
        public Task<bool> DeleteRoles(int roleid, string AdminID);

	}
}
