using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryReactCAH.Server.Models;

public partial class ItinventoryModelContext : DbContext
{
    public ItinventoryModelContext()
    {
    }

    public ItinventoryModelContext(DbContextOptions<ItinventoryModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<CellPhone> CellPhones { get; set; }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Docking> Dockings { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Ricoh> Ricohs { get; set; }

    public virtual DbSet<StaticIp> StaticIps { get; set; }

    public virtual DbSet<TelCode> TelCodes { get; set; }

    public virtual DbSet<Telephone> Telephones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Badges");

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<CellPhone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.CellPhones");

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IcloudPass)
                .HasMaxLength(20)
                .HasColumnName("ICloudPass");
            entity.Property(e => e.IcloudUser)
                .HasMaxLength(50)
                .HasColumnName("ICloudUser");
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.User).HasMaxLength(50);
        });

        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.CPUs");

            entity.ToTable("CPUs");

            entity.Property(e => e.AssetN).HasMaxLength(20);
            entity.Property(e => e.AssetNcr)
                .HasMaxLength(20)
                .HasColumnName("AssetNCR");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.ComputerName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(20);
            entity.Property(e => e.Domain).HasMaxLength(50);
            entity.Property(e => e.HardDisk).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Memory).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.OperatingSystem).HasMaxLength(20);
            entity.Property(e => e.OpticalDrive).HasMaxLength(20);
            entity.Property(e => e.PadLock).HasMaxLength(20);
            entity.Property(e => e.Processor).HasMaxLength(50);
            entity.Property(e => e.ProcessorSpeed).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SerialNumber).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.User).HasMaxLength(50);
            entity.Property(e => e.WireMac).HasMaxLength(20);
            entity.Property(e => e.WirelessMac).HasMaxLength(20);
        });

        modelBuilder.Entity<Docking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Dockings");

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(20);
            entity.Property(e => e.SerialNumber).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.User).HasMaxLength(50);
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Monitors");

            entity.Property(e => e.ActivoCr)
                .HasMaxLength(20)
                .HasColumnName("ActivoCR");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.SerialNumber).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.User).HasMaxLength(50);
        });

        modelBuilder.Entity<Ricoh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Ricohs");

            entity.Property(e => e.ActivoCr)
                .HasMaxLength(20)
                .HasColumnName("ActivoCR");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Link).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.NetName).HasMaxLength(50);
            entity.Property(e => e.SerialNumber).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<StaticIp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.StaticIPs");

            entity.ToTable("StaticIPs");

            entity.Property(e => e.Area).HasMaxLength(50);
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Device).HasMaxLength(50);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(20)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Line).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.NetworkPoint).HasMaxLength(50);
            entity.Property(e => e.Switch).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TelCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.TelCodes");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Telephone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Telephones");

            entity.Property(e => e.Activo).HasMaxLength(20);
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Employee).HasMaxLength(50);
            entity.Property(e => e.Serie).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Users");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
