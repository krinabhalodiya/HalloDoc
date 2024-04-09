﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class VendorsData
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public int ProfessionId { get; set; }
        public string BusinessContact { get; set; }
        public string BusinessName { get; set; }
    }
}
