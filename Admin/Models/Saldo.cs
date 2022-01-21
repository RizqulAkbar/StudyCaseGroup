using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.Models
{
    public partial class Saldo
    {
        public int SaldoId { get; set; }
        public int PenggunaId { get; set; }
        public double TotalSaldo { get; set; }
        public double MutasiSaldo { get; set; }
        public DateTime Created { get; set; }

        public virtual Pengguna Pengguna { get; set; }
    }
}
