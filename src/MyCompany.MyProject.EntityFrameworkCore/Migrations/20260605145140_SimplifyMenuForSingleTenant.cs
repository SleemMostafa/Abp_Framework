using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCompany.MyProject.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyMenuForSingleTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppMenuItems_TenantId_CategoryId",
                table: "AppMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuItems_TenantId_Name",
                table: "AppMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuCategories_TenantId_Name",
                table: "AppMenuCategories");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuCategories_TenantId_ParentId_DisplayOrder",
                table: "AppMenuCategories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppMenuItems");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AppMenuCategories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppMenuCategories");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItems_CategoryId",
                table: "AppMenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItems_Name",
                table: "AppMenuItems",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuCategories_DisplayOrder",
                table: "AppMenuCategories",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuCategories_Name",
                table: "AppMenuCategories",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppMenuItems_CategoryId",
                table: "AppMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuItems_Name",
                table: "AppMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuCategories_DisplayOrder",
                table: "AppMenuCategories");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuCategories_Name",
                table: "AppMenuCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppMenuItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "AppMenuCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppMenuCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItems_TenantId_CategoryId",
                table: "AppMenuItems",
                columns: new[] { "TenantId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItems_TenantId_Name",
                table: "AppMenuItems",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuCategories_TenantId_Name",
                table: "AppMenuCategories",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuCategories_TenantId_ParentId_DisplayOrder",
                table: "AppMenuCategories",
                columns: new[] { "TenantId", "ParentId", "DisplayOrder" });
        }
    }
}
