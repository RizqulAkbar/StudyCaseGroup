﻿// <auto-generated />
using System;
using DriverService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DriverService.Migrations
{
    [DbContext(typeof(bootcampLearnDb5Context))]
    [Migration("20220121075222_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DriverService.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<int>("DriverId")
                        .HasColumnType("int")
                        .HasColumnName("DriverID");

                    b.Property<double>("LatDriver")
                        .HasColumnType("float");

                    b.Property<double>("LatPengguna")
                        .HasColumnType("float");

                    b.Property<double>("LatTujuan")
                        .HasColumnType("float");

                    b.Property<double>("LongDriver")
                        .HasColumnType("float");

                    b.Property<double>("LongPengguna")
                        .HasColumnType("float");

                    b.Property<double>("LongTujuan")
                        .HasColumnType("float");

                    b.Property<int>("PenggunaId")
                        .HasColumnType("int")
                        .HasColumnName("PenggunaID");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DriverService.Models.Pengguna", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit")
                        .HasColumnName("isLocked");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Penggunas");
                });

            modelBuilder.Entity("DriverService.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PricePerKm")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("DriverService.Models.Saldo", b =>
                {
                    b.Property<int>("SaldoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<double>("MutasiSaldo")
                        .HasColumnType("float");

                    b.Property<int>("PenggunaId")
                        .HasColumnType("int");

                    b.Property<double>("TotalSaldo")
                        .HasColumnType("float");

                    b.HasKey("SaldoId");

                    b.HasIndex("PenggunaId");

                    b.ToTable("Saldo");
                });

            modelBuilder.Entity("DriverService.Models.SaldoDriver", b =>
                {
                    b.Property<int>("SaldoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<double>("MutasiSaldo")
                        .HasColumnType("float");

                    b.Property<double>("TotalSaldo")
                        .HasColumnType("float");

                    b.HasKey("SaldoId");

                    b.HasIndex("DriverId");

                    b.ToTable("SaldoDriver");
                });

            modelBuilder.Entity("DriverService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit")
                        .HasColumnName("isLocked");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DriverService.Models.UserDriver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("LatDriver")
                        .HasColumnType("float");

                    b.Property<bool>("Lock")
                        .HasColumnType("bit");

                    b.Property<double>("LongDriver")
                        .HasColumnType("float");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("DriverId");

                    b.ToTable("UserDriver");
                });

            modelBuilder.Entity("DriverService.Models.Saldo", b =>
                {
                    b.HasOne("DriverService.Models.Pengguna", "Pengguna")
                        .WithMany("Saldos")
                        .HasForeignKey("PenggunaId")
                        .HasConstraintName("FK_Saldo_Penggunas")
                        .IsRequired();

                    b.Navigation("Pengguna");
                });

            modelBuilder.Entity("DriverService.Models.SaldoDriver", b =>
                {
                    b.HasOne("DriverService.Models.UserDriver", "Driver")
                        .WithMany("SaldoDrivers")
                        .HasForeignKey("DriverId")
                        .HasConstraintName("FK_SaldoDriver_UserDriver")
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("DriverService.Models.Pengguna", b =>
                {
                    b.Navigation("Saldos");
                });

            modelBuilder.Entity("DriverService.Models.UserDriver", b =>
                {
                    b.Navigation("SaldoDrivers");
                });
#pragma warning restore 612, 618
        }
    }
}
