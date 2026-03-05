using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deep_lines_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeFieldFromBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
