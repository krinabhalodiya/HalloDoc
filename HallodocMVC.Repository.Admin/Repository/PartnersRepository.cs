using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
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
    }
}
