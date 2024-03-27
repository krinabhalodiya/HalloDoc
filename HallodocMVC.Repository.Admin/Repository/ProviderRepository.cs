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

        #region PhysicianAll
        public async Task<List<PhysiciansData>> PhysicianAll()
        {
            List<PhysiciansData> data = await (from r in _context.Physicians
                                         join Notifications in _context.Physiciannotifications
                                         on r.Physicianid equals Notifications.Physicianid into aspGroup
                                         from nof in aspGroup.DefaultIfEmpty()
                                         join role in _context.Roles
                                         on r.Roleid equals role.Roleid into roleGroup
                                         from roles in roleGroup.DefaultIfEmpty()
                                         where r.Isdeleted == new BitArray(1)
                                         select new PhysiciansData
                                         {
                                             notificationid = nof.Id,
                                             Createddate = r.Createddate,
                                             Physicianid = r.Physicianid,
                                             Address1 = r.Address1,
                                             Address2 = r.Address2,
                                             Adminnotes = r.Adminnotes,
                                             Altphone = r.Altphone,
                                             Businessname = r.Businessname,
                                             Businesswebsite = r.Businesswebsite,
                                             City = r.City,
                                             Firstname = r.Firstname,
                                             Lastname = r.Lastname,
                                             notification = nof.Isnotificationstopped,
                                             role = roles.Name,
                                             Status = r.Status,
                                             Email = r.Email,
                                             Isnondisclosuredoc = r.Isnondisclosuredoc
                                         })
                                        .ToListAsync();
            return data;
        }
        #endregion

        #region PhysicianByRegion
        public async Task<List<PhysiciansData>> PhysicianByRegion(int? region)
        {
            List<PhysiciansData> data = await (
                                        from pr in _context.Physicianregions
                                        join ph in _context.Physicians
                                        on pr.Physicianid equals ph.Physicianid into rGroup
                                        from r in rGroup.DefaultIfEmpty()
                                        join Notifications in _context.Physiciannotifications
                                        on r.Physicianid equals Notifications.Physicianid into aspGroup
                                        from nof in aspGroup.DefaultIfEmpty()
                                        join role in _context.Roles
                                        on r.Roleid equals role.Roleid into roleGroup
                                        from roles in roleGroup.DefaultIfEmpty()
                                        where pr.Regionid == region
                                        select new PhysiciansData
                                        {
                                            Createddate = r.Createddate,
                                            Physicianid = r.Physicianid,
                                            Address1 = r.Address1,
                                            Address2 = r.Address2,
                                            Adminnotes = r.Adminnotes,
                                            Altphone = r.Altphone,
                                            Businessname = r.Businessname,
                                            Businesswebsite = r.Businesswebsite,
                                            City = r.City,
                                            Firstname = r.Firstname,
                                            Lastname = r.Lastname,
                                            notification = nof.Isnotificationstopped,
                                            role = roles.Name,
                                            Status = r.Status
                                        })
                                        .ToListAsync();
            return data;
        }
		#endregion

		#region Change_Notification_Physician
		public async Task<bool> ChangeNotificationPhysician(Dictionary<int, bool> changedValuesDict)
		{
			try
			{
				if (changedValuesDict == null)
				{
					return false;
				}
				else
				{
					foreach (var item in changedValuesDict)
					{
						var ar = _context.Physiciannotifications.Where(r => r.Physicianid == item.Key).FirstOrDefault();
						if (ar != null)
						{
							ar.Isnotificationstopped[0] = item.Value;
							_context.Physiciannotifications.Update(ar);
							_context.SaveChanges();
						}
						else
						{
							Physiciannotification pn = new Physiciannotification();
							pn.Physicianid = item.Key;
							pn.Isnotificationstopped = new BitArray(1);
							pn.Isnotificationstopped[0] = item.Value;
							_context.Physiciannotifications.Add(pn);
							_context.SaveChanges();
						}
					}
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		#endregion
	}
}
