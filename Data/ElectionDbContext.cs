using Microsoft.EntityFrameworkCore;
using Core.Entities;
using ElectionSystem.Models; // إذا كان الكلاس موجودًا في فولدر Models

namespace Infrastructure.Data
{
    public class ElectionDbContext : DbContext
    {
        public ElectionDbContext(DbContextOptions<ElectionDbContext> options) : base(options) { }

        // تعريف الجداول (DbSet)
        public DbSet<User> Users { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Province> Provinces { get; set; }
      //  public DbSet<RegisterRequest> registerRequests { get; set; }
        //public DbSet<LoginRequest> loginRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
           new User { Id = 1, Email = "admin@elections.com", Password = "adminpassword", Role = "Admin" },
           new User { Id = 2, Email = "voter@elections.com", Password = "voterpassword", Role = "Voter" }
       );

            // User Configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("Voter");

            // Relationship: User <-> Voter (One-to-One)
            modelBuilder.Entity<Voter>()
                .HasOne(v => v.User)
                .WithOne(u => u.Voter)
                .HasForeignKey<Voter>(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Voter <-> Province (Many-to-One)
            modelBuilder.Entity<Voter>()
                .HasOne(v => v.Province)
                .WithMany(p => p.Voters)
                .HasForeignKey(v => v.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Election <-> Supervisor (User) (Many-to-One)
            modelBuilder.Entity<Election>()
                .HasOne(e => e.Supervisor)
                .WithMany(u => u.SupervisedElections)
                .HasForeignKey(e => e.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Election <-> CreatedByUser (User) (Many-to-One)
            modelBuilder.Entity<Election>()
                .HasOne(e => e.CreatedByUser)
                .WithMany(u => u.CreatedElections)
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Election <-> Province (Optional Many-to-Many)
            modelBuilder.Entity<Election>()
                .HasMany(e => e.Provinces)
                .WithOne(p => p.Election)
                .HasForeignKey(p => p.ElectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Election <-> Candidate (One-to-Many)
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Election)
                .WithMany(e => e.Candidates)
                .HasForeignKey(c => c.ElectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Candidate <-> AddedByUser (User) (Many-to-One)
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.AddedByUser)
                .WithMany(u => u.AddedCandidates)
                .HasForeignKey(c => c.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
