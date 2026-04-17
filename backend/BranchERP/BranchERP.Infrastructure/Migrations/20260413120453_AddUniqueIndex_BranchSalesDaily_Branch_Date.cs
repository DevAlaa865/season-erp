using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BranchERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndex_BranchSalesDaily_Branch_Date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BranchSalesDailies_BranchId",
                table: "BranchSalesDailies");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSalesDailies_BranchId_SalesDate",
                table: "BranchSalesDailies",
                columns: new[] { "BranchId", "SalesDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BranchSalesDailies_BranchId_SalesDate",
                table: "BranchSalesDailies");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSalesDailies_BranchId",
                table: "BranchSalesDailies",
                column: "BranchId");
        }
    }
}
