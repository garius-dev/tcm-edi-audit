using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tcm_edi_audit_core.Services;

namespace tcm_edi_audit_core
{
    public partial class frmValidatorResult : Form
    {
        private List<EdiValidatorServiceDGV> _resultsDGV;
        public frmValidatorResult(List<EdiValidatorServiceDGV> resultsDGV)
        {
            InitializeComponent();
            _resultsDGV = resultsDGV;
        }

        private void frmValidatorResult_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _resultsDGV.OrderBy(o => o.Status).ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersWidth = 35;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            var codes = _resultsDGV
                            .Select(s => s.Status)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            codes.Insert(0, "Todos");

            comboBox1.DataSource = codes;


            var typeOfData = _resultsDGV
                            .Select(s => s.FileName)
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

            var filtered = _resultsDGV.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(lineCode) && lineCode != "Todos")
                filtered = filtered.Where(w => w.Status == lineCode);

            if (!string.IsNullOrWhiteSpace(fieldType) && fieldType != "Todos")
                filtered = filtered.Where(w => w.FileName == fieldType);

            dataGridView1.DataSource = new BindingSource { DataSource = filtered.OrderBy(o => o.Status).ToList() };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource = null;

            var typeOfData = _resultsDGV
                            .Where(w => w.Status == comboBox1.SelectedItem?.ToString() || comboBox1.SelectedItem?.ToString() == "Todos")
                            .Select(s => s.FileName)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

            typeOfData.Insert(0, "Todos");

            comboBox2.DataSource = typeOfData;

            ApplyCombinedFilters();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();

        }
    }
}
