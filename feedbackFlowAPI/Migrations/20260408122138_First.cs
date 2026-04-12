using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace feedbackFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRole_user_roles_UserRoleId",
                table: "UserUserRole");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "UserUserRole",
                newName: "UserRolesId");

            migrationBuilder.RenameColumn(
                name: "Education",
                table: "classes",
                newName: "education");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "student_results",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "education",
                table: "classes",
                type: "education",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRole_user_roles_UserRolesId",
                table: "UserUserRole",
                column: "UserRolesId",
                principalTable: "user_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRole_user_roles_UserRolesId",
                table: "UserUserRole");

            migrationBuilder.RenameColumn(
                name: "UserRolesId",
                table: "UserUserRole",
                newName: "UserRoleId");

            migrationBuilder.RenameColumn(
                name: "education",
                table: "classes",
                newName: "Education");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "student_results",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "Education",
                table: "classes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "education");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRole_user_roles_UserRoleId",
                table: "UserUserRole",
                column: "UserRoleId",
                principalTable: "user_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
