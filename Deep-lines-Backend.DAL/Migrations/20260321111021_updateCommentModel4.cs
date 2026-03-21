using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deep_lines_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateCommentModel4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "published_data",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "published_data",
                table: "Comments");
        }
    }
}
