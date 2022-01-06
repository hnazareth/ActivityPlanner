using ActivityPlanner.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivityPlanner.DAL
{
    public partial class DbContextActivityPlanner : DbContext
    {      

        public DbSet<Property> Property { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Survey> Survey { get; set; }

        public DbContextActivityPlanner() { }

        public DbContextActivityPlanner(DbContextOptions<DbContextActivityPlanner> options) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Helpers.HelperConfiguration.GetAppConfiguration().ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Survey)
                .WithOne(b => b.Activity)
                .HasForeignKey<Survey>(e => e.activity_id);
        }
    }
}
