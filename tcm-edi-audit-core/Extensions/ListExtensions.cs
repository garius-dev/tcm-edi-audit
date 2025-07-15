using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }
    }
}
