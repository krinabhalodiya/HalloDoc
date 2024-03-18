using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
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
    }
}
