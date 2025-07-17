using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tcm_edi_audit_core.Models.Settings;
using tcm_edi_audit_core.Settings.Services;

namespace tcm_edi_audit_core
{
    public partial class frmConfig : Form
    {




        private AppSettings _settings;
        private ConfigManagerService _configManagerService;

        public frmConfig()
        {
            InitializeComponent();

            _settings = new AppSettings();
            _configManagerService = new ConfigManagerService();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                button7.Image = Properties.Resources.restore_windows_icon_24_24; // coloque seu ícone de restaurar
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                button7.Image = Properties.Resources.maximize_icon_24_24; // ícone original
            }
        }

        private async void frmConfig_Load(object sender, EventArgs e)
        {
            _settings = await _configManagerService.LoadSettingsFromCloud();

            if (!_settings.AdminUsers.Any(a => a.UserAccount == Environment.UserName))
            {
                MessageBox.Show("Você não tem permissão para acessar as configurações.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            _settings.EdiFieldDefinitions = _settings.EdiFieldDefinitions.OrderBy(o => o.LineCode).ThenBy(o => o.ColumnId).ToList();
            _settings.AdminUsers = _settings.AdminUsers.OrderBy(o => o.UserName).ToList();

            dgvVehicles.DataSource = new BindingSource { DataSource = _settings.Vehicles };
            dgvBranches.DataSource = new BindingSource { DataSource = _settings.Branches };
            dgvCollections.DataSource = new BindingSource { DataSource = _settings.CollectTypes };
            dgvEdiFieldValidationSettings.DataSource = new BindingSource { DataSource = _settings.EdiFieldDefinitions };

            dgvAdmins.DataSource = new BindingSource { DataSource = _settings.AdminUsers };

            dgvCodeDefinitions.DataSource = new BindingSource { DataSource = _settings.EdiLineCodeDefinitions };

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

            dgvAdmins.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAdmins.RowHeadersWidth = 35;
            dgvAdmins.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dgvCodeDefinitions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCodeDefinitions.RowHeadersWidth = 35;
            dgvCodeDefinitions.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            var codes = _settings.EdiFieldDefinitions
                            .Select(s => s.LineCode)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            codes.Insert(0, "Todos");

            comboBox1.DataSource = codes;


            var typeOfData = _settings.EdiFieldDefinitions
                            .Select(s => s.FieldType)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            typeOfData.Insert(0, "Todos");

            comboBox2.DataSource = typeOfData;
        }

        private void ApplyCombinedFilters()
        {
            string lineCode = comboBox1.SelectedItem?.ToString();
            string fieldType = comboBox2.SelectedItem?.ToString();

            var filtered = _settings.EdiFieldDefinitions.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(lineCode) && lineCode != "Todos")
                filtered = filtered.Where(w => w.LineCode == lineCode);

            if (!string.IsNullOrWhiteSpace(fieldType) && fieldType != "Todos")
                filtered = filtered.Where(w => w.FieldType == fieldType);

            dgvEdiFieldValidationSettings.DataSource = new BindingSource { DataSource = filtered.ToList() };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Força o fim da edição no DataGridView para garantir que os dados sejam atualizados
            this.Validate();

            // Salva as configurações (que foram atualizadas automaticamente pela ligação de dados)
            //ConfigManager.SaveSettings(_settings);

            button2.Enabled = false;
            button2.Text = "Salvando...";

            try
            {
                await _configManagerService.SaveSettingsToCloud(_settings);

                MessageBox.Show("Configurações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Sinaliza que as configs foram salvas
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            finally
            {
                button2.Text = "Salvar";
                button2.Enabled = true;
            }
        }
    }
}
