using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IPartnersRepository
    {
        public Task<List<Healthprofessional>> GetPartnersByProfession(int? regionId);
    }
}
