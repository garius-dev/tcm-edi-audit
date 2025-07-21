using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tcm_edi_audit_core
{
    public partial class frmExcelEntries : Form
    {
        private List<Models.EDI.Settings.ExcelEntry> _excelEntries;

        public frmExcelEntries(List<Models.EDI.Settings.ExcelEntry> excelEntries)
        {
            InitializeComponent();

            _excelEntries = excelEntries;
        }

        private void frmExcelEntries_Load(object sender, EventArgs e)
        {
            dgvExcelView.DataSource = new BindingSource { DataSource = _excelEntries };
            dgvExcelView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExcelView.RowHeadersWidth = 35;
            dgvExcelView.ReadOnly = true;
            dgvExcelView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }
    }
}
