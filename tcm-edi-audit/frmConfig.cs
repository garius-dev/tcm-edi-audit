using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tcm_edi_audit.Models.Settings;
using tcm_edi_audit.Services.Settings;

namespace tcm_edi_audit
{
    public partial class frmConfig : Form
    {
        private AppSettings _settings;

        public frmConfig()
        {
            InitializeComponent();
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(195, 195, 195), 2))
            {
                e.Graphics.DrawLine(pen, 0, 0, t0_p0_footer.Width, 0);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Força o fim da edição no DataGridView para garantir que os dados sejam atualizados
            this.Validate();

            // Salva as configurações (que foram atualizadas automaticamente pela ligação de dados)
            //ConfigManager.SaveSettings(_settings);

            var idToken = await FirebaseService.GetAnonymousIdTokenAsync("AIzaSyAoBr5P2NQifKQVbKHNSsxHMZr0uvdc7vs");
            await FirebaseService.UpdateConfigJsonAsync(idToken, _settings);

            MessageBox.Show("Configurações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK; // Sinaliza que as configs foram salvas
            this.Close();
        }

        private async void frmConfig_Load(object sender, EventArgs e)
        {
            // Carrega as configurações quando o formulário é aberto
            //_settings = ConfigManager.LoadSettings();

            //var idToken = await FirebaseService.GetAnonymousIdTokenAsync("AIzaSyAoBr5P2NQifKQVbKHNSsxHMZr0uvdc7vs");
            var configResponse = await FirebaseService.GetCachedOrUpdatedConfigAsync("AIzaSyAoBr5P2NQifKQVbKHNSsxHMZr0uvdc7vs");
            _settings = JsonConvert.DeserializeObject<AppSettings>(configResponse);

            // Vincula os dados das configurações aos DataGridViews
            // O BindingSource ajuda a gerenciar a ligação de dados de forma mais robusta
            dgvVehicles.DataSource = new BindingSource { DataSource = _settings.Vehicles };
            dgvBranches.DataSource = new BindingSource { DataSource = _settings.Branches };
            dgvCollections.DataSource = new BindingSource { DataSource = _settings.Collections };
            dgvEdiFieldValidationSettings.DataSource = new BindingSource { DataSource = _settings.EdiFieldValidation.OrderBy(o => o.LineCode).ThenBy(o => o.ColumnId).ToList() };

            // Ajusta as colunas para preencher o espaço
            dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVehicles.RowHeadersWidth = 35;
            dgvVehicles.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;


            dgvBranches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBranches.RowHeadersWidth = 35;
            dgvBranches.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;


            dgvCollections.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCollections.RowHeadersWidth = 35;
            dgvCollections.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dgvEdiFieldValidationSettings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEdiFieldValidationSettings.RowHeadersWidth = 35;
            dgvEdiFieldValidationSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;




            var codes = _settings.EdiFieldValidation
                            .Select(s => s.LineCode)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            codes.Insert(0, "Todos");

            comboBox1.DataSource = codes;


            var typeOfData = _settings.EdiFieldValidation
                            .Select(s => s.FieldType)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            typeOfData.Insert(0, "Todos");

            comboBox2.DataSource = typeOfData;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ApplyCombinedFilters()
        {
            string lineCode = comboBox1.SelectedItem?.ToString();
            string fieldType = comboBox2.SelectedItem?.ToString();

            var filtered = _settings.EdiFieldValidation.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(lineCode) && lineCode != "Todos")
                filtered = filtered.Where(w => w.LineCode == lineCode);

            if (!string.IsNullOrWhiteSpace(fieldType) && fieldType != "Todos")
                filtered = filtered.Where(w => w.FieldType == fieldType);

            dgvEdiFieldValidationSettings.DataSource = new BindingSource { DataSource = filtered.ToList() };
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();
        }
    }
}
