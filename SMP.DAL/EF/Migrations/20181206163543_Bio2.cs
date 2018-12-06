using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMP.DAL.EF.Migrations
{
    public partial class Bio2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EduExp",
                table: "AspNetUsers",
                maxLength: 5120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobExp",
                table: "AspNetUsers",
                maxLength: 5120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EduExp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobExp",
                table: "AspNetUsers");
        }
    }
}
