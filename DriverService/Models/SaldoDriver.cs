using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class SaldoDriver
    {
        public int SaldoId { get; set; }
        public int DriverId { get; set; }
        public float TotalSaldo { get; set; }
        public float SelisihSaldo { get; set; }
        public DateTime Created { get; set; }

        public virtual UserDriver Driver { get; set; }
    }
}
