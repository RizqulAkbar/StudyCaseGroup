using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.OutputQuery
{
    public class OrderFee
    {
        public int PenggunaId { get; set; }
        public float Fee { get; set; }
        public DateTime Created { get; set; }
    }
}