
using Data.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Status)
            .WithOne() 
            .HasForeignKey<ProjectEntity>(p => p.StatusId)
            .OnDelete(DeleteBehavior.SetNull); 

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Customer)
            .WithOne() 
            .HasForeignKey<ProjectEntity>(p => p.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Product)
            .WithOne() 
            .HasForeignKey<ProjectEntity>(p => p.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.User)
            .WithOne() 
            .HasForeignKey<ProjectEntity>(p => p.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}