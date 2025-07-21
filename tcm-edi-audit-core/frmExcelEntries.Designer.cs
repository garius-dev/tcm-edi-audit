namespace tcm_edi_audit_core
{
    partial class frmExcelEntries
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExcelEntries));
            dgvExcelView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvExcelView).BeginInit();
            SuspendLayout();
            // 
            // dgvExcelView
            // 
            dgvExcelView.AllowUserToAddRows = false;
            dgvExcelView.AllowUserToDeleteRows = false;
            dgvExcelView.BorderStyle = BorderStyle.None;
            dgvExcelView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExcelView.Dock = DockStyle.Fill;
            dgvExcelView.Location = new Point(8, 8);
            dgvExcelView.Name = "dgvExcelView";
            dgvExcelView.ReadOnly = true;
            dgvExcelView.Size = new Size(784, 434);
            dgvExcelView.TabIndex = 0;
            // 
            // frmExcelEntries
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvExcelView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmExcelEntries";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EDI AUDIT - Excel View";
            Load += frmExcelEntries_Load;
            ((System.ComponentModel.ISupportInitialize)dgvExcelView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvExcelView;
    }
}