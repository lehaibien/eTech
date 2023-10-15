﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTech.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNameFromAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
