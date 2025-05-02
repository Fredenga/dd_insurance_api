using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InsuranceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admin__43AA41419DD66B97", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    agent_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    commission_rate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "active"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Agent__2C05379E935094BA", x => x.agent_id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    kyc_status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "pending"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__CD65CB8599172787", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    admin_id = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    entity_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    entity_id = table.Column<int>(type: "integer", nullable: false),
                    ip_address = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditLog__9E2397E086A2209D", x => x.log_id);
                    table.ForeignKey(
                        name: "FK__AuditLog__admin___6383C8BA",
                        column: x => x.admin_id,
                        principalTable: "Admin",
                        principalColumn: "admin_id");
                });

            migrationBuilder.CreateTable(
                name: "EmailVerificationToken",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AdminId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationToken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailVerificationToken_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "admin_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProduct",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    base_premium = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__47027DF51A7902E0", x => x.product_id);
                    table.ForeignKey(
                        name: "FK__Insurance__creat__3F466844",
                        column: x => x.created_by,
                        principalTable: "Admin",
                        principalColumn: "admin_id");
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    policy_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    policy_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    agent_id = table.Column<int>(type: "integer", nullable: true),
                    premium = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "pending"),
                    approved_by = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Policy__47DA3F0352A02FAB", x => x.policy_id);
                    table.ForeignKey(
                        name: "FK__Policy__agent_id__534D60F1",
                        column: x => x.agent_id,
                        principalTable: "Agent",
                        principalColumn: "agent_id");
                    table.ForeignKey(
                        name: "FK__Policy__approved__5535A963",
                        column: x => x.approved_by,
                        principalTable: "Admin",
                        principalColumn: "admin_id");
                    table.ForeignKey(
                        name: "FK__Policy__customer__52593CB8",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK__Policy__product___5165187F",
                        column: x => x.product_id,
                        principalTable: "InsuranceProduct",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    claim_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    policy_id = table.Column<int>(type: "integer", nullable: false),
                    claim_date = table.Column<DateTime>(type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "pending"),
                    processed_by = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Claim__F9CC08965B622247", x => x.claim_id);
                    table.ForeignKey(
                        name: "FK__Claim__policy_id__5BE2A6F2",
                        column: x => x.policy_id,
                        principalTable: "Policy",
                        principalColumn: "policy_id");
                    table.ForeignKey(
                        name: "FK__Claim__processed__5DCAEF64",
                        column: x => x.processed_by,
                        principalTable: "Admin",
                        principalColumn: "admin_id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Admin_Email",
                table: "Admin",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Admin_Username",
                table: "Admin",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_agent_name",
                table: "Agent",
                columns: new[] { "first_name", "last_name" });

            migrationBuilder.CreateIndex(
                name: "UQ_Agent_Email",
                table: "Agent",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_audit_timestamp",
                table: "AuditLog",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_admin_id",
                table: "AuditLog",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "idx_claim_status",
                table: "Claim",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_policy_id",
                table: "Claim",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_processed_by",
                table: "Claim",
                column: "processed_by");

            migrationBuilder.CreateIndex(
                name: "idx_customer_name",
                table: "Customer",
                columns: new[] { "first_name", "last_name" });

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__AB6E6164F45352DE",
                table: "Customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationToken_AdminId",
                table: "EmailVerificationToken",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProduct_created_by",
                table: "InsuranceProduct",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "idx_policy_dates",
                table: "Policy",
                columns: new[] { "start_date", "end_date" });

            migrationBuilder.CreateIndex(
                name: "idx_policy_number",
                table: "Policy",
                column: "policy_number");

            migrationBuilder.CreateIndex(
                name: "idx_policy_status",
                table: "Policy",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_agent_id",
                table: "Policy",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_approved_by",
                table: "Policy",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_customer_id",
                table: "Policy",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_product_id",
                table: "Policy",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Policy_PolicyNumber",
                table: "Policy",
                column: "policy_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "EmailVerificationToken");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "InsuranceProduct");

            migrationBuilder.DropTable(
                name: "Admin");
        }
    }
}
