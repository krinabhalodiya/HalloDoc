using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HalloDoc.Entity.Models.Constant;

namespace HalloDoc.Entity.Models.PatientModels
{
    public class PatientDashList
    {
        public int UserId { get; set; }
        public DateTime createdDate { get; set; }
        public short Status { get; set; }
        public int RequestId { get; set; }
        public int DocumentCount { get; set; }
        public List<PatientDashList>? patientdata { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public bool? IsAscending { get; set; } = false;
    }

}
