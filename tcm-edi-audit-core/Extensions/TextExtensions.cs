using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Extensions
{
    public static class TextExtensions
    {
        public static string ToEdiTotalValueString(this decimal value)
        {
            if (value % 1 != 0)
            {
                value = Math.Truncate(value * 100) / 100;
                return value.ToString("F2")
                            .Replace(".", string.Empty)
                            .Replace(",", string.Empty);
            }
            else
            {
                return value.ToString()
                            .Replace(".", string.Empty)
                            .Replace(",", string.Empty);
            }
        }

        public static bool ValidateStringBounds(this string text, int startPosition, int textLenght)
        {
            return startPosition >= 0 && startPosition + textLenght <= text.Length;
        }
    }
}
