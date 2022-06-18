using Core.Domain.Common;
using Core.Domain.Properties;
using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public class RedDbContext: DbContext
{
    public RedDbContext(DbContextOptions<RedDbContext> options):base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<PropertyType> PropertyTypes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().HasOne(u => u.UserAddress)
            .WithOne(a => a.User).HasForeignKey<User>(u => u.AddressId);

        builder.Entity<Property>().HasKey(p => p.Id);
        builder.Entity<Property>().HasOne(p => p.User)
            .WithMany(u => u.Properties).HasForeignKey(p => p.SellerId);
        builder.Entity<Property>().HasOne<PropertyType>().WithMany(pt => pt.Properties)
            .HasForeignKey(p => p.PropertyTypeId);
        builder.Entity<Property>().HasMany(p => p.PropertyImages)
            .WithOne(pi => pi.Property);
        builder.Entity<Property>().HasOne(p => p.Address)
            .WithOne(a => a.Property).HasForeignKey<Property>(p => p.AddressId);

        builder.Entity<PropertyImage>().HasKey(pi => pi.Id);
        builder.Entity<PropertyImage>().HasOne(pi => pi.Property)
            .WithMany(p => p.PropertyImages).HasForeignKey(pi => pi.PropertyId);

        builder.Entity<Address>().HasKey(a => a.Id);
        builder.Entity<Address>().HasOne(p => p.Property)
            .WithOne().HasForeignKey<Address>(a => a.PropertyId);
        builder.Entity<Address>().HasOne(a => a.User)
            .WithOne(u => u.UserAddress).HasForeignKey<Address>(a => a.OfficeId);

        builder.Entity<Appointment>().HasKey(a => a.Id);

        builder.Entity<Comment>().HasKey(c => c.Id);
        builder.Entity<Comment>().HasOne(c => c.Admin).WithOne()
            .HasForeignKey<Comment>(c => c.AdminId);
        builder.Entity<Comment>().HasOne(c => c.Buyer).WithOne()
            .HasForeignKey<Comment>(c => c.BuyerId);
        builder.Entity<Comment>().HasOne(c => c.Seller).WithOne()
            .HasForeignKey<Comment>(c => c.SellerId);
        builder.Entity<Comment>().HasOne(c => c.Property).WithOne()
            .HasForeignKey<Comment>(c => c.PropertyId);

        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().HasOne<User>(n => n.Admin).WithOne()
            .HasForeignKey<Notification>(n => n.AdminId);
    }
}
