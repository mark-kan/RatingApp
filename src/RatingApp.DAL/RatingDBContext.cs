using Microsoft.EntityFrameworkCore;
using RatingApp.Domain;

namespace RatingApp.DAL
{
    public class RatingDBContext : DbContext
    {
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RatingApp;Trusted_Connection=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<UserSkill>()
                .HasKey(k => new { k.SkillId, k.UserId});
            
            
        }
    }   
}