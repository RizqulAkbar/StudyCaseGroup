using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.OutputQuery
{
    public class SaldoOutput
    {
        public int SaldoId { get; set; }
        public int PenggunaId { get; set; }
        public float TotalSaldo { get; set; }
        public float? MutasiSaldo { get; set; }
        public DateTime Created { get; set; }
    }
}