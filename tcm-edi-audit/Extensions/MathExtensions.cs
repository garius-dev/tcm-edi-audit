using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit.Models.Settings;

namespace tcm_edi_audit.Extensions
{
    public static class MathExtensions
    {
        public static decimal TruncDecimal(this decimal value)
        {
            return Math.Truncate(value * 100) / 100;

        }
    }
}
