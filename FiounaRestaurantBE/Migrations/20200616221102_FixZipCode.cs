using Microsoft.EntityFrameworkCore.Migrations;

namespace FiounaRestaurantBE.Migrations
{
    public partial class FixZipCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zipCode",
                table: "Addresses",
                newName: "ZipCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Addresses",
                newName: "zipCode");
        }
    }
}
