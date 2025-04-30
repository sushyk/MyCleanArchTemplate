using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Adapter.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=MyCleanArchTemplateDB;Username=postgres;Password=mysecretpassword");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(builder =>
        {
            builder.ToTable("Customer");
            builder.HasKey(e => e.CustomerId);
            builder.Property(e => e.CustomerId).UseIdentityAlwaysColumn();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValueSql("NOW()");
        });
    }
}
