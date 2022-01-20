using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaService.OutputMutation
{
    public record Status
    (
        bool IsSucceed,
        string Message
    );
}