﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;

namespace HallodocMVC.Repository.Admin.Repository.Interface
{
    public interface IComboboxRepository
    {
        Task<List<RegionComboBox>> RegionComboBox();
        Task<List<CaseReasonComboBox>> CaseReasonComboBox();
        public List<Physician> ProviderbyRegion(int? regionid);
        public Task<List<HealthProfessionalTypeComboBox>> healthprofessionaltype();
        public List<HealthProfessionalComboBox> ProfessionalByType(int? HealthprofessionalID);
        public Task<List<UserRoleCombobox>> UserRoleComboBox();
        public Task<List<UserRoleCombobox>> PhysicianRoleComboBox();
        public Task<List<UserRoleCombobox>> AdminRoleComboBox();
    }
}