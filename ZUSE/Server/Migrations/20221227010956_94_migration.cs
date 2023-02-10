using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZUSE.Server.Migrations
{
    /// <inheritdoc />
    public partial class _94migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    businessreference = table.Column<string>(name: "business_reference", type: "nvarchar(450)", nullable: false),
                    branchreference = table.Column<string>(name: "branch_reference", type: "nvarchar(max)", nullable: false),
                    ordernumber = table.Column<string>(name: "order_number", type: "nvarchar(max)", nullable: true),
                    orderreference = table.Column<string>(name: "order_reference", type: "nvarchar(max)", nullable: true),
                    eventtype = table.Column<int>(name: "event_type", type: "int", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "DateTime", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    source = table.Column<int>(type: "int", nullable: false),
                    deliverystatus = table.Column<int>(name: "delivery_status", type: "int", nullable: false),
                    closedat = table.Column<DateTime>(name: "closed_at", type: "datetime2", nullable: true),
                    customernotes = table.Column<string>(name: "customer_notes", type: "nvarchar(max)", nullable: true),
                    kitchennotes = table.Column<string>(name: "kitchen_notes", type: "nvarchar(max)", nullable: true),
                    products = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    combos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productskitchennotes = table.Column<string>(name: "products_kitchen_notes", type: "nvarchar(max)", nullable: true),
                    customersatisfaction = table.Column<double>(name: "customer_satisfaction", type: "float", nullable: true),
                    customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tablename = table.Column<string>(name: "table_name", type: "nvarchar(max)", nullable: true),
                    browserid = table.Column<string>(name: "browser_id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => new { x.id, x.businessreference });
                });

            migrationBuilder.CreateTable(
                name: "ZUSEClients",
                columns: table => new
                {
                    reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    branchid = table.Column<string>(name: "branch_id", type: "nvarchar(450)", nullable: false),
                    topicid = table.Column<string>(name: "topic_id", type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    businesstype = table.Column<int>(name: "business_type", type: "int", nullable: false),
                    isposintegrated = table.Column<bool>(name: "is_pos_integrated", type: "bit", nullable: false),
                    integratedpos = table.Column<int>(name: "integrated_pos", type: "int", nullable: false),
                    baseposproviderapiurl = table.Column<string>(name: "base_pos_provider_api_url", type: "nvarchar(max)", nullable: false),
                    poscategoriesfetchurl = table.Column<string>(name: "pos_categories_fetch_url", type: "nvarchar(max)", nullable: false),
                    sectionsfetchurl = table.Column<string>(name: "sections_fetch_url", type: "nvarchar(max)", nullable: true),
                    tablesfetchurl = table.Column<string>(name: "tables_fetch_url", type: "nvarchar(max)", nullable: false),
                    istvprovider = table.Column<bool>(name: "is_tv_provider", type: "bit", nullable: false),
                    iskdsprovider = table.Column<bool>(name: "is_kds_provider", type: "bit", nullable: false),
                    ismobilenotifierprovider = table.Column<bool>(name: "is_mobile_notifier_provider", type: "bit", nullable: false),
                    isloyaltyenabled = table.Column<bool>(name: "is_loyalty_enabled", type: "bit", nullable: false),
                    isexternalnotificationuser = table.Column<bool>(name: "is_external_notification_user", type: "bit", nullable: false),
                    accesstoken = table.Column<string>(name: "access_token", type: "nvarchar(max)", nullable: true),
                    externalnotificationname = table.Column<string>(name: "external_notification_name", type: "nvarchar(max)", nullable: true),
                    externalnotificationlimit = table.Column<int>(name: "external_notification_limit", type: "int", nullable: true),
                    externalnotificationcount = table.Column<int>(name: "external_notification_count", type: "int", nullable: true),
                    externalnotificationspecialmsg = table.Column<string>(name: "external_notification_special_msg", type: "nvarchar(max)", nullable: true),
                    rewardsplan = table.Column<string>(name: "rewards_plan", type: "nvarchar(max)", nullable: true),
                    rewardspointsthreshold = table.Column<double>(name: "rewards_points_threshold", type: "float", nullable: true),
                    rewardsgreeting = table.Column<string>(name: "rewards_greeting", type: "nvarchar(max)", nullable: true),
                    tvtexttospeach = table.Column<string>(name: "tv_text_to_speach", type: "nvarchar(max)", nullable: true),
                    iskdsordercompletionapprovalneeded = table.Column<bool>(name: "is_kds_order_completion_approval_needed", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZUSEClients", x => new { x.reference, x.branchid });
                });

            migrationBuilder.CreateIndex(
                name: "IX_sessions_id_business_reference",
                table: "sessions",
                columns: new[] { "id", "business_reference" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "ZUSEClients");
        }
    }
}
