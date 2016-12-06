using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RatingApp.DAL.Migrations
{
    public partial class droppedUserSkillId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_UserSkills_UserSkillSkillId_UserSkillUserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_UserSkillSkillId_UserSkillUserId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserSkillId",
                table: "UserSkills");

            migrationBuilder.DropColumn(
                name: "UserSkillSkillId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserSkillUserId",
                table: "Skills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserSkillId",
                table: "UserSkills",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserSkillSkillId",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSkillUserId",
                table: "Skills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserSkillSkillId_UserSkillUserId",
                table: "Skills",
                columns: new[] { "UserSkillSkillId", "UserSkillUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_UserSkills_UserSkillSkillId_UserSkillUserId",
                table: "Skills",
                columns: new[] { "UserSkillSkillId", "UserSkillUserId" },
                principalTable: "UserSkills",
                principalColumns: new[] { "SkillId", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
