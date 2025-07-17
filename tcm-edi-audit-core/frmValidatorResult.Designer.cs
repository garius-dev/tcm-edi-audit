namespace tcm_edi_audit_core
{
    partial class frmValidatorResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValidatorResult));
            panel8 = new Panel();
            button4 = new ReaLTaiizor.Controls.Button();
            pcbLoading = new PictureBox();
            panel11 = new Panel();
            comboBox2 = new ComboBox();
            label10 = new Label();
            panel10 = new Panel();
            comboBox1 = new ComboBox();
            label9 = new Label();
            panel2 = new Panel();
            ckbExpandSelection = new ReaLTaiizor.Controls.MaterialCheckBox();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcbLoading).BeginInit();
            panel11.SuspendLayout();
            panel10.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel8
            // 
            panel8.Controls.Add(button4);
            panel8.Controls.Add(pcbLoading);
            panel8.Controls.Add(panel11);
            panel8.Controls.Add(panel10);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(8, 8);
            panel8.Margin = new Padding(0);
            panel8.Name = "panel8";
            panel8.Padding = new Padding(8, 8, 8, 0);
            panel8.Size = new Size(837, 72);
            panel8.TabIndex = 1;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.BackColor = Color.Transparent;
            button4.BorderColor = Color.FromArgb(40, 167, 69);
            button4.Cursor = Cursors.Hand;
            button4.EnteredBorderColor = Color.FromArgb(30, 126, 52);
            button4.EnteredColor = Color.FromArgb(33, 136, 56);
            button4.Font = new Font("Microsoft Sans Serif", 12F);
            button4.Image = null;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.InactiveColor = Color.FromArgb(40, 167, 69);
            button4.Location = new Point(706, 37);
            button4.Name = "button4";
            button4.PressedBorderColor = Color.FromArgb(28, 116, 48);
            button4.PressedColor = Color.FromArgb(30, 126, 52);
            button4.Size = new Size(123, 28);
            button4.TabIndex = 13;
            button4.Text = "SALVAR";
            button4.TextAlignment = StringAlignment.Center;
            button4.Click += button4_Click;
            // 
            // pcbLoading
            // 
            pcbLoading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pcbLoading.Image = Properties.Resources.loading_35;
            pcbLoading.Location = new Point(602, 39);
            pcbLoading.Name = "pcbLoading";
            pcbLoading.Size = new Size(168, 25);
            pcbLoading.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLoading.TabIndex = 14;
            pcbLoading.TabStop = false;
            pcbLoading.Visible = false;
            // 
            // panel11
            // 
            panel11.Controls.Add(comboBox2);
            panel11.Controls.Add(label10);
            panel11.Dock = DockStyle.Left;
            panel11.Location = new Point(246, 8);
            panel11.Name = "panel11";
            panel11.Size = new Size(238, 64);
            panel11.TabIndex = 1;
            // 
            // comboBox2
            // 
            comboBox2.Dock = DockStyle.Left;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(0, 29);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(230, 28);
            comboBox2.TabIndex = 3;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Top;
            label10.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(64, 64, 64);
            label10.Location = new Point(0, 0);
            label10.Margin = new Padding(0);
            label10.Name = "label10";
            label10.Padding = new Padding(0, 6, 6, 6);
            label10.Size = new Size(82, 29);
            label10.TabIndex = 2;
            label10.Text = "Arquivos:";
            // 
            // panel10
            // 
            panel10.Controls.Add(comboBox1);
            panel10.Controls.Add(label9);
            panel10.Dock = DockStyle.Left;
            panel10.Location = new Point(8, 8);
            panel10.Name = "panel10";
            panel10.Size = new Size(238, 64);
            panel10.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Left;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0, 29);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(230, 28);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Top;
            label9.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(64, 64, 64);
            label9.Location = new Point(0, 0);
            label9.Margin = new Padding(0);
            label9.Name = "label9";
            label9.Padding = new Padding(0, 6, 6, 6);
            label9.Size = new Size(65, 29);
            label9.TabIndex = 2;
            label9.Text = "Status:";
            // 
            // panel2
            // 
            panel2.Controls.Add(ckbExpandSelection);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(8, 430);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(837, 51);
            panel2.TabIndex = 3;
            // 
            // ckbExpandSelection
            // 
            ckbExpandSelection.AutoSize = true;
            ckbExpandSelection.Depth = 0;
            ckbExpandSelection.Dock = DockStyle.Left;
            ckbExpandSelection.Location = new Point(8, 8);
            ckbExpandSelection.Margin = new Padding(0);
            ckbExpandSelection.MouseLocation = new Point(-1, -1);
            ckbExpandSelection.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            ckbExpandSelection.Name = "ckbExpandSelection";
            ckbExpandSelection.ReadOnly = false;
            ckbExpandSelection.Ripple = true;
            ckbExpandSelection.Size = new Size(155, 35);
            ckbExpandSelection.TabIndex = 15;
            ckbExpandSelection.Text = "Expandir seleção";
            ckbExpandSelection.UseAccentColor = false;
            ckbExpandSelection.UseVisualStyleBackColor = true;
            ckbExpandSelection.CheckedChanged += ckbExpandSelection_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(8, 80);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(8);
            panel1.Size = new Size(837, 350);
            panel1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(8, 8);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(821, 334);
            dataGridView1.TabIndex = 0;
            // 
            // frmValidatorResult
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(853, 489);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel8);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmValidatorResult";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EDI AUDIT - Resultados";
            Load += frmValidatorResult_Load;
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pcbLoading).EndInit();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel8;
        private Panel panel11;
        private ComboBox comboBox2;
        private Label label10;
        private Panel panel10;
        private ComboBox comboBox1;
        private Label label9;
        private ReaLTaiizor.Controls.Button button4;
        private PictureBox pcbLoading;
        private Panel panel2;
        private Panel panel1;
        private DataGridView dataGridView1;
        private ReaLTaiizor.Controls.MaterialCheckBox ckbExpandSelection;
    }
}