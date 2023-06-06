using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagementDB.Models;

public partial class DeviceManagementContext : DbContext
{
    public DeviceManagementContext()
    {
    }

    public DeviceManagementContext(DbContextOptions<DeviceManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<OperatingSystem> OperatingSystems { get; set; }

    public virtual DbSet<OperatingSystemVersion> OperatingSystemVersions { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    public virtual DbSet<Ramamount> Ramamounts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DeviceManagement;Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC07D4FE29BA");

            entity.Property(e => e.IdCountry).HasColumnName("Id_Country");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__Id_Count__03BB8E22");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07F51AC08E");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devices__3214EC076B637DF0");

            entity.Property(e => e.IdCurrentUser).HasColumnName("Id_CurrentUser");
            entity.Property(e => e.IdDeviceType).HasColumnName("Id_DeviceType");
            entity.Property(e => e.IdManufacturer).HasColumnName("Id_Manufacturer");
            entity.Property(e => e.IdOsversion).HasColumnName("Id_OSVersion");
            entity.Property(e => e.IdProcessor).HasColumnName("Id_Processor");
            entity.Property(e => e.IdRamamount).HasColumnName("Id_RAMAmount");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCurrentUserNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdCurrentUser)
                .HasConstraintName("FK__Devices__Id_Curr__2057CCD0");

            entity.HasOne(d => d.IdDeviceTypeNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdDeviceType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__Id_Devi__1C873BEC");

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__Id_Manu__1B9317B3");

            entity.HasOne(d => d.IdOsversionNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdOsversion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__Id_OSVe__1D7B6025");

            entity.HasOne(d => d.IdProcessorNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdProcessor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__Id_Proc__1E6F845E");

            entity.HasOne(d => d.IdRamamountNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdRamamount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__Id_RAMA__1F63A897");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceTy__3214EC075E720CB9");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC0718236C57");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdCity).HasColumnName("Id_City");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Locations__Id_Ci__0697FACD");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC070AFED174");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OperatingSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatin__3214EC0717D5C45E");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OperatingSystemVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatin__3214EC0700CB8637");

            entity.Property(e => e.IdOs).HasColumnName("Id_OS");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOsNavigation).WithMany(p => p.OperatingSystemVersions)
                .HasForeignKey(d => d.IdOs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Operating__Id_OS__7755B73D");
        });

        modelBuilder.Entity<Processor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Processo__3214EC070E2B276A");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ramamount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RAMAmoun__3214EC07481C2159");

            entity.ToTable("RAMAmounts");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07E4979575");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC072299BBBB");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344B6D876C").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdLocation).HasColumnName("Id_Location");
            entity.Property(e => e.IdRole).HasColumnName("Id_Role");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLocationNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdLocation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Id_Locati__0D44F85C");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Id_Role__0C50D423");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
