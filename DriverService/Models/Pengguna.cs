using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class Pengguna
    {
        public Pengguna()
        {
            Saldos = new HashSet<Saldo>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsLocked { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Saldo> Saldos { get; set; }
    }
}
