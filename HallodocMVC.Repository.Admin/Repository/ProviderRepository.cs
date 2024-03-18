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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly HalloDocContext _context;
        private readonly EmailConfiguration _emailConfig;
        public ProviderRepository(HalloDocContext context, EmailConfiguration emailConfig)
        {
            _context = context;
            _emailConfig = emailConfig;
        }
        #region Find_Location_Physician
        public async Task<List<PhysicianLocation>> FindPhysicianLocation()
        {
            List<PhysicianLocation> pl = await _context.Physicianlocations
                                    .OrderByDescending(x => x.Physicianname)
                        .Select(r => new PhysicianLocation
                        {
                            locationid = r.Locationid,
                            longitude = r.Longitude,
                            latitude = r.Latitude,
                            physicianname = r.Physicianname

                        }).ToListAsync();
            return pl;

        }
        #endregion

    }
}
