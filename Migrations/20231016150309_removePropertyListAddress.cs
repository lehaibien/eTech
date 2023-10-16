using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTech.Migrations
{
    /// <inheritdoc />
    public partial class removePropertyListAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "AspNetUsers",
                newName: "AddressesId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AddressesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressesId",
                table: "AspNetUsers",
                column: "AddressesId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressesId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AddressesId",
                table: "AspNetUsers",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AddressesId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
