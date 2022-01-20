using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.OutputQuery
{
    public class OrderHistory
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public int PenggunaId { get; set; }
        public string StartingLocation { get; set; }
        public string Destination { get; set; }
        public DateTime Created { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
    }
}