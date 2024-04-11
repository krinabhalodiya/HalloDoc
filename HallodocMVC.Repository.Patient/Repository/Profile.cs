using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
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
                                    State = r.State,
                                    City = r.City,
                                    Zipcode = r.Zipcode,
                                    Birthdate = new DateTime((int)r.Intyear, Convert.ToInt32(r.Strmonth.Trim()), (int)r.Intdate),
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
        public async Task<bool> Edit(ViewDataUserProfile userprofile)
        {
            try
            {
                User userToUpdate = await _context.Users.FindAsync(userprofile.Userid);

                userToUpdate.Firstname = userprofile.Firstname;
                userToUpdate.Lastname = userprofile.Lastname;
                userToUpdate.Mobile = userprofile.Mobile;
                userToUpdate.Email = userprofile.Email;
                userToUpdate.State = userprofile.State;
                userToUpdate.Street = userprofile.Street;
                userToUpdate.City = userprofile.City;
                userToUpdate.Zipcode = userprofile.Zipcode;
                userToUpdate.Intdate = userprofile.Birthdate.Day;
                userToUpdate.Intyear = userprofile.Birthdate.Year;
                userToUpdate.Strmonth = userprofile.Birthdate.Month.ToString();
                userToUpdate.Modifiedby = userprofile.Createdby;
                userToUpdate.Modifieddate = DateTime.Now;
                _context.Update(userToUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
        #endregion
    }
}