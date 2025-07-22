using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muyik.SmartSchool.Migrations
{
    /// <inheritdoc />
    public partial class AddExtendedUserPropertiesAndNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AbpUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                comment: "Address of the user");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true,
                comment: "Date of birth of the user");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AbpUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AbpUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "First name of the user");

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasLeftSchool",
                table: "AbpUsers",
                type: "bit",
                nullable: true,
                defaultValue: false,
                comment: "Indicates if the user has left school");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AbpUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Middle name of the user");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolClassId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPhoto",
                table: "AbpUsers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Photo path or URL of the user");

            migrationBuilder.CreateTable(
                name: "Beno_Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the gender"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Description of the gender"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beno_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beno_SchoolClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Name of the school class"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Description of the school class"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beno_SchoolClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_GenderId",
                table: "AbpUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_SchoolClassId",
                table: "AbpUsers",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Beno_Genders_GenderName",
                table: "Beno_Genders",
                column: "GenderName");

            migrationBuilder.CreateIndex(
                name: "IX_Beno_SchoolClasses_ClassName",
                table: "Beno_SchoolClasses",
                column: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Beno_Genders_GenderId",
                table: "AbpUsers",
                column: "GenderId",
                principalTable: "Beno_Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Beno_SchoolClasses_SchoolClassId",
                table: "AbpUsers",
                column: "SchoolClassId",
                principalTable: "Beno_SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Beno_Genders_GenderId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Beno_SchoolClasses_SchoolClassId",
                table: "AbpUsers");

            migrationBuilder.DropTable(
                name: "Beno_Genders");

            migrationBuilder.DropTable(
                name: "Beno_SchoolClasses");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_GenderId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_SchoolClassId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "HasLeftSchool",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "UserPhoto",
                table: "AbpUsers");
        }
    }
}
