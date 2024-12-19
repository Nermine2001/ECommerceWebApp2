using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Check",
                table: "Products",
                newName: "CheckStatus");

            migrationBuilder.AddColumn<int>(
                name: "BillingDetailsId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BillingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingDetailsId",
                table: "Orders",
                column: "BillingDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_BillingDetails_BillingDetailsId",
                table: "Orders",
                column: "BillingDetailsId",
                principalTable: "BillingDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_BillingDetails_BillingDetailsId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "BillingDetails");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BillingDetailsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingDetailsId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CheckStatus",
                table: "Products",
                newName: "Check");
        }
    }
}
