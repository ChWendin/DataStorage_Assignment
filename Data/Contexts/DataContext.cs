
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
        // Relation mellan ProjectEntity och StatusTypeEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Status)
            .WithOne() // Om StatusType kan vara associerad med flera ProjectEntities
            .HasForeignKey<ProjectEntity>(p => p.StatusId)
            .OnDelete(DeleteBehavior.SetNull); // Exempel på delete behavior

        // Relation mellan ProjectEntity och CustomerEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Customer)
            .WithOne() // Om Customer kan vara associerad med flera ProjectEntities
            .HasForeignKey<ProjectEntity>(p => p.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation mellan ProjectEntity och ProductEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Product)
            .WithOne() // Om Product kan vara associerad med flera ProjectEntities
            .HasForeignKey<ProjectEntity>(p => p.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation mellan ProjectEntity och UserEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.User)
            .WithOne() // Om User kan vara associerad med flera ProjectEntities
            .HasForeignKey<ProjectEntity>(p => p.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}