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
using Microsoft.AspNetCore.Identity;
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
                                             Isnondisclosuredoc = r.Isnondisclosuredoc == null ? false : true
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
                                            Status = r.Status,
                                            Email = r.Email,
                                            Isnondisclosuredoc = r.Isnondisclosuredoc == null ? false : true
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

        #region Physician_Add
        public async Task<bool> PhysicianAddEdit(PhysiciansData physiciandata, string AdminId)
        {
            try
            {
                if (physiciandata.UserName != null && physiciandata.PassWord != null)
                {
                    // ASP_User
                    var Aspnetuser = new Aspnetuser();
                    var hasher = new PasswordHasher<string>();
                    Aspnetuser.Id = Guid.NewGuid().ToString();
                    Aspnetuser.Username = physiciandata.UserName;
                    Aspnetuser.Passwordhash = hasher.HashPassword(null, physiciandata.PassWord);
                    Aspnetuser.Email = physiciandata.Email;
                    Aspnetuser.CreatedDate = DateTime.Now;
                    _context.Aspnetusers.Add(Aspnetuser);
                    _context.SaveChanges();

                    //aspnet_user_roles
                    var aspnetuserroles = new Aspnetuserrole();
                    aspnetuserroles.Userid = Aspnetuser.Id;
                    aspnetuserroles.Roleid = "Provider";
                    _context.Aspnetusers.Add(Aspnetuser);
                    _context.SaveChanges();

                    // Physician
                    var Physician = new Physician();
                    Physician.Aspnetuserid = Aspnetuser.Id;
                    Physician.Firstname = physiciandata.Firstname;
                    Physician.Lastname = physiciandata.Lastname;
                    Physician.Status = physiciandata.Status;
                    Physician.Roleid = physiciandata.Roleid;
                    Physician.Email = physiciandata.Email;
                    Physician.Mobile = physiciandata.Mobile;
                    Physician.Medicallicense = physiciandata.Medicallicense;
                    Physician.Npinumber = physiciandata.Npinumber;
                    Physician.Syncemailaddress = physiciandata.Syncemailaddress;
                    Physician.Address1 = physiciandata.Address1;
                    Physician.Address2 = physiciandata.Address2;
                    Physician.City = physiciandata.City;
                    Physician.Zip = physiciandata.Zipcode;
                    Physician.Altphone = physiciandata.Altphone;
                    Physician.Businessname = physiciandata.Businessname;
                    Physician.Businesswebsite = physiciandata.Businesswebsite;
                    Physician.Createddate = DateTime.Now;
                    Physician.Createdby = AdminId;
                    Physician.Regionid = physiciandata.Regionid;

                    Physician.Isagreementdoc = new BitArray(1);
                    Physician.Isbackgrounddoc = new BitArray(1);
                    Physician.Isnondisclosuredoc = false;
                    Physician.Islicensedoc = new BitArray(1);
                    Physician.Istrainingdoc = new BitArray(1);
                    Physician.Isdeleted = new BitArray(1);

                    Physician.Isagreementdoc[0] = physiciandata.Isagreementdoc;
                    Physician.Isbackgrounddoc[0] = physiciandata.Isbackgrounddoc;
                    Physician.Isnondisclosuredoc = physiciandata.Isnondisclosuredoc;
                    Physician.Islicensedoc[0] = physiciandata.Islicensedoc;
                    Physician.Istrainingdoc[0] = physiciandata.Istrainingdoc;
                    Physician.Isdeleted[0] = false;
                    Physician.Adminnotes = physiciandata.Adminnotes;


                    Physician.Photo = physiciandata.PhotoFile != null ? Physician.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-Photo." + Path.GetExtension(physiciandata.PhotoFile.FileName).Trim('.') : null;
                    Physician.Signature = physiciandata.SignatureFile != null ? Physician.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-Signature.png" : null;



                    _context.Physicians.Add(Physician);
                    _context.SaveChanges();

                    FileSave.UploadProviderDoc(physiciandata.Agreementdoc, Physician.Physicianid, "Agreementdoc.pdf");
                    FileSave.UploadProviderDoc(physiciandata.BackGrounddoc, Physician.Physicianid, "BackGrounddoc.pdf");
                    FileSave.UploadProviderDoc(physiciandata.NonDisclosuredoc, Physician.Physicianid, "NonDisclosuredoc.pdf");
                    FileSave.UploadProviderDoc(physiciandata.Licensedoc, Physician.Physicianid, "Agreementdoc.pdf");
                    FileSave.UploadProviderDoc(physiciandata.Trainingdoc, Physician.Physicianid, "Trainingdoc.pdf");

                    FileSave.UploadProviderDoc(physiciandata.SignatureFile, Physician.Physicianid, Physician.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-Signature.png");
                    FileSave.UploadProviderDoc(physiciandata.PhotoFile, Physician.Physicianid, Physician.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-Photo." + Path.GetExtension(physiciandata.PhotoFile.FileName).Trim('.'));

                    // Physician_region
                    List<int> priceList = physiciandata.Regionsid.Split(',').Select(int.Parse).ToList();
                    foreach (var item in priceList)
                    {
                        Physicianregion ar = new Physicianregion();
                        ar.Regionid = item;
                        ar.Physicianid = (int)Physician.Physicianid;
                        _context.Physicianregions.Add(ar);
                        _context.SaveChanges();

                    }
                }
                else
                {

                }
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }
        #endregion
    }
}
