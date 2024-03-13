﻿using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloDocAdmin.Repositories
{
    public class ComboboxRepository : IComboboxRepository
    {
        private readonly HalloDocContext _context;

        public ComboboxRepository(HalloDocContext context)
        {
            _context = context;

        }
        public async Task<List<RegionComboBox>> RegionComboBox()
        {
            return await _context.Regions.Select(req => new RegionComboBox()
            {
                RegionId = req.Regionid,
                RegionName = req.Name
            })
                .ToListAsync();
        }
        public async Task<List<CaseReasonComboBox>> CaseReasonComboBox()
        {
            return await _context.Casetags.Select(req => new CaseReasonComboBox()
            {
                CaseReasonId = req.Casetagid,
                CaseReasonName = req.Name
            })
                .ToListAsync();
        }
        #region Provider_By_Region
        public List<Physician> ProviderbyRegion(int? regionid)
        {
            var result = _context.Physicians
                        .Where(r => r.Regionid == regionid)
                        .OrderByDescending(x => x.Createddate)
                        .ToList();

            return result;
        }
        #endregion
        public async Task<List<HealthProfessionalTypeComboBox>> healthprofessionaltype()
        {
            return await _context.Healthprofessionaltypes.Select(req => new HealthProfessionalTypeComboBox()
            {
                HealthprofessionalID = req.Healthprofessionalid,
                ProfessionName = req.Professionname
            })
            .ToListAsync();
        }
        /*public async Task<List<HealthProfessionalComboBox>> healthprofessionals()
        {
            return await _context.Healthprofessionals.Select(req => new HealthProfessionalComboBox()
            {
                VendorID = req.Vendorid,
                VendorName = req.Vendorname
            })
            .ToListAsync();
        }*/
        public List<HealthProfessionalComboBox> ProfessionalByType(int? HealthprofessionalID)
        {
            var result = _context.Healthprofessionals
                        .Where(r => r.Profession == HealthprofessionalID)
                        .Select(req => new HealthProfessionalComboBox()
                        {
                            VendorID = req.Vendorid,
                            VendorName = req.Vendorname
                        }).ToList();
            return result;
        }
    }
}