using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit.Extensions
{
    public static class TextExtensions
    {
        public static string ToEdiTotalValueText(this decimal value)
        {
            if (value % 1 != 0)
            {
                return value.ToString("F2").Replace(".", string.Empty).Replace(",", string.Empty);
            }
            else
            {
                return value.ToString().Replace(".", string.Empty).Replace(",", string.Empty);
            }
        }
    }
}
