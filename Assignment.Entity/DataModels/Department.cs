using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Entity.DataModels;

[Table("Department")]
public partial class Department
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string? Name { get; set; }

    [InverseProperty("Dept")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
