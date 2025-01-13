using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Entities;

[Table("AuditLog")]
[Index("Timestamp", Name = "idx_audit_timestamp")]
public partial class AuditLog
{
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }

    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("action")]
    [StringLength(50)]
    [Unicode(false)]
    public string Action { get; set; } = null!;

    [Column("entity_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string EntityType { get; set; } = null!;

    [Column("entity_id")]
    public int EntityId { get; set; }

    [Column("ip_address")]
    [StringLength(45)]
    [Unicode(false)]
    public string? IpAddress { get; set; }

    [Column("timestamp", TypeName = "datetime")]
    public DateTime? Timestamp { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("AuditLogs")]
    public virtual Admin Admin { get; set; } = null!;
}
