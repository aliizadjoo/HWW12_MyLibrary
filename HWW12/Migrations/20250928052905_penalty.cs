using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HWW12.Migrations
{
    /// <inheritdoc />
    public partial class penalty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PenaltyAmount",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyAmount",
                table: "Users");
        }
    }
}
