using System.Data;
using tcm_edi_audit_core.Extensions;
using tcm_edi_audit_core.Models.EDI;
using tcm_edi_audit_core.Models.Settings;

namespace tcm_edi_audit_core
{
    public partial class frmValidatorResult : Form
    {
        private AppSettingsLocal _localSettings;
        private List<EdiValidationResult> _validationResults;
        private List<Models.DTOs.EdiValidationDisplayModel> _validationDisplayItems;

        public frmValidatorResult(List<EdiValidationResult> validationResults, AppSettingsLocal localSettings)
        {
            InitializeComponent();

            _localSettings = localSettings ?? throw new ArgumentNullException(nameof(localSettings));
            _validationResults = validationResults ?? throw new ArgumentNullException(nameof(validationResults));
            _validationDisplayItems = _validationResults.ToDisplayModel(ckbExpandSelection.Checked);

            lblErrorCount.Text = _validationResults.Count(w => w.Status == EdiValidationStatus.Error).ToString("00");
            lblWarningCount.Text = _validationResults.Count(w => w.Status == EdiValidationStatus.Warning).ToString("00");
            lblSuccessCount.Text = _validationResults.Count(w => w.Status == EdiValidationStatus.Success).ToString("00");
        }

        private void frmValidatorResult_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new BindingSource { DataSource = _validationDisplayItems };
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersWidth = 35;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            PopulateComboboxes();
        }

        private void PopulateComboboxes()
        {
            var status = _validationDisplayItems
                            .Select(s => s.Status)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            status.Insert(0, "Todos");
            comboBox1.DataSource = null;
            comboBox1.DataSource = status;

            var files = _validationDisplayItems
                            .Select(s => s.FileName)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            files.Insert(0, "Todos");
            comboBox2.DataSource = null;
            comboBox2.DataSource = files;
        }

        private void ApplyCombinedFilters()
        {
            string status = comboBox1.SelectedItem?.ToString() ?? string.Empty;
            string files = comboBox2.SelectedItem?.ToString() ?? string.Empty;

            var filtered = _validationDisplayItems.AsEnumerable();

            if (!string.IsNullOrEmpty(status) && status != "Todos")
                filtered = filtered.Where(w => w.Status == status);

            if (!string.IsNullOrEmpty(files) && files != "Todos")
                filtered = filtered.Where(w => w.FileName == files);

            filtered = filtered.OrderByPriority();

            dataGridView1.DataSource = new BindingSource { DataSource = filtered };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource = null;

            var files = _validationDisplayItems
                            .Where(w => w.Status == comboBox1.SelectedItem?.ToString() || comboBox1.SelectedItem?.ToString() == "Todos")
                            .Select(s => s.FileName)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            files.Insert(0, "Todos");

            comboBox2.DataSource = files;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();
        }

        private void SaveFiles()
        {
            if (!_validationDisplayItems.IsNullOrEmpty())
            {
                var protocolGroup = _validationResults.GroupBy(g => g.Protocol)
                    .Select(s => new
                    {
                        Protocol = s.Key,
                        Items = s.ToList()
                    }).ToList();

                string dateString = DateTime.Now.ToString("yyyy-MM-dd");

                foreach (var protocolItem in protocolGroup)
                {
                    string outputResultFolderPath = Path.Combine(_localSettings.OutputFolderPath, dateString, protocolItem.Protocol);
                    FileExtensions.CheckOrCreateFolder(outputResultFolderPath);

                    foreach (var item in protocolItem.Items)
                    {
                        if (item.Status == EdiValidationStatus.Warning && item.File != null)
                        {
                            var fixedFileContent = item.EdiLines.ToStringBuild();
                            string fixedFilePath = Path.Combine(outputResultFolderPath, item.File.Name);

                            FileExtensions.CreateOrReplaceFile(fixedFilePath, fixedFileContent);
                        }
                        else if (item.Status == EdiValidationStatus.Success && item.File != null)
                        {
                            FileExtensions.CopyFileReplacingIfExists(item.File.FullName, outputResultFolderPath);
                        }
                        else if (item.Status == EdiValidationStatus.Error && item.File != null)
                        {
                            FileExtensions.DeleteFileIfExists(Path.Combine(outputResultFolderPath, item.File.Name));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Não há arquivos para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                button4.Enabled = false;
                button4.Text = "Salvando...";
                pcbLoading.Visible = true;
            }));

            await Task.Delay(1);

            try
            {
                await Task.Run(() => SaveFiles());
                MessageBox.Show("Arquivos salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            finally
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    button4.Text = "SALVAR";
                    button4.Enabled = true;
                    pcbLoading.Visible = false;
                }));
            }
        }

        private void ckbExpandSelection_CheckedChanged(object sender, EventArgs e)
        {
            _validationDisplayItems = _validationResults.ToDisplayModel(ckbExpandSelection.Checked);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new BindingSource { DataSource = _validationDisplayItems };
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersWidth = 35;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            PopulateComboboxes();
        }
    }
}