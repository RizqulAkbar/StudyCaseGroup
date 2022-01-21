using System;
using System.Collections.Generic;

#nullable disable

namespace PenggunaService.Models
{
    public partial class Pengguna
    {
        public Pengguna()
        {
            SaldoPenggunas = new HashSet<SaldoPengguna>();
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

        public virtual ICollection<SaldoPengguna> SaldoPenggunas { get; set; }
    }
}
