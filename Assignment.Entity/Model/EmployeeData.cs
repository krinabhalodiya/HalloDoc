
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;

namespace Assignment.Entity.Model
{
    public class EmployeeData
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DeptId { get; set; }
        public string? DeptName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Education { get; set; }
        public string? Company { get; set; }
        public string? Experience { get; set; }
        public string? Gender { get; set; }
        public string? Package { get; set; }
        
    }
    public class PaginatedViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }
        public int? DeptId { get; set; }
        [Required(ErrorMessage = "DeptName is required")]
        public string? DeptName { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Email Is Required!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Education is required")]
        public string? Education { get; set; }
        [Required(ErrorMessage = "Company is required")]
        public string? Company { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public string? Experience { get; set; }
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Package is required")]
        public string? Package { get; set; }
        public List<EmployeeData>? Employees { get; set; } = null;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SearchInput { get; set; }
        public string? viewname { get; set; }
        public int totalentries { get; set; }
        public string curententries { get; set; }
}
}
