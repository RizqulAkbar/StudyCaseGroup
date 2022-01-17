using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class PriceAdmin
    {
        public int PriceId { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
    }
}
