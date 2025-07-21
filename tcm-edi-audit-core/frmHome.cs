using tcm_edi_audit_core.Extensions;
using tcm_edi_audit_core.Models.EDI;
using tcm_edi_audit_core.Models.Settings;
using tcm_edi_audit_core.Services;
using tcm_edi_audit_core.Settings.Services;

namespace tcm_edi_audit_core
{
    public partial class frmHome : Form
    {
        private ConfigManagerService _configManagerService;
        private AppSettings _settings;
        private AppSettingsLocal _localSettings;
        private FileManagerService _fileManagerService;
        private List<Models.EDI.Settings.ExcelEntry> _excelEntries;
        private ExcelService _excelService;
        public frmHome()
        {
            InitializeComponent();

            _configManagerService = new ConfigManagerService();
            _settings = new AppSettings();
            _localSettings = new AppSettingsLocal();
            _fileManagerService = new FileManagerService();
            _excelService = new ExcelService();
            _excelEntries = new List<Models.EDI.Settings.ExcelEntry>();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmConfig configForm = new frmConfig();
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                // Aqui você pode adicionar lógica para lidar com a configuração, se necessário
            }
        }

        private async void frmHome_Load(object sender, EventArgs e)
        {
            

            var currentUser = Environment.UserName;

            _settings = await _configManagerService.LoadSettingsFromCloud();
            _localSettings = _configManagerService.LoadSettings();

            if (!_settings.AdminUsers.Any(a => a.UserAccount == Environment.UserName))
            {
                button6.Enabled = false;
                button6.InactiveColor = System.Drawing.Color.FromArgb(194, 194, 194);
                button6.BorderColor = System.Drawing.Color.FromArgb(194, 194, 194);
            }

            txtFolderPath.Text = _localSettings.SourceFolderPath;
            txtExcelPath.Text = _localSettings.ReferenceExcelFilePath;
            txtOutputPath.Text = _localSettings.OutputFolderPath;
            ckbFixIt.Checked = _localSettings.TryFixIt;

            ValidatePreConfig();
            TryLoadExcelFile();
        }

        private void ValidatePreConfig()
        {
            button4.Enabled = !string.IsNullOrEmpty(txtFolderPath.Text) && !string.IsNullOrEmpty(txtExcelPath.Text) && !string.IsNullOrEmpty(txtExcelPath.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Selecione a pasta de exportação dos arquivos EDI",
                ShowNewFolderButton = true
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;
                txtFolderPath.Text = selectedPath;

                _localSettings.SourceFolderPath = selectedPath;
                _configManagerService.SaveSettings(_localSettings);
            }

            ValidatePreConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|Todos os Arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione o Planilha de referência";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    txtExcelPath.Text = selectedFile;

                    _localSettings.ReferenceExcelFilePath = selectedFile;
                    _configManagerService.SaveSettings(_localSettings);

                    TryLoadExcelFile(true);
                }
            }

            ValidatePreConfig();
        }

        private void TryLoadExcelFile(bool preView = false)
        {
            if (!string.IsNullOrEmpty(txtExcelPath.Text))
            {
                try
                {
                    _excelEntries = _excelService.Load(_localSettings.ReferenceExcelFilePath);
                    if (preView)
                    {
                        frmExcelEntries frmExcelEntries = new frmExcelEntries(_excelEntries);
                        frmExcelEntries.ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossível validar o arquivo excel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExcelPath.Text = string.Empty;
                }
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Selecione a pasta de resultado dos arquivos EDI",
                ShowNewFolderButton = true
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;
                txtOutputPath.Text = selectedPath;

                _localSettings.OutputFolderPath = selectedPath;
                _configManagerService.SaveSettings(_localSettings);
            }

            ValidatePreConfig();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                button4.Enabled = false;
                button4.Text = "Processando...";
                pcbLoading.Visible = true;
            }));

            await Task.Delay(1);

            try
            {
                var filesValidated = await Task.Run(() => ValidateFiles(_localSettings.TryFixIt));

                frmValidatorResult frmValidatorResult = new frmValidatorResult(filesValidated, _localSettings);

                frmValidatorResult.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            finally
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    button4.Text = "AUDITAR";
                    button4.Enabled = true;
                    pcbLoading.Visible = false;
                }));
            }
        }

        private async Task<List<EdiValidationResult>> ValidateFiles(bool tryFixIt = false)
        {
            EdiParserService parser = new EdiParserService(_settings);
            EdiValidatorService validatorService = new EdiValidatorService(_settings);
            ExcelService excelService = new ExcelService();
            List<EdiValidationResult> validatonResults = new List<EdiValidationResult>();

            List<Models.EDI.Settings.ExcelEntry> excelEntries = _excelEntries;

            if (!excelEntries.IsNullOrEmpty())
            {
                foreach (var excelEntry in excelEntries)
                {
                    var files = _fileManagerService.GetEdiFiles(_localSettings.SourceFolderPath, null, $"_{excelEntry.Invoice}");

                    if (!files.IsNullOrEmpty())
                    {
                        foreach (var file in files)
                        {
                            var lines = await _fileManagerService.ReadEdiFileAsync(file.FullName);

                            var parseResult = parser.ParseFile(lines);

                            EdiValidationResult validatonResult = validatorService.Validate(parseResult, excelEntries, excelEntry.Invoice, tryFixIt);
                            validatonResult.File = file;
                            validatonResult.Protocol = excelEntry.Protocol;
                            validatonResults.Add(validatonResult);
                        }
                    }
                    else
                    {
                        // No files found for the current invoice
                    }
                }
            }
            else
            {
                // No entries found in the Excel file
            }

            return validatonResults;
        }

        private void ckbFixIt_CheckedChanged(object sender, EventArgs e)
        {
            _localSettings.TryFixIt = ckbFixIt.Checked;
            _configManagerService.SaveSettings(_localSettings);
        }
    }
}