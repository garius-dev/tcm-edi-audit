using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Services
{
    public class FileManagerService
    {
        public async Task<string[]> ReadEdiFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return null;

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(stream))
                {
                    var lines = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                            lines.Add(line);
                    }
                    return lines.ToArray();
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Arquivo em uso ou erro de I/O: {ioEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler '{filePath}': {ex.Message}");
                return null;
            }
        }



        public List<string> GetEdiFiles(string folderPath, string prefix = null, string suffix = null)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("O caminho da pasta não pode ser nulo ou vazio.", nameof(folderPath));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"A pasta '{folderPath}' não existe.");

            var files = Directory.GetFiles(folderPath, "*.txt");

            return files.Where(file =>
            {
                var name = Path.GetFileNameWithoutExtension(file);

                bool matchesPrefix = string.IsNullOrEmpty(prefix) || name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
                bool matchesSuffix = string.IsNullOrEmpty(suffix) || name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);

                return matchesPrefix && matchesSuffix;
            }).ToList();
        }
    }
}
