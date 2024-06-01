using UniversityCommunity.Data.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniversityCommunity.Data.EntityFramework
{
    public class UniversityCommunityContext : DbContext
    {
        public UniversityCommunityContext(DbContextOptions<UniversityCommunityContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-748VRTV;database=UniversityCommunity; integrated security=true;TrustServerCertificate=True;");
        }

        public DbSet<Community> Communities { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserEmailOtp> UserEmailOtps { get; set; }
        public DbSet<OutgoingMail> OutgoingMails { get; set; }
        public DbSet<CommunityMember> CommunityMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>().HasKey(p => p.Id);
            modelBuilder.Entity<Community>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Announcement>().HasKey(p => p.Id);
            modelBuilder.Entity<Announcement>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<UserType>().HasKey(p => p.Id);

            modelBuilder.Entity<UserEmailOtp>().HasKey(p => p.Id);
            modelBuilder.Entity<UserEmailOtp>().Property(p => p.Status).HasDefaultValue(0);
            modelBuilder.Entity<UserEmailOtp>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<OutgoingMail>().HasKey(p => p.Id);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.UserId).HasDefaultValue(0);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.Status).HasDefaultValue(0);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<CommunityMember>().HasKey(p => p.Id);
            modelBuilder.Entity<CommunityMember>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

        }
    }
}