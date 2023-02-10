using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZUSE.Server.Migrations
{
    /// <inheritdoc />
    public partial class _95migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "topic_id",
                table: "ZUSEClients",
                newName: "topic");

            migrationBuilder.RenameColumn(
                name: "reference",
                table: "ZUSEClients",
                newName: "reference_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "ZUSEClients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "topic",
                table: "ZUSEClients",
                newName: "topic_id");

            migrationBuilder.RenameColumn(
                name: "reference_id",
                table: "ZUSEClients",
                newName: "reference");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "ZUSEClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
