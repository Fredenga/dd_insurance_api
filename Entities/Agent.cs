using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Entities;

[Table("Agent")]
[Index(nameof(Email), Name = "UQ_Agent_Email", IsUnique = true)]
[Index(nameof(FirstName), nameof(LastName), Name = "idx_agent_name")]
public partial class Agent
{
    [Key]
    [Column("agent_id")]
    public int AgentId { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    // Use PostgreSQL-compatible decimal mapping
    [Column("commission_rate", TypeName = "numeric(5,2)")]
    public decimal CommissionRate { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    
    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Agent")]
    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
