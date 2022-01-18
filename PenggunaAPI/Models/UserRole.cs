using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaAPI.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int PenggunaId { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public Pengguna Pengguna { get; set; }
    }
}