using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatureDashboard.Web.Migrations
{
    public partial class ApplicationEnvironmenttoApplicationEnvironment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationEnvironments_EnvironmentId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "EnvironmentId",
                table: "Applications",
                newName: "ApplicationEnvironmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_EnvironmentId",
                table: "Applications",
                newName: "IX_Applications_ApplicationEnvironmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationEnvironments_ApplicationEnvironmentId",
                table: "Applications",
                column: "ApplicationEnvironmentId",
                principalTable: "ApplicationEnvironments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationEnvironments_ApplicationEnvironmentId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "ApplicationEnvironmentId",
                table: "Applications",
                newName: "EnvironmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationEnvironmentId",
                table: "Applications",
                newName: "IX_Applications_EnvironmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationEnvironments_EnvironmentId",
                table: "Applications",
                column: "EnvironmentId",
                principalTable: "ApplicationEnvironments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
