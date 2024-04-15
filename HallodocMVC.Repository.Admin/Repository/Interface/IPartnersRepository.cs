using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IPartnersRepository
    {
        public VendorsPagination PartnersByProfession(VendorsPagination model);
        public bool DeleteVendor(int? VendorId);
        public bool AddEditBusiness(VendorsData data);
        public Task<VendorsData> BusinessById(int? VendorId);
    }
}
