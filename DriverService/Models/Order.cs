using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? DriverId { get; set; }
        public int PenggunaId { get; set; }
        public double LatPengguna { get; set; }
        public double LongPengguna { get; set; }
        public double? LatDriver { get; set; }
        public double? LongDriver { get; set; }
        public double LatTujuan { get; set; }
        public double LongTujuan { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}
