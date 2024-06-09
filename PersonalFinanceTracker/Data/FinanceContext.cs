using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data;

public class FinanceContext : DbContext
{
    public DbSet<FinanceUser> FinanceUsers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=FinanceTrackerData;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne<FinanceUser>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}