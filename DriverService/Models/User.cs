using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsLocked { get; set; }
    }
}
