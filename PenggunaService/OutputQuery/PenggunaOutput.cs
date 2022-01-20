using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.OutputQuery
{
    public class PenggunaOutput
    {
        public int PenggunaId { get; set; } 
        public string Email { get; set; }   
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }
    };
}