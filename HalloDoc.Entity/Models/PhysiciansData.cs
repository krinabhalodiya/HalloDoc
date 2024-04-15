using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HalloDoc.Entity.Models
{
    public class PhysiciansData
    {
        public int? notificationid { get; set; }
        public BitArray? notification { get; set; }
        public int? onCallStatus { get; set; } = 0;
        public int? shiftid { get; set; }
        public string? role { get; set; }
        public int? Physicianid { get; set; }
        public string? Aspnetuserid { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? Regionsid { get; set; }
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
        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }
        [Required(ErrorMessage = "Zipcode is required")]
        public string? Zipcode { get; set; }
        [Required(ErrorMessage = "Medicallicense is required")]
        public string? Medicallicense { get; set; }
        public string? Photo { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string? Adminnotes { get; set; }
        public bool Isagreementdoc { get; set; }
        public bool Isbackgrounddoc { get; set; }
        public bool Istrainingdoc { get; set; }
        public bool Isnondisclosuredoc { get; set; }
        public bool Islicensedoc { get; set; }
        [Required(ErrorMessage = "Address1 is required")]
        public string? Address1 { get; set; }
        [Required(ErrorMessage = "Address2 is required")]
        public string? Address2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }
        public int? Regionid { get; set; }
        [Required(ErrorMessage = "Alternative phone number is required")]
        public string? Altphone { get; set; }
        public string? Createdby { get; set; } = null!;
        public DateTime? Createddate { get; set; }
        public string? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        [Required(ErrorMessage = "Select at least one status")]
        public short? Status { get; set; }
        [Required(ErrorMessage = "Businessname is required")]
        public string Businessname { get; set; } = null!;
        [Required(ErrorMessage = "Businesswebsite is required")]
        public string Businesswebsite { get; set; } = null!;
        public BitArray? Isdeleted { get; set; }
        [Required(ErrorMessage = "Select at least one role")]
        public int? Roleid { get; set; }
        [Required(ErrorMessage = "Npinumber is required")]
        public string? Npinumber { get; set; }
        public string? Signature { get; set; }
        public IFormFile? SignatureFile { get; set; }
        public BitArray? Iscredentialdoc { get; set; }
        public BitArray? Istokengenerate { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string? Syncemailaddress { get; set; }
        public IFormFile? Agreementdoc { get; set; }
        public IFormFile? NonDisclosuredoc { get; set; }
        public IFormFile? Trainingdoc { get; set; }
        public IFormFile? BackGrounddoc { get; set; }
        public IFormFile? Licensedoc { get; set; }
        public List<Regions>? Regionids { get; set; }
        public class Regions
        {
            public int? regionid { get; set; }
            public string? regionname { get; set; }
        }
    }
}