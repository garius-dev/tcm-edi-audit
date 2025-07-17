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
        public static List<EdiValidationDisplayModel> ToDisplayModel(this List<EdiValidationResult> validationResult, bool expandSelection = false)
        {

            if (!expandSelection)
            {
                var ediValidationDisplayItems = validationResult.Select(s => new EdiValidationDisplayModel
                {
                    Status = s.Status.ToString(),
                    StatusIcon = s.StatusIcon,
                    FileName = s.File?.Name ?? "",
                    Message = s.Status == EdiValidationStatus.Sucess ?
                "Sucesso!" : s.Status == EdiValidationStatus.Warning ? "Arquivo corrigido." : "Erro.",
                    Protocol = s.Protocol
                }).OrderBy(o => o.Status).ThenBy(o => o.Protocol).ToList();

                return ediValidationDisplayItems;
            }
            else
            {
                return FlatItems(validationResult);
            }
        }

        private static List<EdiValidationDisplayModel> FlatItems(List<EdiValidationResult> validationResults)
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

                    if(validationResult.Status == EdiValidationStatus.Sucess)
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

            return ediValidationDisplayItems.OrderBy(o => o.Status).ThenBy(o => o.Protocol).ToList();
        }
    }
}
