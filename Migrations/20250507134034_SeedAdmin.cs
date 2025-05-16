using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAEAA_DEPI_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins_REAEAA_DEPI",
                columns: new[] { "ID", "Password", "Username" },
                values: new object[] { 1, "admin123", "admin1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins_REAEAA_DEPI",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
