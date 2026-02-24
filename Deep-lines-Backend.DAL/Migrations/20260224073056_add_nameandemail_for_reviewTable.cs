using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deep_lines_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_nameandemail_for_reviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reviewer_email",
                table: "Reviwe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reviewer_name",
                table: "Reviwe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reviewer_email",
                table: "Reviwe");

            migrationBuilder.DropColumn(
                name: "reviewer_name",
                table: "Reviwe");
        }
    }
}
