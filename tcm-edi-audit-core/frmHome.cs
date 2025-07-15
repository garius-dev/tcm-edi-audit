using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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


        public frmHome()
        {
            InitializeComponent();

            _configManagerService = new ConfigManagerService();
            _settings = new AppSettings();
            _localSettings = new AppSettingsLocal();
            _fileManagerService = new FileManagerService();
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
            _settings = await _configManagerService.LoadSettingsFromCloud();
            _localSettings = _configManagerService.LoadSettings();

            txtFolderPath.Text = _localSettings.SourceFolderPath;
            txtExcelPath.Text = _localSettings.ReferenceExcelFilePath;
            txtOutputPath.Text = _localSettings.OutputFolderPath;
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
                var filesValidated = await Task.Run(() => ValidateFiles());

                frmValidatorResult frmValidatorResult = new frmValidatorResult(filesValidated);

                this.Invoke((MethodInvoker)(() =>
                {
                    button4.Text = "AUDITAR";
                    button4.Enabled = true;
                    pcbLoading.Visible = false;
                }));

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

        private async Task<List<EdiValidatorServiceDGV>> ValidateFiles()
        {

            EdiParserService parser = new EdiParserService(_settings);
            EdiValidatorService validatorService = new EdiValidatorService(_settings);
            ProtocolReferenceLoader protocolReferenceLoader = new ProtocolReferenceLoader();
            List<EdiValidationResult> results = new List<EdiValidationResult>();

            var protocolReferences = protocolReferenceLoader.LoadFromExcel(_localSettings.ReferenceExcelFilePath);

            if (!protocolReferences.IsNullOrEmpty())
            {
                foreach (var protocol in protocolReferences)
                {
                    var files = _fileManagerService.GetEdiFiles(_localSettings.SourceFolderPath, null, $"_{protocol.Invoice}");

                    foreach (var file in files)
                    {
                        var fileNameShort = new System.IO.FileInfo(file);

                        var lines = await _fileManagerService.ReadEdiFileAsync(file);
                        var parseResult = parser.ParseFile(lines);
                        if (parseResult.Success)
                        {
                            var result = validatorService.Validate(parseResult.Lines, protocolReferences, protocol.Invoice);
                            result.FileName = fileNameShort.Name;
                            results.Add(result);
                        }

                    }
                }
            }

            List<EdiValidatorServiceDGV> resultsDGV = new List<EdiValidatorServiceDGV>();

            foreach (var result in results)
            {
                foreach (var erro in result.Errors)
                {
                    EdiValidatorServiceDGV ediValidator = new EdiValidatorServiceDGV();
                    ediValidator.StatusIcon = Properties.Resources.circle_red_16_16;
                    ediValidator.FileName = result.FileName;
                    ediValidator.Message = erro;
                    ediValidator.Status = "3 - Error";
                    resultsDGV.Add(ediValidator);
                }

                foreach (var warning in result.Warnings)
                {
                    EdiValidatorServiceDGV ediValidator = new EdiValidatorServiceDGV();
                    ediValidator.StatusIcon = Properties.Resources.circle_yellow_16_16;
                    ediValidator.FileName = result.FileName;
                    ediValidator.Message = warning;
                    ediValidator.Status = "2 - Warning";
                    resultsDGV.Add(ediValidator);
                }

                if (result.Success)
                {
                    EdiValidatorServiceDGV ediValidator = new EdiValidatorServiceDGV();
                    ediValidator.StatusIcon = Properties.Resources.circle_green_16_16;
                    ediValidator.FileName = result.FileName;
                    ediValidator.Message = "Sucesso!";
                    ediValidator.Status = "1 - Success";
                    resultsDGV.Add(ediValidator);
                }

            }

            return resultsDGV;

        }
    }
}
