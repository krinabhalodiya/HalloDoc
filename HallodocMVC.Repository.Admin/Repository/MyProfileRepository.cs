using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static HalloDoc.Entity.Models.ViewAdminProfile;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class MyProfileRepository : IMyProfileRepository
    {
        private readonly HalloDocContext _context;
        
        public MyProfileRepository(HalloDocContext context)
        {
            _context = context;
        }

        #region GetProfile
        public async Task<ViewAdminProfile> GetProfileDetails(int UserId)
        {
            ViewAdminProfile? v = await (from r in _context.Admins
                                        join Aspnetuser in _context.Aspnetusers
                                        on r.Aspnetuserid equals Aspnetuser.Id into aspGroup
                                        from asp in aspGroup.DefaultIfEmpty()
                                        where r.Adminid == UserId
                                        select new ViewAdminProfile
                                        {
                                            Roleid = r.Roleid,
                                            AdminId = r.Adminid,
                                            UserName = asp.Username,
                                            Address1 = r.Address1,
                                            Address2 = r.Address2,
                                            AltMobile = r.Altphone,
                                            City = r.City,
                                            Aspnetuserid = r.Aspnetuserid,
                                            Createdby = r.Createdby,
                                            Email = r.Email,
                                            Createddate = r.Createddate,
                                            Mobile = r.Mobile,
                                            Modifiedby = r.Modifiedby,
                                            Modifieddate = r.Modifieddate,
                                            Regionid = r.Regionid,
                                            Lastname = r.Lastname,
                                            Firstname = r.Firstname,
                                            Status = r.Status,
                                            Zipcode = r.Zip
                                        }).FirstOrDefaultAsync();
            List<Regions> regions = new List<Regions>();
            regions = await _context.Adminregions
                  .Where(r => r.Adminid == UserId)
                  .Select(req => new Regions()
                  {
                      regionid = req.Regionid
                  })
                  .ToListAsync();
            v.Regionids = regions;
            return v;
        }
        #endregion

        #region EditPassword
        public async Task<bool> EditPassword(string password, int adminId)
        {
            var hasher = new PasswordHasher<string>();
            var req = await _context.Admins.Where(W => W.Adminid == adminId).FirstOrDefaultAsync();
            Aspnetuser? U = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Id == req.Aspnetuserid);

            if (U != null)
            {
                U.Passwordhash = hasher.HashPassword(null, password);
                _context.Aspnetusers.Update(U);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region EditAdministratorInfo
        public async Task<bool> EditAdministratorInfo(ViewAdminProfile _viewAdminProfile)
        {
            try
            {
                if (_viewAdminProfile == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Admins.Where(W => W.Adminid == _viewAdminProfile.AdminId).FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        DataForChange.Email = _viewAdminProfile.Email;
                        DataForChange.Firstname = _viewAdminProfile.Firstname;
                        DataForChange.Lastname = _viewAdminProfile.Lastname;
                        DataForChange.Mobile = _viewAdminProfile.Mobile;
                        _context.Admins.Update(DataForChange);
                        _context.SaveChanges();
                        List<int> regions = await _context.Adminregions.Where(r => r.Adminid == _viewAdminProfile.AdminId).Select(req => req.Regionid).ToListAsync();
                        List<int> priceList = _viewAdminProfile.Regionsid.Split(',').Select(int.Parse).ToList();
                        foreach (var item in priceList)
                        {
                            if (regions.Contains(item))
                            {
                                regions.Remove(item);
                            }
                            else
                            {
                                Adminregion ar = new()
                                {
                                    Regionid = item,
                                    Adminid = (int)_viewAdminProfile.AdminId
                                };
                                _context.Adminregions.Update(ar);
                                await _context.SaveChangesAsync();
                                regions.Remove(item);
                            }
                        }
                        if (regions.Count > 0)
                        {
                            foreach (var item in regions)
                            {
                                Adminregion ar = await _context.Adminregions.Where(r => r.Adminid == _viewAdminProfile.AdminId && r.Regionid == item).FirstAsync();
                                _context.Adminregions.Remove(ar);
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
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region BillingInfoEdit
        public async Task<bool> BillingInfoEdit(ViewAdminProfile _viewAdminProfile)
        {
            try
            {
                if (_viewAdminProfile == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Admins.Where(W => W.Adminid == _viewAdminProfile.AdminId).FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        DataForChange.Address1 = _viewAdminProfile.Address1;
                        DataForChange.Address2 = _viewAdminProfile.Address2;
                        DataForChange.City = _viewAdminProfile.City;
                        DataForChange.Mobile = _viewAdminProfile.Mobile;
                        _context.Admins.Update(DataForChange);
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
