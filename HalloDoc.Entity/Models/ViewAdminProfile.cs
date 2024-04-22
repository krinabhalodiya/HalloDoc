using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class ViewAdminProfile
    {
        public int? AdminId { get; set; }
        public string? Aspnetuserid { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public short? Status { get; set; }
        public int? Roleid { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string Email { get; set; }
        [Compare("Email", ErrorMessage = "Email and Confirm Email must match")]
        public string confirmEmail { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"([0-9]{10})", ErrorMessage = "Please enter 10 digits for a contact number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"([0-9]{10})", ErrorMessage = "Please enter 10 digits for a contact number")]
        public string AltMobile { get; set; }
        [Required(ErrorMessage = "Address1 is required")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Address2 is required")]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public int State { get; set; }
        public int? Regionid { get; set; }
        public string? Regionsid { get; set; }
        public List<Regions>? Regionids { get; set; }
        public string? Zipcode { get; set; }
        public string? Createdby { get; set; } = null!;
        public DateTime? Createddate { get; set; }
        public string? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string? Ip { get; set; }
        public class Regions
        {
            public int? regionid { get; set; }
            public string? regionname { get; set; }
        }
    }
}
