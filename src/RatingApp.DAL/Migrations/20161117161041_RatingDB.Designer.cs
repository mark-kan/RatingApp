using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RatingApp.DAL;

namespace RatingApp.DAL.Migrations
{
    [DbContext(typeof(RatingDBContext))]
    [Migration("20161117161041_RatingDB")]
    partial class RatingDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RatingApp.Domain.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SkillName")
                        .IsRequired();

                    b.Property<int?>("UserSkillSkillId");

                    b.Property<string>("UserSkillUserId");

                    b.HasKey("SkillId");

                    b.HasIndex("UserSkillSkillId", "UserSkillUserId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("RatingApp.Domain.UserSkill", b =>
                {
                    b.Property<int>("SkillId");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("Added");

                    b.Property<int?>("Level");

                    b.Property<DateTime?>("Updated");

                    b.Property<int>("UserSkillId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("SkillId", "UserId");

                    b.HasIndex("SkillId");

                    b.ToTable("UserSkills");
                });

            modelBuilder.Entity("RatingApp.Domain.Skill", b =>
                {
                    b.HasOne("RatingApp.Domain.UserSkill")
                        .WithMany("Skills")
                        .HasForeignKey("UserSkillSkillId", "UserSkillUserId");
                });

            modelBuilder.Entity("RatingApp.Domain.UserSkill", b =>
                {
                    b.HasOne("RatingApp.Domain.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
