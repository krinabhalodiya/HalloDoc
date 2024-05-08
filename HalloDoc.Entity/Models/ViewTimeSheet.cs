using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataModels;
using Microsoft.AspNetCore.Http;

namespace HalloDoc.Entity.Models
{
    public class ViewTimeSheet
    {
        public List<ViewTimesheetdetails>? ViewTimesheetdetails { get; set; }
        public List<ViewTimesheetdetailreimbursements>? ViewTimesheetdetailreimbursements { get; set; }
        public List<Payratebyprovider>? PayrateWithProvider { get; set; }
        public int Timesheeid { get; set; }
        public string? Bonus { get; set; }
        public string? AdminNotes { get; set; }
        public int PhysicianId { get; set; }
    }
    public class ViewTimesheetdetails
    {
        public int Timesheetdetailid { get; set; }

        public int Timesheetid { get; set; }

        public DateOnly Timesheetdate { get; set; }

        public int? OnCallhours { get; set; }
        public decimal? Totalhours { get; set; }

        public bool Isweekend { get; set; }

        public int? Numberofhousecall { get; set; }

        public int? Numberofphonecall { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifieddate { get; set; }
    }
    public class ViewTimesheetdetailreimbursements
    {
        public int? Timesheetdetailreimbursementid { get; set; } = null!;

        public int Timesheetdetailid { get; set; }
        public int Timesheetid { get; set; }

        public string Itemname { get; set; } = null!;

        public int? Amount { get; set; } = null!;
        public DateOnly Timesheetdate { get; set; }
        public string Bill { get; set; } = null!;
        public IFormFile Billfile { get; set; }

        public bool? Isdeleted { get; set; }

        public string Createdby { get; set; } = null!;

        public DateTime Createddate { get; set; }

        public string? Modifiedby { get; set; }
    }
}
