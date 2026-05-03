using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personel.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToPersonelEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_Personels_ManagerId1",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Personels_departments_DepartmentId",
                table: "Personels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Departments");

            migrationBuilder.RenameIndex(
                name: "IX_departments_ManagerId1",
                table: "Departments",
                newName: "IX_Departments_ManagerId1");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "HireDate",
                table: "Personels",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_Email",
                table: "Personels",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Personels_ManagerId1",
                table: "Departments",
                column: "ManagerId1",
                principalTable: "Personels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_Departments_DepartmentId",
                table: "Personels",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Personels_ManagerId1",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Personels_Departments_DepartmentId",
                table: "Personels");

            migrationBuilder.DropIndex(
                name: "IX_Personels_Email",
                table: "Personels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "departments");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_ManagerId1",
                table: "departments",
                newName: "IX_departments_ManagerId1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Personels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_Personels_ManagerId1",
                table: "departments",
                column: "ManagerId1",
                principalTable: "Personels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_departments_DepartmentId",
                table: "Personels",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
