using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HalloDoc.Entity.Models.PatientModels;
using HallodocMVC.Repository.Patient.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HallodocMVC.Repository.Patient.Repository
{
    public class Profile : IProfile
    {
        private readonly HalloDocContext _context;

        public Profile(HalloDocContext context)
        {
            _context = context;
        }

        #region ProviderProfile
        public ViewDataUserProfile? ProviderProfile(string id)
        {
            if (id != null)
            {
                ViewDataUserProfile? UsersProfile = _context.Users
                                .Where(r => Convert.ToString(r.Aspnetuserid) == id)
                                .Select(r => new ViewDataUserProfile
                                {
                                    Userid = r.Userid,
                                    Firstname = r.Firstname,
                                    Lastname = r.Lastname,
                                    Mobile = r.Mobile,
                                    Email = r.Email,
                                    Street = r.Street,
                                    State = r.Regionid,
                                    City = r.City,
                                    Zipcode = r.Zipcode,
                                    Birthdate = new DateTime((int)r.Intyear, DateTime.ParseExact(r.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)r.Intdate),
                                })
                                .FirstOrDefault();

                return UsersProfile;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Edit
        public async Task<bool> Edit(ViewDataUserProfile userprofile, string id)
        {
            
            try
            {
                var isUserExist = _context.Aspnetusers.FirstOrDefault(x => x.Email == userprofile.Email && x.Id != id);
                if(isUserExist == null)
                {
                    var statename = _context.Regions.FirstOrDefault(x => x.Regionid == userprofile.State);
                    User userToUpdate = await _context.Users.FindAsync(userprofile.Userid);
                    userToUpdate.Firstname = userprofile.Firstname;
                    userToUpdate.Lastname = userprofile.Lastname;
                    userToUpdate.Mobile = userprofile.Mobile;
                    userToUpdate.Email = userprofile.Email;
                    userToUpdate.Regionid = userprofile.State;
                    userToUpdate.State = statename.Name;
                    userToUpdate.Street = userprofile.Street;
                    userToUpdate.City = userprofile.City;
                    userToUpdate.Zipcode = userprofile.Zipcode;
                    userToUpdate.Intdate = userprofile.Birthdate.Day;
                    userToUpdate.Intyear = userprofile.Birthdate.Year;
                    userToUpdate.Strmonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(userprofile.Birthdate.Month);
                    userToUpdate.Modifiedby = userprofile.Createdby;
                    userToUpdate.Modifieddate = DateTime.Now;
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();

                    Aspnetuser user = await _context.Aspnetusers.FindAsync(id);
                    user.Email = userprofile.Email;
                    user.Modifieddate = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
        #endregion
    }
}