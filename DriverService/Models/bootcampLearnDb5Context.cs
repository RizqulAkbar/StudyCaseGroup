using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DriverService.Models
{
    public partial class bootcampLearnDb5Context : DbContext
    {
        public bootcampLearnDb5Context()
        {
        }

        public bootcampLearnDb5Context(DbContextOptions<bootcampLearnDb5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Pengguna> Penggunas { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Saldo> Saldos { get; set; }
        public virtual DbSet<SaldoDriver> SaldoDrivers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDriver> UserDrivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:bootcampdb.database.windows.net;Initial Catalog=bootcampLearnDb5;User ID=bootcampdb;Password=bootcampLearnDb1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.PenggunaId).HasColumnName("PenggunaID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pengguna>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsLocked).HasColumnName("isLocked");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.PricePerKm)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Saldo>(entity =>
            {
                entity.ToTable("Saldo");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.HasOne(d => d.Pengguna)
                    .WithMany(p => p.Saldos)
                    .HasForeignKey(d => d.PenggunaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Saldo_Penggunas");
            });

            modelBuilder.Entity<SaldoDriver>(entity =>
            {
                entity.HasKey(e => e.SaldoId);

                entity.ToTable("SaldoDriver");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.SaldoDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaldoDriver_UserDriver");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsLocked).HasColumnName("isLocked");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDriver>(entity =>
            {
                entity.HasKey(e => e.DriverId);

                entity.ToTable("UserDriver");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
