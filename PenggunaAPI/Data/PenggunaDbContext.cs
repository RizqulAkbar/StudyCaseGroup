using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PenggunaAPI.Models;

namespace PenggunaAPI.Data
{
    public class PenggunaDbContext : DbContext
    {
        public PenggunaDbContext()
        {
            
        }
        public PenggunaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pengguna> Penggunas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Saldo> Saldos { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}