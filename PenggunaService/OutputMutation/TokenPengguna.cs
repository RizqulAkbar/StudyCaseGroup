using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.OutputMutation
{
    public record TokenPengguna
    (
        string Token,
        string Expired,
        string Message
    );
}