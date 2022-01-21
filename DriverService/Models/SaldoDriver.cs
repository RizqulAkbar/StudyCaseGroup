using System;
using System.Collections.Generic;

#nullable disable

namespace DriverService.Models
{
    public partial class SaldoDriver
    {
        public int SaldoId { get; set; }
        public int DriverId { get; set; }
        public double TotalSaldo { get; set; }
        public double MutasiSaldo { get; set; }
        public DateTime Created { get; set; }

        public virtual UserDriver Driver { get; set; }
    }
}
