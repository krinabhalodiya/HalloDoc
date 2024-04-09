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
    public class PartnersRepository : IPartnersRepository
    {
        private readonly HalloDocContext _context;

        public PartnersRepository(HalloDocContext context)
        {
            _context = context;
        }
        #region GetPartnersByProfession
        public async Task<List<Healthprofessional>> GetPartnersByProfession(int? regionId)
        {
            List<Healthprofessional> pl = await (from r in _context.Healthprofessionals
                                                 where r.Isdeleted == new BitArray(1) && (!regionId.HasValue || r.Regionid == regionId)
                                                 select r)
                                        .ToListAsync();
            return pl;
        }
        #endregion
        #region BusinessById
        public async Task<VendorsData> BusinessById(int? VendorId)
        {
            VendorsData? data = await (from v in _context.Healthprofessionals
                                       join type in _context.Healthprofessionaltypes
                                       on v.Profession equals type.Healthprofessionalid into VendorGroup
                                       from vg in VendorGroup.DefaultIfEmpty()
                                       where v.Vendorid == VendorId
                                       select new VendorsData
                                       {
                                           VendorId = v.Vendorid,
                                           VendorName = v.Vendorname,
                                           ProfessionId = (int)v.Profession,
                                           FaxNumber = v.Faxnumber,
                                           Address = v.Address,
                                           City = v.City,
                                           State = v.State,
                                           ZipCode = v.Zip,
                                           CreatedDate = v.Createddate,
                                           Email = v.Email,
                                           BusinessContact = v.Businesscontact,
                                           PhoneNumber = v.Phonenumber,
                                           BusinessName = vg.Professionname
                                       }).FirstOrDefaultAsync();
            return data;
        }
        #endregion BusinessById

        #region AddEditBusiness
        public bool AddEditBusiness(VendorsData data)
        {
            try
            {
                if (data.VendorId != 0)
                {
                    var result = _context.Healthprofessionals.Where(hp => hp.Vendorid == data.VendorId).FirstOrDefault();
                    if (result != null)
                    {

                        result.Vendorname = data.VendorName;
                        result.Profession = data.ProfessionId;
                        result.Faxnumber = data.FaxNumber;
                        result.Address = data.Address;
                        result.City = data.City;
                        result.State = data.State;
                        result.Zip = data.ZipCode;
                        result.Createddate = DateTime.Now;
                        result.Phonenumber = data.PhoneNumber;
                        result.Isdeleted = new BitArray(1);
                        result.Email = data.Email;
                        result.Businesscontact = data.BusinessContact;
                        result.Modifieddate = DateTime.Now;

                        _context.Healthprofessionals.Update(result);
                        _context.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Healthprofessional hp = new()
                    {
                        Vendorname = data.VendorName,
                        Profession = data.ProfessionId,
                        Faxnumber = data.FaxNumber,
                        Address = data.Address,
                        City = data.City,
                        State = data.State,
                        Zip = data.ZipCode,
                        Createddate = DateTime.Now,
                        Phonenumber = data.PhoneNumber,
                        Isdeleted = new BitArray(1),
                        Email = data.Email,
                        Businesscontact = data.BusinessContact
                    };
                    _context.Healthprofessionals.Add(hp);
                    _context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion AddEditBusiness

        #region DeleteVendor
        public bool DeleteVendor(int? VendorId)
        {
            try
            {
                var data = _context.Healthprofessionals.FirstOrDefault(v => v.Vendorid == VendorId);
                data.Isdeleted = new BitArray(1);
                data.Isdeleted[0] = true;
                _context.Healthprofessionals.Update(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion DeleteVendor
    }
}
