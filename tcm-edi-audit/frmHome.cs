using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Windows.Forms;
using tcm_edi_audit.Models.Settings;
using tcm_edi_audit.Services;
using tcm_edi_audit.Services.Settings;

namespace tcm_edi_audit
{
    public partial class frmHome : Form
    {
        private AppSettings _settings;
        private SystemSettings _systemSettings;

        public frmHome()
        {
            InitializeComponent();
        }

        private int? ProximoIndice<T>(List<T> lista, int fromIndex, Func<T, bool> predicate)
        {
            return lista
                .Skip(fromIndex + 1)
                .Select((item, i) => new { Item = item, Index = i + fromIndex + 1 })
                .FirstOrDefault(x => predicate(x.Item))?.Index;
        }

        public string ReadFileSettings(string[] ediLines)
        {
            string[] rulesCode = new string[] { "000", "320", "321", "322", "329", "323" };

            List<(string line, int start, int end)> linesToProcess = new List<(string, int, int)>();

            for (int i = 0; i < ediLines.Length; i++)
            {
                var regex = new Regex(@"TPREGISTRO\s*:\s*(000|320|321|322|323|329)");
                var match = regex.Match(ediLines[i]);
                if (match.Success)
                {
                    string code = match.Groups[1].Value;

                    int n = ProximoIndice(ediLines.ToList(), i, x => string.IsNullOrEmpty(x.Trim())) ?? ediLines.Length;
                    linesToProcess.Add((code, i, n));
                }
            }

            List<EdiFieldValidationSettings> ediFieldValidationSettings = new List<EdiFieldValidationSettings>();

            foreach (var line in linesToProcess)
            {
                var sublista = ediLines.ToList().GetRange(line.start, line.end - line.start + 1).Where(w => !string.IsNullOrEmpty(w.Trim())).ToList();
                int columnId = 0;

                foreach (var subItem in sublista)
                {
                    var rule = subItem.Split(':')[0].Trim();
                    var splitRule = Regex.Split(rule, @"\s+");

                    string campo = splitRule[4];
                    string tipo = splitRule[3];
                    string dec = splitRule[2];
                    string tam = splitRule[1];
                    string ini = splitRule[0];

                    int iniInt = int.Parse(ini) - 1;
                    int tamInt = int.Parse(tam);
                    int decInt = int.Parse(dec);

                    ediFieldValidationSettings.Add(new EdiFieldValidationSettings
                    {
                        ColumnId = columnId,
                        LineCode = line.line,
                        FieldName = campo,
                        FieldType = tipo,
                        TextStartPosition = iniInt,
                        TextLength = tamInt,
                        Dec = decInt
                    });

                    columnId++;
                }
            }

            return JsonConvert.SerializeObject(ediFieldValidationSettings, Formatting.Indented);
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione o arquivo EDI";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    //try
                    //{
                    //    string[] ediLines = System.IO.File.ReadAllLines(filePath);
                    //    //var decodedConfig = ReadFileSettings(ediLines);

                    //    //foreach(var line in ediLines)
                    //    //{
                    //    //    string lineCode = line.Substring(0, 3).Trim();
                    //    //    var foundRule = _settings.FileConfigSettings.FirstOrDefault(f => f.Code == lineCode);

                    //    //    if(foundRule != null)
                    //    //    {
                    //    //        foreach(var columnRule in foundRule.Fields)
                    //    //        {
                    //    //            var columnValue = line.Substring(columnRule.Ini, columnRule.Tam);
                    //    //        }
                    //    //    }

                    //    //}

                    //    var parser = new EdiParser(_settings);
                    //    parser.Parse(ediLines);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show($"Erro ao carregar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
        }

        private async void frmHome_Load(object sender, EventArgs e)
        {
            _settings = ConfigManager.LoadSettings();

            _systemSettings = SystemConfigManager.LoadSettings();

            txtFolderPath.Text = _systemSettings.FolderPath;
            txtExcelPath.Text = _systemSettings.ExcelPath;
        }

        public async Task<string> ObterIdTokenAnonimoAsync(string apiKey)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return obj?["idToken"]?.ToString();
        }

        public async Task<string> LerFirebaseComTokenAsync(string idToken)
        {
            var client = new HttpClient();
            var url = $"https://tcmsharedsettings-default-rtdb.firebaseio.com/.json?auth={idToken}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmConfig frmConfig = new frmConfig();

            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                _settings = ConfigManager.LoadSettings();
            }
        }

        private void btnSelectFolderPath_Click(object sender, EventArgs e)
        {
        }

        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
        }

        private void txtFolderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Selecione a pasta onde os arquivos EDI serão salvos",
                ShowNewFolderButton = true
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;
                _systemSettings.FolderPath = selectedPath;
                SystemConfigManager.SaveSettings(_systemSettings); // Salva as configurações do sistema

                txtFolderPath.Text = selectedPath;
            }
        }

        private void txtExcelPath_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|Todos os Arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione o Planilha de referência";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    _systemSettings.ExcelPath = selectedFile;
                    SystemConfigManager.SaveSettings(_systemSettings);

                    txtExcelPath.Text = selectedFile;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolderPath.Text) && !string.IsNullOrEmpty(txtExcelPath.Text))
            {
                bool existingFile = File.Exists(txtExcelPath.Text);
                bool folderExists = Directory.Exists(txtFolderPath.Text);

                if (existingFile && folderExists)
                {
                    List<TransportRecord> excelContent = new List<TransportRecord>();

                    try
                    {
                        excelContent = TransportRecord.ReadExcelFile(txtExcelPath.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao ler o Excel: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    var invoiceGroup = excelContent.GroupBy(g => g.Invoice)
                        .Select(g => new ExcelRecordGroup
                        {
                            InvoiceNumber = g.Key,
                            Records = g.ToList(),
                            TotalRevenue = g.Sum(s => s.TotalRevenue),
                        }).ToList();

                    if (invoiceGroup != null && invoiceGroup.Any())
                    {
                        var parser = new EdiParser(_settings);

                        foreach (var invoiceItem in invoiceGroup)
                        {

                            var ediFiles = Directory.GetFiles(txtFolderPath.Text, "*.txt")
                                .Where(f => Path.GetFileNameWithoutExtension(f).EndsWith($"_{invoiceItem.InvoiceNumber}"))
                                .ToList();

                            if (ediFiles != null && ediFiles.Any())
                            {
                                foreach (var ediFile in ediFiles)
                                {
                                    string[] ediLines = System.IO.File.ReadAllLines(ediFile);
                                    var parsingResult = parser.Parse(ediLines, invoiceItem);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Excel não retornou nenhuma linha", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Excel não retornou nenhuma linha", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Pasta raiz ou arquivo excel não existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Pasta raiz ou arquivo excel inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}