using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit_core.Models.DTOs;
using tcm_edi_audit_core.Models.EDI;

namespace tcm_edi_audit_core.Extensions
{
    public static class EdiValidationExtensions
    {
        private static readonly Dictionary<string, int> priorityOrder = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "Success", 0 },
                { "Error",    1 },
                { "Warning", 2 }
            };

        public static List<EdiValidationDisplayModel> OrderByPriority(this IEnumerable<EdiValidationDisplayModel> items)
        {
            return items
                .OrderBy(o => priorityOrder.ContainsKey(o.Status) ? priorityOrder[o.Status] : int.MaxValue)
                .ThenBy(o => o.Protocol)
                .ToList();
        }

        public static List<EdiValidationDisplayModel> ToDisplayModel(this List<EdiValidationResult> validationResult, bool expandSelection = false)
        {
            
            if (!expandSelection)
            {
                var ediValidationDisplayItems = validationResult.Select(s => new EdiValidationDisplayModel
                {
                    Status = s.Status.ToString(),
                    StatusIcon = s.StatusIcon,
                    FileName = s.File?.Name ?? "",
                    Message = s.Status == EdiValidationStatus.Success ?
                "Sucesso!" : s.Status == EdiValidationStatus.Warning ? "Arquivo corrigido." : "Erro (expanda a seleção para mais detalhes)",
                    Protocol = s.Protocol
                }).OrderByPriority();

                return ediValidationDisplayItems;
            }
            else
            {
                return FlattenItems(validationResult);
            }
        }

        private static List<EdiValidationDisplayModel> FlattenItems(List<EdiValidationResult> validationResults)
        {
            List<EdiValidationDisplayModel> ediValidationDisplayItems = new List<EdiValidationDisplayModel>();

            if (!validationResults.IsNullOrEmpty())
            {
                foreach(var validationResult in validationResults)
                {
                    foreach(var erro in validationResult.Errors)
                    {
                        ediValidationDisplayItems.Add(new EdiValidationDisplayModel()
                        {
                            Status = validationResult.Status.ToString(),
                            StatusIcon = validationResult.StatusIcon,
                            FileName = validationResult.File?.Name ?? "",
                            Protocol = validationResult.Protocol,
                            Message = erro
                        });
                    }

                    foreach (var warning in validationResult.Warnings)
                    {
                        ediValidationDisplayItems.Add(new EdiValidationDisplayModel()
                        {
                            Status = validationResult.Status.ToString(),
                            StatusIcon = validationResult.StatusIcon,
                            FileName = validationResult.File?.Name ?? "",
                            Protocol = validationResult.Protocol,
                            Message = warning
                        });
                    }

                    if(validationResult.Status == EdiValidationStatus.Success)
                    {
                        ediValidationDisplayItems.Add(new EdiValidationDisplayModel()
                        {
                            Status = validationResult.Status.ToString(),
                            StatusIcon = validationResult.StatusIcon,
                            FileName = validationResult.File?.Name ?? "",
                            Protocol = validationResult.Protocol,
                            Message = "Sucesso!"
                        });
                    }
                }
            }

            return ediValidationDisplayItems.OrderByPriority();
        }
    }
}
