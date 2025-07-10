using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit.Models
{
    public class EdiLine
    {
        public string LineCode { get; set; }
        public int Id { get; set; }

        public bool HasErrors { get; set; }
        public List<EdiColumn> Columns { get; set; } = new List<EdiColumn>();

        public EdiLine()
        {
            
        }

        public EdiLine(int id, string lineCode, List<EdiColumn> columns)
        {
            this.Id = id;
            this.LineCode = lineCode;
            if(columns != null && columns.Any())
            {
                this.Columns.AddRange(columns);
            }
        }

        public EdiLine(int id, string lineCode)
        {
            this.Id = id;
            this.LineCode = lineCode;
        }

        public EdiLine AddValidColumn(int id, string content)
        {
            Columns.Add(EdiColumn.AddValid(id, content));
            return this;
        }

        public EdiLine AddInvalidColumn(int id, string content, string error)
        {
            Columns.Add(EdiColumn.AddInvalid(id, content, error));
            return this;
        }
    }

    public class EdiColumn
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsValidated { get; set; } = true;
        public string Error { get; set; }

        public static EdiColumn AddValid(int id, string content)
        {
            return new EdiColumn { Id = id, Content = content, IsValidated = true, Error = null };
        }

        public static EdiColumn AddInvalid(int id, string content, string error)
        {
            return new EdiColumn { Id = id, Content = content, IsValidated = false, Error = error };
        }
    }
}
