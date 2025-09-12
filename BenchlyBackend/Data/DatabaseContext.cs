using BenchlyBackend.Models.Locations;
using BenchlyBackend.Models.Reviews;
using BenchlyBackend.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace BenchlyBackend.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Location> Locations { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        OnModelCreatingLocation(modelBuilder);
        OnModelCreatingReview(modelBuilder);
        OnModelCreatingTag(modelBuilder);
    }

    private static void OnModelCreatingLocation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Locations");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Latitude)
                .IsRequired()
                .HasColumnType("decimal(9, 6)"); // Total of 9 digits, 6 after the decimal point

            entity.Property(e => e.Longitude)
                .IsRequired()
                .HasColumnType("decimal(9, 6)"); // Total of 9 digits, 6 after the decimal point

            entity.Property(e => e.DistanceFromCurrentLocationMiles)
                .IsRequired()
                .HasColumnType("decimal(7, 2)"); // Total of 7 digits, 2 after the decimal point

            entity.Property(e => e.DateAdded)
                .IsRequired()
                .HasColumnType("datetime");

            entity.HasMany(e => e.Reviews)
                .WithOne(e => e.Location)
                .HasForeignKey(r => r.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Tags)
                .WithOne(e => e.Location)
                .HasForeignKey(t => t.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void OnModelCreatingReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Comments)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(e => e.Rating);

            entity.Property(e => e.DatePosted)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.Removed)
                .HasDefaultValue(false);
        });
    }

    private static void OnModelCreatingTag(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tags");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(e => e.Removed)
                .HasDefaultValue(false);

            entity.HasOne(e => e.Location)
                .WithMany(l => l.Tags)
                .HasForeignKey(t => t.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Removed)
                .HasDefaultValue(false);
        });
    }
}