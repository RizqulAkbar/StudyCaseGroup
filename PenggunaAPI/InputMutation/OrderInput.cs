using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaAPI.InputMutation
{
    public record OrderInput
    (
        int PenggunaId,
        double LatPengguna,
        double LongPengguna,
        double LatTujuan,
        double LongTujuan
    );
}