using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit_core.Models.EDI;

namespace tcm_edi_audit_core.Extensions
{
    public static class FileExtensions
    {
        public static string PadRightFixed(string input, int totalLength)
        {
            return input.Length >= totalLength
                ? input.Substring(0, totalLength)
                : input.PadRight(totalLength, ' ');
        }

        public static void CheckOrCreateFolder(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("O caminho da pasta não pode ser nulo ou vazio.", nameof(path));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateOrReplaceFile(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("O caminho do arquivo não pode ser nulo ou vazio.", nameof(path));

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Arquivo existente removido com sucesso.");
                }

                File.WriteAllText(path, content);
                Console.WriteLine("Novo arquivo criado com sucesso.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Erro: Permissão negada para acessar ou modificar o arquivo.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro de E/S ao manipular o arquivo: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao criar o arquivo: {ex.Message}");
            }
        }

        public static void CopyFileReplacingIfExists(string sourcePath, string destinationFolder)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(destinationFolder))
            {
                Console.WriteLine("Erro: Caminho de origem ou destino inválido.");
                return;
            }

            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("Erro: O arquivo de origem não existe.");
                return;
            }

            try
            {
                // Garante que a pasta de destino exista
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                    Console.WriteLine("Pasta de destino criada.");
                }

                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(destinationFolder, fileName);

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                    Console.WriteLine("Arquivo de destino existente removido.");
                }

                File.Copy(sourcePath, destinationPath);
                Console.WriteLine("Arquivo copiado com sucesso.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Erro: Permissão negada para acessar os arquivos.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro de E/S: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }

        public static string ToStringBuild(this List<EdiLine> ediLines)
        {
            var linhas = new List<string>();

            foreach (var ediline in ediLines)
            {
                var lineBuilder = new StringBuilder();

                foreach (var ediColumn in ediline.Columns)
                {
                    var columnString = PadRightFixed(ediColumn.Value, ediColumn.FieldDefinitionRule.TextLength);
                    lineBuilder.Append(columnString);
                }

                var lineText = PadRightFixed(lineBuilder.ToString(), ediline.LineLenght);
                linhas.Add(lineText);
            }

            return string.Join(Environment.NewLine, linhas);
        }
    }
}
