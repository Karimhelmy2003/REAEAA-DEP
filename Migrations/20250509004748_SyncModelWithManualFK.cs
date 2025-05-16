using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAEAA_DEPI_API.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelWithManualFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_REAEAA_DEPI_Doctors_REAEAA_DEPI_DoctorID",
                table: "MedicalHistories_REAEAA_DEPI");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_REAEAA_DEPI_Doctors_REAEAA_DEPI_DoctorID",
                table: "MedicalHistories_REAEAA_DEPI",
                column: "DoctorID",
                principalTable: "Doctors_REAEAA_DEPI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_REAEAA_DEPI_Doctors_REAEAA_DEPI_DoctorID",
                table: "MedicalHistories_REAEAA_DEPI");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_REAEAA_DEPI_Doctors_REAEAA_DEPI_DoctorID",
                table: "MedicalHistories_REAEAA_DEPI",
                column: "DoctorID",
                principalTable: "Doctors_REAEAA_DEPI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
