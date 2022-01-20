using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.Models
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }
        public float PricePerKm { get; set; }
    }
}