﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class Constant
    {
        public enum RequestType
        {
            Business = 1,
            Patient,
            Family,
            Concierge
        }
        public enum Status
        {
            Unassigne = 1, 
            Accepted, Cancelled, MDEnRoute, MDONSite, Conclude, CancelledByPatients, Closed, Unpaid, Clear,
            Block

        }
        public enum AdminDashStatus
        {
            New = 1,
            Pending,
            Active,
            Conclude,
            ToClose,
            UnPaid
        }
    }
}
