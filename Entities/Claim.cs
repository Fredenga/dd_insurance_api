using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Entities;

[Table("Claim")]
[Index(nameof(Status), Name = "idx_claim_status")]
public partial class Claim
{
    [Key]
    [Column("claim_id")]
    public int ClaimId { get; set; }

    [Column("policy_id")]
    public int PolicyId { get; set; }

   
    [Column("claim_date", TypeName = "date")]
    public DateTime ClaimDate { get; set; }

    // Use 'numeric' for PostgreSQL decimals
    [Column("amount", TypeName = "numeric(12,2)")]
    public decimal Amount { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("processed_by")]
    public int? ProcessedBy { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PolicyId")]
    [InverseProperty("Claims")]
    public virtual Policy Policy { get; set; } = null!;

    [ForeignKey("ProcessedBy")]
    [InverseProperty("Claims")]
    public virtual Admin? ProcessedByNavigation { get; set; }
}
