using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int? DriverId { get; set; }
        public int PenggunaId { get; set; }
        public double LatPengguna { get; set; }
        public double LongPengguna { get; set; }
        public double? LatDriver { get; set; }
        public double? LongDriver { get; set; }
        public double LatTujuan { get; set; }
        public double LongTujuan { get; set; }
        public DateTime Created { get; set; }
        public float? Price { get; set; }
        public string Status { get; set; }
    }
}