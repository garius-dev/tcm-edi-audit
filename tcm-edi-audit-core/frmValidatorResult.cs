using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tcm_edi_audit_core.Extensions;
using tcm_edi_audit_core.Models.EDI;
using tcm_edi_audit_core.Models.Settings;
using tcm_edi_audit_core.Services;

namespace tcm_edi_audit_core
{
    public partial class frmValidatorResult : Form
    {
        //private List<EdiValidatorServiceDGV> _resultsDGV;
        //private List<EdiValidatorServiceResultDGV> _resultsConsolidatedDGV;

        private AppSettingsLocal _localSettings;
        private List<EdiValidationResult> _validationResults;

        public frmValidatorResult(List<EdiValidationResult> validationResults, AppSettingsLocal localSettings)
        {
            InitializeComponent();

            _localSettings = localSettings ?? throw new ArgumentNullException(nameof(localSettings));
            _validationResults = validationResults ?? throw new ArgumentNullException(nameof(validationResults));

            var xxx = _validationResults.ToDisplayModel(false);
            var yyy = _validationResults.ToDisplayModel(true);

            //_resultsConsolidatedDGV = resultsDGV;

            //if (!_resultsConsolidatedDGV.IsNullOrEmpty())
            //{
            //    _resultsDGV = _resultsConsolidatedDGV.SelectMany(s => s.ResultItems).ToList();

            //}
            //else
            //{
            //    _resultsDGV = new List<EdiValidatorServiceDGV>();
            //}

        }

        private void frmValidatorResult_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = _resultsDGV.OrderBy(o => o.Status).ToList();

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.RowHeadersWidth = 35;
            //dataGridView1.ReadOnly = true;
            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //var codes = _resultsDGV
            //                .Select(s => s.Status)
            //                .Distinct()
            //                .OrderBy(c => c)
            //                .ToList();

            //codes.Insert(0, "Todos");

            //comboBox1.DataSource = codes;


            //var typeOfData = _resultsDGV
            //                .Select(s => s.FileName)
            //                .Distinct()
            //                .OrderBy(c => c)
            //                .ToList();

            //typeOfData.Insert(0, "Todos");

            //comboBox2.DataSource = typeOfData;
        }

        private void ApplyCombinedFilters()
        {
            //string lineCode = comboBox1.SelectedItem?.ToString();
            //string fieldType = comboBox2.SelectedItem?.ToString();

            //var filtered = _resultsDGV.AsEnumerable();

            //if (!string.IsNullOrWhiteSpace(lineCode) && lineCode != "Todos")
            //    filtered = filtered.Where(w => w.Status == lineCode);

            //if (!string.IsNullOrWhiteSpace(fieldType) && fieldType != "Todos")
            //    filtered = filtered.Where(w => w.FileName == fieldType);

            //dataGridView1.DataSource = new BindingSource { DataSource = filtered.OrderBy(o => o.Status).ToList() };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.DataSource = null;

            //var typeOfData = _resultsDGV
            //                .Where(w => w.Status == comboBox1.SelectedItem?.ToString() || comboBox1.SelectedItem?.ToString() == "Todos")
            //                .Select(s => s.FileName)
            //                .Distinct()
            //                .OrderBy(c => c)
            //                .ToList();

            //typeOfData.Insert(0, "Todos");

            //comboBox2.DataSource = typeOfData;

            //ApplyCombinedFilters();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();

        }

        private void SaveFiles()
        {
            //if (!_resultsConsolidatedDGV.IsNullOrEmpty())
            //{

            //    var protocolGroup = _resultsConsolidatedDGV.GroupBy(g => new { g.ProtocolReference })
            //        .Select(s => new
            //        {
            //            Protocol = s.Key.ProtocolReference,
            //            Items = s.ToList()
            //        }).ToList();

            //    string dateString = DateTime.Now.ToString("yyyy-MM-dd");

            //    foreach (var protocolItem in protocolGroup)
            //    {
            //        var statusGroup = protocolItem.Items.GroupBy(g => new { g.Status })
            //            .Select(s => new
            //            {
            //                Status = s.Key.Status,
            //                Items = s.ToList()
            //            }).ToList();

            //        foreach (var statusItem in statusGroup)
            //        {

            //            foreach (var item in statusItem.Items)
            //            {
            //                var folderNew = Path.Combine(_settings.OutputFolderPath, dateString, item.ProtocolReference);
            //                FileExtensions.CheckOrCreateFolder(folderNew);

            //                if (item.Status.StartsWith("2"))
            //                {
            //                    var fixedFile = item.EdiLines.ToStringBuild();
            //                    FileExtensions.CreateOrReplaceFile(Path.Combine(folderNew, item.FileName), fixedFile);

            //                }
            //                else if (item.Status.StartsWith("1"))
            //                {
            //                    FileExtensions.CopyFileReplacingIfExists(item.FileNameFull, folderNew);
            //                }
            //                else
            //                {
            //                    FileExtensions.DeleteFileIfExists(Path.Combine(folderNew, item.FileName));
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //this.Invoke((MethodInvoker)(() =>
            //{
            //    button4.Enabled = false;
            //    button4.Text = "Salvando...";
            //    pcbLoading.Visible = true;
            //}));

            //await Task.Delay(1);

            //try
            //{
            //    await Task.Run(() => SaveFiles());


            //    this.Invoke((MethodInvoker)(() =>
            //    {
            //        button4.Text = "SALVAR";
            //        button4.Enabled = true;
            //        pcbLoading.Visible = false;
            //    }));

            //    MessageBox.Show("Arquivos salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Erro: {ex.Message}");
            //}
            //finally
            //{
            //    this.Invoke((MethodInvoker)(() =>
            //    {
            //        button4.Text = "SALVAR";
            //        button4.Enabled = true;
            //        pcbLoading.Visible = false;
            //    }));
            //}
        }
    }
}
