using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Entities;

[Table("Admin")]
[Index(nameof(Email), Name = "UQ_Admin_Email", IsUnique = true)]
[Index(nameof(Username), Name = "UQ_Admin_Username", IsUnique = true)]
public partial class Admin
{
    [Key]
    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("role")]
    [StringLength(20)]
    public string Role { get; set; } = null!;

    
    [Column("last_login", TypeName = "timestamp")]
    public DateTime? LastLogin { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    [InverseProperty("ProcessedByNavigation")]
    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<InsuranceProduct> InsuranceProducts { get; set; } = new List<InsuranceProduct>();

    [InverseProperty("ApprovedByNavigation")]
    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
