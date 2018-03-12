using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatureDashboard.Web.Migrations
{
    public partial class ApplicationApplicationEnvironmentIdbackingproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationEnvironments_ApplicationEnvironmentId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationEnvironmentId",
                table: "Applications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationEnvironments_ApplicationEnvironmentId",
                table: "Applications",
                column: "ApplicationEnvironmentId",
                principalTable: "ApplicationEnvironments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationEnvironments_ApplicationEnvironmentId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationEnvironmentId",
                table: "Applications",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationEnvironments_ApplicationEnvironmentId",
                table: "Applications",
                column: "ApplicationEnvironmentId",
                principalTable: "ApplicationEnvironments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
