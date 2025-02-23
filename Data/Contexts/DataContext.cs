
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
            .WithMany() // Om StatusType kan vara associerad med flera ProjectEntities
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Cascade); // Exempel på delete behavior

        // Relation mellan ProjectEntity och CustomerEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Customer)
            .WithMany() // Om Customer kan vara associerad med flera ProjectEntities
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation mellan ProjectEntity och ProductEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Product)
            .WithMany() // Om Product kan vara associerad med flera ProjectEntities
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation mellan ProjectEntity och UserEntity
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.User)
            .WithMany() // Om User kan vara associerad med flera ProjectEntities
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}