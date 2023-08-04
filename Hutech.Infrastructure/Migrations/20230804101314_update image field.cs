using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hutech.Infrastructure.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981
    public partial class updateimagefield : Migration
#pragma warning restore CS8981
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Product");
        }
    }
}
