
using mofa_test.Models;
using Microsoft.EntityFrameworkCore;
namespace mofa_test.DBContextEFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "StudentDB");
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }

        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<StudentNationality> StudentNationality { get; set; }
        public DbSet<FamilyMemberNationality> FamilyMembersNationalities { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

    }
}