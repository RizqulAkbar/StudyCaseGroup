using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public int PenggunaId { get; set; }
        public float LatPengguna { get; set; }
        public float LongPengguna { get; set; }
        public float? LatDriver { get; set; }
        public float? LongDriver { get; set; }
        public float LatTujuan { get; set; }
        public float LongTujuan { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }

        public virtual UserDriver Driver { get; set; }
        public virtual UserPengguna Pengguna { get; set; }
    }
}
