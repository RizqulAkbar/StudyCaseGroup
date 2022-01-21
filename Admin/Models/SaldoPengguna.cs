using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.Models
{
    public partial class SaldoPengguna
    {
        public int SaldoId { get; set; }
        public int PenggunaId { get; set; }
        public float TotalSaldo { get; set; }
        public float? MutasiSaldo { get; set; }
        public DateTime Created { get; set; }

        public virtual Pengguna Pengguna { get; set; }
    }
}
