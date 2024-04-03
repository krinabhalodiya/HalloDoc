﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class ViewUserAcces
    {
        public int? UserID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public short? accounttype { get; set; }
        public short? status { get; set; }
        public int? OpenRequest { get; set; }
        public string? Mobile { get; set; }
        public bool isAdmin { get; set; }
        public int ? PhysicianId { get; set; }

	}
}
