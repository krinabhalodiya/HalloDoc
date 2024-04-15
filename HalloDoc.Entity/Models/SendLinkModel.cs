using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class SendLinkModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; } = null!;
        [Required(ErrorMessage = "Lastname is required")]
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"([0-9]{10})", ErrorMessage = "Please enter 10 digits for a contact number")]
        public string? Mobile { get; set; }
    }
}
