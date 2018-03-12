using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatureDashboard.Web.Migrations
{
    public partial class ApplicationEnvironment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Environment",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentId",
                table: "Applications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationEnvironments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsProduction = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEnvironments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EnvironmentId",
                table: "Applications",
                column: "EnvironmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationEnvironments_EnvironmentId",
                table: "Applications",
                column: "EnvironmentId",
                principalTable: "ApplicationEnvironments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationEnvironments_EnvironmentId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "ApplicationEnvironments");

            migrationBuilder.DropIndex(
                name: "IX_Applications_EnvironmentId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EnvironmentId",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "Applications",
                nullable: true);
        }
    }
}
