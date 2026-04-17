using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BranchERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeToShortageDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "BranchSalesShortageDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchSalesShortageDetails_EmployeeId",
                table: "BranchSalesShortageDetails",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchSalesShortageDetails_Employees_EmployeeId",
                table: "BranchSalesShortageDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchSalesShortageDetails_Employees_EmployeeId",
                table: "BranchSalesShortageDetails");

            migrationBuilder.DropIndex(
                name: "IX_BranchSalesShortageDetails_EmployeeId",
                table: "BranchSalesShortageDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "BranchSalesShortageDetails");
        }
    }
}
