using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Her under kunne man have brugt reflection.
        public DbSet<AgeGroup> AgeGroups { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Score> Scores { get; set; } = null!;
        public DbSet<DailyChallenge> DailyChallenges { get; set; } = null!;
    }
}
