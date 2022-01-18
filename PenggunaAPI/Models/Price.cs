using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaAPI.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public float PricePerKm { get; set; }
    }
}