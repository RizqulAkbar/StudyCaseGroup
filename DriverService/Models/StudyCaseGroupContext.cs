using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DriverService.Models
{
    public partial class StudyCaseGroupContext : DbContext
    {
        public StudyCaseGroupContext()
        {
        }

        public StudyCaseGroupContext(DbContextOptions<StudyCaseGroupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PriceAdmin> PriceAdmins { get; set; }
        public virtual DbSet<SaldoDriver> SaldoDrivers { get; set; }
        public virtual DbSet<UserDriver> UserDrivers { get; set; }
        public virtual DbSet<UserPengguna> UserPenggunas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=StudyCaseGroup;User ID=user;Password=password12345;");
//            }
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

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_UserDriver");

                entity.HasOne(d => d.Pengguna)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PenggunaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_UserPengguna");
            });

            modelBuilder.Entity<PriceAdmin>(entity =>
            {
                entity.HasKey(e => e.PriceId)
                    .HasName("PK_PriceAdmin1");

                entity.ToTable("PriceAdmin");

                entity.Property(e => e.PriceId).HasColumnName("PriceID");

                entity.Property(e => e.Created).HasColumnType("datetime");
            });

            modelBuilder.Entity<SaldoDriver>(entity =>
            {
                entity.HasKey(e => e.SaldoId);

                entity.ToTable("SaldoDriver");

                entity.Property(e => e.SaldoId).HasColumnName("SaldoID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.SaldoDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaldoDriver_UserDriver");
            });

            modelBuilder.Entity<UserDriver>(entity =>
            {
                entity.HasKey(e => e.DriverId);

                entity.ToTable("UserDriver");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

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

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPengguna>(entity =>
            {
                entity.HasKey(e => e.PenggunaId);

                entity.ToTable("UserPengguna");

                entity.Property(e => e.PenggunaId).HasColumnName("PenggunaID");

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
