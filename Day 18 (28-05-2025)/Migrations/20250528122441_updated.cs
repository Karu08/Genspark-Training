using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinment_Patient",
                table: "Appointmnets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DoctorSpecialities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Appointmnets");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient",
                table: "Appointmnets",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient",
                table: "Appointmnets");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DoctorSpecialities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Appointmnets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinment_Patient",
                table: "Appointmnets",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
