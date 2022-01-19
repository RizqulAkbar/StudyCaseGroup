using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.Models
{
    public partial class UserDriver
    {
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
    }
}
