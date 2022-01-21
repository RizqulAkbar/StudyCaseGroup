using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.Models
{
    public class SaldoPengguna
    {
        [Key]
        public int SaldoId { get; set; }
        public int PenggunaId { get; set; }
        public float TotalSaldo { get; set; }
        public float? MutasiSaldo { get; set; }
        public DateTime Created { get; set; }
        public Pengguna Pengguna { get; set; }
    }
}