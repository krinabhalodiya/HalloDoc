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
                    aspnetuserroles.Roleid = "2";
                    _context.Aspnetuserroles.Add(aspnetuserroles);
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

        #region GetPhysicianById
        public async Task<PhysiciansData> GetPhysicianById(int id)
        {


            PhysiciansData? pl = await (from r in _context.Physicians
                                   join Aspnetuser in _context.Aspnetusers
                                   on r.Aspnetuserid equals Aspnetuser.Id into aspGroup
                                   from asp in aspGroup.DefaultIfEmpty()
                                   join Notifications in _context.Physiciannotifications
                                    on r.Physicianid equals Notifications.Physicianid into PhyNGroup
                                   from nof in PhyNGroup.DefaultIfEmpty()
                                   join role in _context.Roles
                                   on r.Roleid equals role.Roleid into roleGroup
                                   from roles in roleGroup.DefaultIfEmpty()
                                   where r.Physicianid == id
                                   select new PhysiciansData
                                   {
                                       UserName = asp.Username,
                                       Roleid = r.Roleid,
                                       Status = r.Status,
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
                                       Email = r.Email,
                                       Photo = r.Photo,
                                       Signature = r.Signature,
                                       Isagreementdoc = r.Isagreementdoc[0],
                                       Isnondisclosuredoc = r.Isnondisclosuredoc == false ? false : true,
                                       Isbackgrounddoc = r.Isbackgrounddoc[0],
                                       Islicensedoc = r.Islicensedoc[0],
                                       Istrainingdoc = r.Istrainingdoc[0],
                                       Medicallicense = r.Medicallicense,
                                       Npinumber = r.Npinumber,
                                       Syncemailaddress = r.Syncemailaddress,
                                       Zipcode = r.Zip,
                                       Regionid = r.Regionid

                                   })
                                   .FirstOrDefaultAsync();

            List<PhysiciansData.Regions> regions = new List<PhysiciansData.Regions>();

            regions = _context.Physicianregions
                  .Where(r => r.Physicianid == pl.Physicianid)
                  .Select(req => new PhysiciansData.Regions()
                  {
                      regionid = req.Regionid
                  })
                  .ToList();

            pl.Regionids = regions;
            return pl;
        }
        #endregion

        #region SavePhysicianInfo
        public async Task<bool> EditAccountInfo(PhysiciansData vm)
        {
            try
            {
                if (vm == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Physicians
                        .Where(W => W.Physicianid == vm.Physicianid)
                        .FirstOrDefaultAsync();
                    Aspnetuser User = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Id == DataForChange.Aspnetuserid);
                    if (DataForChange != null && User!=null)
                    {
                        User.Username = vm.UserName;
                        DataForChange.Status = vm.Status;
                        DataForChange.Roleid = vm.Roleid;
                        _context.Physicians.Update(DataForChange);
                        _context.Aspnetusers.Update(User);
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

        #region Change_Password
        public async Task<bool> ChangePasswordAsync(string password, int Physicianid)
        {
            var hasher = new PasswordHasher<string>();
            var req = await _context.Physicians
                .Where(W => W.Physicianid == Physicianid)
                    .FirstOrDefaultAsync();
            if (req != null)
            {
                var User = await _context.Aspnetusers.Where(m => m.Id == req.Aspnetuserid).FirstOrDefaultAsync();
                if(User != null)
                {
                    User.Passwordhash = hasher.HashPassword(null, password);
                    _context.Aspnetusers.Update(User);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
        #endregion

        #region EditPhysicianInfo
        public async Task<bool> EditPhysicianInfo(PhysiciansData vm)
        {
            try
            {
                if (vm == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Physicians
                        .Where(W => W.Physicianid == vm.Physicianid)
                        .FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        DataForChange.Firstname = vm.Firstname;
                        DataForChange.Lastname = vm.Lastname;
                        DataForChange.Email = vm.Email;
                        DataForChange.Mobile = vm.Mobile;
                        DataForChange.Medicallicense = vm.Medicallicense;
                        DataForChange.Npinumber = vm.Npinumber;
                        DataForChange.Syncemailaddress = vm.Syncemailaddress;
                        _context.Physicians.Update(DataForChange);
                        _context.SaveChanges();
                        List<int> regions = await _context.Physicianregions.Where(r => r.Physicianid == vm.Physicianid).Select(req => req.Regionid).ToListAsync();
                        List<int> priceList = vm.Regionsid.Split(',').Select(int.Parse).ToList();
                        foreach (var item in priceList)
                        {
                            if (regions.Contains(item))
                            {
                                regions.Remove(item);
                            }
                            else
                            {
                                Physicianregion pr = new()
                                {
                                    Regionid = item,
                                    Physicianid = (int)vm.Physicianid
                                };
                                _context.Physicianregions.Update(pr);
                                await _context.SaveChangesAsync();
                                regions.Remove(item);
                            }
                        }
                        if (regions.Count > 0)
                        {
                            foreach (var item in regions)
                            {
                                Physicianregion pr = await _context.Physicianregions.Where(r => r.Physicianid == vm.Physicianid && r.Regionid == item).FirstAsync();
                                _context.Physicianregions.Remove(pr);
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
        #region EditMailBillingInfo
        public async Task<bool> EditMailBillingInfo(PhysiciansData vm, string AdminId)
        {
            try
            {
                if (vm == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Physicians
                        .Where(W => W.Physicianid == vm.Physicianid)
                        .FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        DataForChange.Address1 = vm.Address1;
                        DataForChange.Address2 = vm.Address2;
                        DataForChange.City = vm.City;
                        DataForChange.Regionid = vm.Regionid;
                        DataForChange.Zip = vm.Zipcode;
                        DataForChange.Altphone = vm.Altphone;
                        DataForChange.Modifiedby = AdminId;
                        DataForChange.Modifieddate = DateTime.Now;
                        _context.Physicians.Update(DataForChange);
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
        #region EditProviderProfile
        public async Task<bool> EditProviderProfile(PhysiciansData vm, string AdminId)
        {
            try
            {
                if (vm == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Physicians
                        .Where(W => W.Physicianid == vm.Physicianid)
                        .FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        if (vm.PhotoFile != null)
                        {
                            DataForChange.Photo = vm.PhotoFile != null ? vm.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmm") + "-Photo." + Path.GetExtension(vm.PhotoFile.FileName).Trim('.') : null;
                            FileSave.UploadProviderDoc(vm.PhotoFile, (int)vm.Physicianid, vm.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmm") + "-Photo." + Path.GetExtension(vm.PhotoFile.FileName).Trim('.'));

                        }
                        if (vm.SignatureFile != null)
                        {
                            DataForChange.Signature = vm.SignatureFile != null ? vm.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmm") + "-Signature.png" : null;
                            FileSave.UploadProviderDoc(vm.SignatureFile, (int)vm.Physicianid, vm.Firstname + "-" + DateTime.Now.ToString("yyyyMMddhhmm") + "-Signature.png");
                        }
                        DataForChange.Businessname = vm.Businessname;
                        DataForChange.Businesswebsite = vm.Businesswebsite;
                        DataForChange.Modifiedby = AdminId;
                        DataForChange.Adminnotes = vm.Adminnotes;
                        DataForChange.Modifieddate = DateTime.Now;
                        _context.Physicians.Update(DataForChange);
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
        #region EditProviderOnbording
        public async Task<bool> EditProviderOnbording(PhysiciansData vm, string AdminId)
        {
            try
            {
                if (vm == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Physicians
                        .Where(W => W.Physicianid == vm.Physicianid)
                        .FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        FileSave.UploadProviderDoc(vm.Agreementdoc, (int)vm.Physicianid, "Agreementdoc.pdf");
                        FileSave.UploadProviderDoc(vm.BackGrounddoc, (int)vm.Physicianid, "BackGrounddoc.pdf");
                        FileSave.UploadProviderDoc(vm.NonDisclosuredoc, (int)vm.Physicianid, "NonDisclosuredoc.pdf");
                        FileSave.UploadProviderDoc(vm.Licensedoc, (int)vm.Physicianid, "Agreementdoc.pdf");
                        FileSave.UploadProviderDoc(vm.Trainingdoc, (int)vm.Physicianid, "Trainingdoc.pdf");

                        DataForChange.Isagreementdoc = new BitArray(1);
                        DataForChange.Isbackgrounddoc = new BitArray(1);
                        DataForChange.Isnondisclosuredoc = false;
                        DataForChange.Islicensedoc = new BitArray(1);
                        DataForChange.Istrainingdoc = new BitArray(1);

                        DataForChange.Isagreementdoc[0] = vm.Isagreementdoc;
                        DataForChange.Isbackgrounddoc[0] = vm.Isbackgrounddoc;
                        DataForChange.Isnondisclosuredoc = vm.Isnondisclosuredoc;
                        DataForChange.Islicensedoc[0] = vm.Islicensedoc;
                        DataForChange.Istrainingdoc[0] = vm.Istrainingdoc;
                        DataForChange.Modifiedby = AdminId;
                        DataForChange.Modifieddate = DateTime.Now;

                        _context.Physicians.Update(DataForChange);
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
        #region DeletePhysician
        public async Task<bool> DeletePhysician(int PhysicianID, string AdminID)
        {
            try
            {
                BitArray bt = new BitArray(1);
                bt.Set(0, true);
                if (PhysicianID == null)
                {
                    return false;
                }
                else
                {
                    var DataForChange = await _context.Physicians
                        .Where(W => W.Physicianid == PhysicianID)
                        .FirstOrDefaultAsync();
                    if (DataForChange != null)
                    {
                        DataForChange.Isdeleted = bt;
                        DataForChange.Isdeleted[0] = true;
                        DataForChange.Modifieddate = DateTime.Now;
                        DataForChange.Modifiedby = AdminID;
                        _context.Physicians.Update(DataForChange);
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
