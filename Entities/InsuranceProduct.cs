using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Entities;

[Table("InsuranceProduct")]
public partial class InsuranceProduct
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("product_name")]
    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    [Column("category")]
    [StringLength(50)]
    public string Category { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    // Use 'numeric' instead of 'decimal' in PostgreSQL
    [Column("base_premium", TypeName = "numeric(10,2)")]
    public decimal BasePremium { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("InsuranceProducts")]
    public virtual Admin? CreatedByNavigation { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
