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
        #region PartnersByProfession
        public VendorsPagination PartnersByProfession(VendorsPagination model)
        {
            List<VendorsData> data =  (from v in _context.Healthprofessionals
                                            join type in _context.Healthprofessionaltypes
                                            on v.Profession equals type.Healthprofessionalid into VendorGroup
                                            from vg in VendorGroup.DefaultIfEmpty()
                                            where v.Isdeleted == new BitArray(1) && (!model.searchprofessions.HasValue || v.Profession == model.searchprofessions) &&
                                            (model.searchvendors == null || v.Vendorname.Contains(model.searchvendors) || vg.Professionname.Contains(model.searchvendors) || v.Email.Contains(model.searchvendors) || v.Faxnumber.Contains(model.searchvendors) || v.Businesscontact.Contains(model.searchvendors))
                                            select new VendorsData
                                            {
                                                VendorId = v.Vendorid,
                                                VendorName = v.Vendorname,
                                                FaxNumber = v.Faxnumber,
                                                Address = v.Address,
                                                City = v.City,
                                                State = (int)v.Regionid,
                                                ZipCode = v.Zip,
                                                CreatedDate = v.Createddate,
                                                Email = v.Email,
                                                BusinessContact = v.Businesscontact,
                                                BusinessName = vg.Professionname
                                            }).ToList();
            int totalItemCount = data.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)model.PageSize);
            List<VendorsData> list = data.Skip((model.CurrentPage - 1) * model.PageSize).Take(model.PageSize).ToList();

            VendorsPagination pagination = new()
            {
                Vendors = list,
                CurrentPage = model.CurrentPage,
                PageSize = 5,
                TotalPages = totalPages
            };
            return pagination;
        }
        #endregion PartnersByProfession

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
                                           State = (int)v.Regionid,
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
                var statename = _context.Regions.FirstOrDefault(x => x.Regionid == data.State);
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
                        result.Regionid = data.State;
                        result.State = statename.Name;
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
                        Regionid = data.State,
                        State = statename.Name,
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
