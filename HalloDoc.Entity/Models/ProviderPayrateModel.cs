using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class ProviderPayrateModel
    {
        public int PayrateId { get; set; }
        public int PhysicianId { get; set; }
        public int PayrateCategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Payrate { get; set; }
        public TimeOnly CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
