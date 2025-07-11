using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit.Models;

namespace tcm_edi_audit.Extensions
{
    public static class EdiObjectExtensions
    {
        public static List<EdiLine> GetEdiLinesByCode(this List<EdiLine> ediLines, string lineCode)
        {
            return ediLines.Where(w => w.LineCode == lineCode).ToList();
        }

        public static EdiLine Get329Peer(this List<EdiLine> ediLines, string knowledgeNumber)
        {
            return ediLines.FirstOrDefault(w => w.LineCode == "329" && w.Columns[12].Content.StartsWith(knowledgeNumber));
        }
    }
}
