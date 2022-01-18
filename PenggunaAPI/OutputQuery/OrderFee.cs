using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaAPI.OutputQuery
{
    public class OrderFee
    {
        public int OrderId { get; set; }
        public int PenggunaId { get; set; }
        public DateTime Created { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
    }
}