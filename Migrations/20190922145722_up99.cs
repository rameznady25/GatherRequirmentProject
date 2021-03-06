﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gather_Requirement_Project.Migrations
{
    public partial class up99 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Screens");

            migrationBuilder.DropColumn(
                name: "DevName",
                table: "Screens");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CustomerPrograms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "CustomerPrograms");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Screens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DevName",
                table: "Screens",
                nullable: false,
                defaultValue: "");
        }
    }
}
