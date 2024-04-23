using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HalloDoc.Entity.Models.ViewDocuments;

namespace HalloDoc.Entity.Models
{
    public class ViewCloseCaseModel
    {
        public List<Documents> documentslist { get; set; } = null;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ConfirmationNumber { get; set; }
        public int RequestID { get; set; }
        public int RequestWiseFileID { get; set; }
        public string RC_FirstName { get; set; }
        public string RC_LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string RC_Email { get; set; } 
        public DateTime RC_Dob { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        public string RC_PhoneNumber { get; set; }
        public int RequestClientID { get; set; }
    }
}
