using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class UserDriver
    {
        public UserDriver()
        {
            Orders = new HashSet<Order>();
            SaldoDrivers = new HashSet<SaldoDriver>();
        }

        public int DriverId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double LatDriver { get; set; }
        public double LongDriver { get; set; }
        public bool Lock { get; set; }
        public bool Approved { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<SaldoDriver> SaldoDrivers { get; set; }
    }
}
