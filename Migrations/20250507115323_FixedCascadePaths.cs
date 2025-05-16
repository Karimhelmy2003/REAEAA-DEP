using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAEAA_DEPI_API.Migrations
{
    /// <inheritdoc />
    public partial class FixedCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins_REAEAA_DEPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins_REAEAA_DEPI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments_REAEAA_DEPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments_REAEAA_DEPI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Patients_REAEAA_DEPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients_REAEAA_DEPI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patients_REAEAA_DEPI_Admins_REAEAA_DEPI_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins_REAEAA_DEPI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors_REAEAA_DEPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeptID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors_REAEAA_DEPI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Doctors_REAEAA_DEPI_Admins_REAEAA_DEPI_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins_REAEAA_DEPI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_REAEAA_DEPI_Departments_REAEAA_DEPI_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Departments_REAEAA_DEPI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories_REAEAA_DEPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories_REAEAA_DEPI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_REAEAA_DEPI_Patients_REAEAA_DEPI_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients_REAEAA_DEPI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
     name: "MedicalHistories_REAEAA_DEPI",
     columns: table => new
     {
         ID = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         Date = table.Column<DateTime>(type: "datetime2", nullable: false),
         DoctorID = table.Column<int>(type: "int", nullable: false), // ✅ NEW
         DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
         Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
         Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
         PatientID = table.Column<int>(type: "int", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_MedicalHistories_REAEAA_DEPI", x => x.ID);
         table.ForeignKey(
             name: "FK_MedicalHistories_REAEAA_DEPI_Patients_REAEAA_DEPI_PatientID",
             column: x => x.PatientID,
             principalTable: "Patients_REAEAA_DEPI",
             principalColumn: "ID",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey( // ✅ NEW FK
             name: "FK_MedicalHistories_REAEAA_DEPI_Doctors_REAEAA_DEPI_DoctorID",
             column: x => x.DoctorID,
             principalTable: "Doctors_REAEAA_DEPI",
             principalColumn: "ID",
             onDelete: ReferentialAction.Restrict); // ✅ Avoid cascade path error
     });

            migrationBuilder.CreateIndex(
    name: "IX_MedicalHistories_REAEAA_DEPI_DoctorID",
    table: "MedicalHistories_REAEAA_DEPI",
    column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_REAEAA_DEPI_AdminID",
                table: "Appointments_REAEAA_DEPI",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_REAEAA_DEPI_DoctorID",
                table: "Appointments_REAEAA_DEPI",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_REAEAA_DEPI_PatientID",
                table: "Appointments_REAEAA_DEPI",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_REAEAA_DEPI_AdminID",
                table: "Doctors_REAEAA_DEPI",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_REAEAA_DEPI_DeptID",
                table: "Doctors_REAEAA_DEPI",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_REAEAA_DEPI_PatientID",
                table: "MedicalHistories_REAEAA_DEPI",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_REAEAA_DEPI_AdminID",
                table: "Patients_REAEAA_DEPI",
                column: "AdminID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments_REAEAA_DEPI");

            migrationBuilder.DropTable(
                name: "MedicalHistories_REAEAA_DEPI");

            migrationBuilder.DropTable(
                name: "Doctors_REAEAA_DEPI");

            migrationBuilder.DropTable(
                name: "Patients_REAEAA_DEPI");

            migrationBuilder.DropTable(
                name: "Departments_REAEAA_DEPI");

            migrationBuilder.DropTable(
                name: "Admins_REAEAA_DEPI");
        }
    }
}
