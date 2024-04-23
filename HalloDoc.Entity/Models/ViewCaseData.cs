using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class ViewCaseData
    {
        public int RequestID { get; set; }
		[Required(ErrorMessage = "Notes is required")]
		public string PatientNotes { get; set; }
        public string? ConfirmationNumber { get; set; }
		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Contact number is required")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Date Of Birth is required")]
		public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string Email { get; set; }
		[Required(ErrorMessage = "Region is required")]
		public string Region { get; set; }
		[Required(ErrorMessage = "Address is required")]
		public string Address { get; set; }
        public string? Room { get; set; }
        public int RequestTypeID { get; set; }
    }
}
