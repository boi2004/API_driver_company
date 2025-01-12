using System;
using System.Collections.Generic;
using Driver_Company_5._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Driver_Company_5._0.Infrastructure.Data;

public partial class DriverManagementContext : DbContext
{
    public DriverManagementContext()
    {
    }

    public DriverManagementContext(DbContextOptions<DriverManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Livestream> Livestreams { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-BKK9SGBB\\SQLEXPRESS;Database=driver_management;User Id=nguyen;Password=*Nguyen2004;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83FFE1F563E");

            entity.ToTable("documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DocNumber)
                .HasMaxLength(100)
                .HasColumnName("doc_number");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .HasColumnName("doc_type");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Documents)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__documents__vehic__60A75C0F");
        });

        modelBuilder.Entity<Livestream>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__livestre__3213E83FEDED4E64");

            entity.ToTable("livestreams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AudioUrl)
                .HasMaxLength(355)
                .HasColumnName("audio_url");
            entity.Property(e => e.DownloadUrl)
                .HasMaxLength(355)
                .HasColumnName("download_url");
            entity.Property(e => e.DriverId).HasColumnName("driver_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(355)
                .HasColumnName("video_url");

            entity.HasOne(d => d.Driver).WithMany(p => p.Livestreams)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__livestrea__drive__656C112C");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Livestreams)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__livestrea__vehic__6477ECF3");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__location__3213E83F05F94471");

            entity.ToTable("locations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Locations)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__locations__vehic__6D0D32F4");
        });

        modelBuilder.Entity<MaintenanceRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__maintena__3213E83FA76EBFE1");

            entity.ToTable("maintenance_records");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.MaintenanceDate).HasColumnName("maintenance_date");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.MaintenanceRecords)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__maintenan__vehic__70DDC3D8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FEC88167A");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "UQ__roles__783254B1CB0A984E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(150)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9025C58E");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164B74041A2").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC5721A30074D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedReason)
                .HasMaxLength(255)
                .HasColumnName("deleted_reason");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__users__role_id__5165187F");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vehicles__3213E83FE4CF99B6");

            entity.ToTable("vehicles");

            entity.HasIndex(e => e.LicensePlate, "UQ__vehicles__F72CD56EE35C25C7").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentDriverId).HasColumnName("current_driver_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastMaintenanceDate).HasColumnName("last_maintenance_date");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(50)
                .HasColumnName("license_plate");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VehicleName)
                .HasMaxLength(200)
                .HasColumnName("vehicle_name");

            entity.HasOne(d => d.CurrentDriver).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CurrentDriverId)
                .HasConstraintName("FK__vehicles__curren__59FA5E80");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__videos__3213E83F5CABBECF");

            entity.ToTable("videos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(355)
                .HasColumnName("description");
            entity.Property(e => e.FileSize)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("file_size");
            entity.Property(e => e.LivestreamId).HasColumnName("livestream_id");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(355)
                .HasColumnName("video_url");

            entity.HasOne(d => d.Livestream).WithMany(p => p.Videos)
                .HasForeignKey(d => d.LivestreamId)
                .HasConstraintName("FK__videos__livestre__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
