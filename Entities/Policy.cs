using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Entities;

[Table("Policy")]
[Index("PolicyNumber", Name = "UQ__Policy__96916872247AAA97", IsUnique = true)]
[Index("StartDate", "EndDate", Name = "idx_policy_dates")]
[Index("PolicyNumber", Name = "idx_policy_number")]
[Index("Status", Name = "idx_policy_status")]
public partial class Policy
{
    [Key]
    [Column("policy_id")]
    public int PolicyId { get; set; }

    [Column("policy_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string PolicyNumber { get; set; } = null!;

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("agent_id")]
    public int? AgentId { get; set; }

    [Column("premium", TypeName = "decimal(10, 2)")]
    public decimal Premium { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("status")]
    [StringLength(20)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Column("approved_by")]
    public int? ApprovedBy { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("Policies")]
    public virtual Agent? Agent { get; set; }

    [ForeignKey("ApprovedBy")]
    [InverseProperty("Policies")]
    public virtual Admin? ApprovedByNavigation { get; set; }

    [InverseProperty("Policy")]
    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Policies")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Policies")]
    public virtual InsuranceProduct Product { get; set; } = null!;
}
