using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class UserPengguna
    {
        public UserPengguna()
        {
            Orders = new HashSet<Order>();
        }

        public int PenggunaId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public float LatPengguna { get; set; }
        public float LongPengguna { get; set; }
        public bool Lock { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
