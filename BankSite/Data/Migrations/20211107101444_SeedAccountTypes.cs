using Microsoft.EntityFrameworkCore.Migrations;

namespace BankSite.Data.Migrations
{
    public partial class SeedAccountTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "InterestRate",
                table: "AccountTypes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "InterestRate", "TypeName" },
                values: new object[,]
                {
                    { 1, 0.03f, "Checking" },
                    { 2, 0.06f, "Savings" },
                    { 3, 0.09f, "Money Market" },
                    { 4, 0.5f, "Certificate of Deposit" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "AccountTypes");
        }
    }
}
