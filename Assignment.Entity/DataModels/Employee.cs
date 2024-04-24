using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Entity.DataModels;

[Table("Employee")]
public partial class Employee
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string? FirstName { get; set; }

    [StringLength(128)]
    public string? LastName { get; set; }

    public int? DeptId { get; set; }

    public int? Age { get; set; }

    [StringLength(128)]
    public string? Email { get; set; }

    [StringLength(123)]
    public string? Education { get; set; }

    [StringLength(128)]
    public string? Company { get; set; }

    [StringLength(128)]
    public string? Experience { get; set; }

    [StringLength(128)]
    public string? Package { get; set; }

    [StringLength(128)]
    public string? Gender { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsDelete { get; set; }

    [Column("Created Date", TypeName = "timestamp without time zone")]
    public DateTime? CreatedDate { get; set; }

    [Column("Modified Date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("Employees")]
    public virtual Department? Dept { get; set; }
}
