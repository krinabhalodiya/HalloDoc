using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class RoleAccessRepository : IRoleAccessRepository
    {
        private readonly HalloDocContext _context;
        private readonly EmailConfiguration _emailConfig;
        public RoleAccessRepository(HalloDocContext context, EmailConfiguration emailConfig)
        {
            _context = context;
            _emailConfig = emailConfig;
        }
        #region GetRoleAccessDetails
        public async Task<List<Role>> GetRoleAccessDetails()
        {
            List<Role> v = await _context.Roles.Where(r => r.Isdeleted == new BitArray(1)).ToListAsync();
            return v;
        }
        #endregion

        #region GetAllUserDetails
        public async Task<List<ViewUserAcces>> GetAllUserDetails(int? User)
        {
            IQueryable<ViewUserAcces> query =
                from user in _context.Aspnetusers
                join admin in _context.Admins on user.Id equals admin.Aspnetuserid into adminGroup
                from admin in adminGroup.DefaultIfEmpty()
                join physician in _context.Physicians on user.Id equals physician.Aspnetuserid into physicianGroup
                from physician in physicianGroup.DefaultIfEmpty()
                join req in _context.Requests on physician.Physicianid equals req.Physicianid into totalrequestgroup
                from req in totalrequestgroup.DefaultIfEmpty()
                where (admin != null || physician != null) &&
                      (admin.Isdeleted == new BitArray(1) || physician.Isdeleted == new BitArray(1))
                select new ViewUserAcces
                {
                    UserName = user.Username,
                    FirstName = admin != null ? admin.Firstname : (physician != null ? physician.Firstname : null),
                    isAdmin = admin != null,
                    UserID = admin != null ? admin.Adminid : (physician != null ? physician.Physicianid : null),
                    accounttype = admin != null ? 2 : (physician != null ? 3 : null),
                    status = admin != null ? admin.Status : (physician != null ? physician.Status : null),
                    Mobile = admin != null ? admin.Mobile : (physician != null ? physician.Mobile : null),
                };

            if (User.HasValue)
            {
                switch (User.Value)
                {
                    case 2: // Admin data
                        query = query.Where(u => u.isAdmin);
                        break;
                    case 3: // Provider data
                        query = query.Where(u => !u.isAdmin);
                        break;
                }
            }
            return await query.ToListAsync();
        }
        #endregion

        #region GetRoleByMenus
        public async Task<ViewRoleByMenu> GetRoleByMenus(int roleid)
        {
            var r = await _context.Roles
                        .Where(r => r.Roleid == roleid)
                        .Select(r => new ViewRoleByMenu
                        {
                            Accounttype = r.Accounttype,
                            Createdby = r.Createdby,
                            Roleid = r.Roleid,
                            Name = r.Name,
                            Isdeleted = r.Isdeleted
                        })
                        .FirstOrDefaultAsync();
            return r;
        }
        #endregion
        #region GetMenusByAccount
        public async Task<List<HalloDoc.Entity.DataModels.Menu>> GetMenusByAccount(short Accounttype)
        {
            return await _context.Menus.Where(r => r.Accounttype == Accounttype).ToListAsync();
        }
        #endregion
        #region CheckMenuByRole
        public async Task<List<int>> CheckMenuByRole(int roleid)
        {
            return await _context.Rolemenus
                        .Where(r => r.Roleid == roleid)
                        .Select(r => r.Menuid)
                        .ToListAsync();
        }
        #endregion
        #region PostRoleMenu
        public async Task<bool> PostRoleMenu(ViewRoleByMenu role, string Menusid, string ID)
        {
            try
            {
                Role check = await _context.Roles.Where(r => r.Name == role.Name).FirstOrDefaultAsync();
                if (check == null && role != null && Menusid != null)
                {
                    Role r = new Role();
                    r.Name = role.Name;
                    r.Accounttype = role.Accounttype;
                    r.Createdby = ID;
                    r.Createddate = DateTime.Now;
                    r.Isdeleted = new System.Collections.BitArray(1);
                    r.Isdeleted[0] = false;
                    _context.Roles.Add(r);
                    _context.SaveChanges();
                    List<int> priceList = Menusid.Split(',').Select(int.Parse).ToList();
                    foreach (var item in priceList)
                    {
                        Rolemenu ar = new Rolemenu();
                        ar.Roleid = r.Roleid;
                        ar.Menuid = item;
                        _context.Rolemenus.Add(ar);
                    }
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PutRoleMenu
        public async Task<bool> PutRoleMenu(ViewRoleByMenu role, string Menusid, string ID)
        {
            try
            {
                Role check = await _context.Roles.Where(r => r.Roleid == role.Roleid).FirstOrDefaultAsync();
                if (check != null && role != null && Menusid != null)
                {
                    check.Name = role.Name;
                    check.Accounttype = role.Accounttype;
                    check.Modifiedby = ID;
                    check.Modifieddate = DateTime.Now;
                    _context.Roles.Update(check);
                    _context.SaveChanges();
                    List<int> regions = await CheckMenuByRole(check.Roleid);
                    List<int> priceList = Menusid.Split(',').Select(int.Parse).ToList();
                    foreach (var item in priceList)
                    {
                        if (regions.Contains(item))
                        {
                            regions.Remove(item);
                        }
                        else
                        {
                            Rolemenu ar = new Rolemenu();
                            ar.Menuid = item;
                            ar.Roleid = check.Roleid;
                            _context.Rolemenus.Update(ar);
                            await _context.SaveChangesAsync();
                            regions.Remove(item);
                        }
                    }
                    if (regions.Count > 0)
                    {
                        foreach (var item in regions)
                        {
                            Rolemenu ar = await _context.Rolemenus.Where(r => r.Roleid == check.Roleid && r.Menuid == item).FirstAsync();
                            _context.Rolemenus.Remove(ar);
                            await _context.SaveChangesAsync();
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
		#endregion

		#region DeletePhysician
		public async Task<bool> DeleteRoles(int roleid, string AdminID)
		{
			try
			{
				BitArray bt = new BitArray(1);
				bt.Set(0, true);
				if (roleid == null)
				{
					return false;
				}
				else
				{
					var DataForChange = await _context.Roles
						.Where(W => W.Roleid == roleid)
						.FirstOrDefaultAsync();
					if (DataForChange != null)
					{
						DataForChange.Isdeleted = bt;
						DataForChange.Isdeleted[0] = true;
						DataForChange.Modifieddate = DateTime.Now;
						DataForChange.Modifiedby = AdminID;
						_context.Roles.Update(DataForChange);
						_context.SaveChanges();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		#endregion
	}
}
