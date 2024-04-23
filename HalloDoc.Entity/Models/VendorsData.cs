using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Entity.Models
{
    public class VendorsData
    {
        public int VendorId { get; set; }
        [Required(ErrorMessage = "VendorName is required")]
        public string VendorName { get; set; }
        [Required(ErrorMessage = "FaxNumber is required")]
        public string FaxNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public int State { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Select at least one Profession!")]
        public int ProfessionId { get; set; }
        [Required(ErrorMessage = "Business Contact is required")]
        public string BusinessContact { get; set; }
        [Required(ErrorMessage = "Business Name is required")]
        public string BusinessName { get; set; }
    }
    public class VendorsPagination
    {
        public List<VendorsData> Vendors { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int? searchprofessions { get; set; }
        public string? searchvendors { get; set; }
    }
}
