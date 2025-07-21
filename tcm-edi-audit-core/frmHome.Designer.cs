namespace tcm_edi_audit_core
{
    partial class frmHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            panel1 = new Panel();
            ckbFixIt = new ReaLTaiizor.Controls.MaterialCheckBox();
            button4 = new ReaLTaiizor.Controls.Button();
            pcbLoading = new PictureBox();
            panel7 = new Panel();
            txtOutputPath = new TextBox();
            button3 = new Button();
            label2 = new Label();
            panel6 = new Panel();
            txtExcelPath = new TextBox();
            button2 = new Button();
            label1 = new Label();
            panel5 = new Panel();
            txtFolderPath = new TextBox();
            button1 = new Button();
            label6 = new Label();
            panel4 = new Panel();
            panel2 = new Panel();
            label4 = new Label();
            panel3 = new Panel();
            button6 = new ReaLTaiizor.Controls.Button();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcbLoading).BeginInit();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(ckbFixIt);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(pcbLoading);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(8, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(596, 416);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // ckbFixIt
            // 
            ckbFixIt.AutoSize = true;
            ckbFixIt.Depth = 0;
            ckbFixIt.Location = new Point(22, 363);
            ckbFixIt.Margin = new Padding(0);
            ckbFixIt.MouseLocation = new Point(-1, -1);
            ckbFixIt.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            ckbFixIt.Name = "ckbFixIt";
            ckbFixIt.ReadOnly = false;
            ckbFixIt.Ripple = true;
            ckbFixIt.Size = new Size(280, 37);
            ckbFixIt.TabIndex = 14;
            ckbFixIt.Text = "Corrigir arquivos automaticamente";
            ckbFixIt.UseAccentColor = false;
            ckbFixIt.UseVisualStyleBackColor = true;
            ckbFixIt.CheckedChanged += ckbFixIt_CheckedChanged;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BorderColor = Color.FromArgb(40, 167, 69);
            button4.Cursor = Cursors.Hand;
            button4.Enabled = false;
            button4.EnteredBorderColor = Color.FromArgb(30, 126, 52);
            button4.EnteredColor = Color.FromArgb(33, 136, 56);
            button4.Font = new Font("Microsoft Sans Serif", 12F);
            button4.Image = null;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.InactiveColor = Color.FromArgb(40, 167, 69);
            button4.Location = new Point(421, 362);
            button4.Name = "button4";
            button4.PressedBorderColor = Color.FromArgb(28, 116, 48);
            button4.PressedColor = Color.FromArgb(30, 126, 52);
            button4.Size = new Size(153, 39);
            button4.TabIndex = 12;
            button4.Text = "AUDITAR";
            button4.TextAlignment = StringAlignment.Center;
            button4.Click += button4_Click;
            // 
            // pcbLoading
            // 
            pcbLoading.Image = Properties.Resources.loading_35;
            pcbLoading.Location = new Point(313, 369);
            pcbLoading.Name = "pcbLoading";
            pcbLoading.Size = new Size(168, 25);
            pcbLoading.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLoading.TabIndex = 13;
            pcbLoading.TabStop = false;
            pcbLoading.Visible = false;
            // 
            // panel7
            // 
            panel7.Controls.Add(txtOutputPath);
            panel7.Controls.Add(button3);
            panel7.Controls.Add(label2);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 251);
            panel7.Margin = new Padding(0);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(22, 8, 8, 8);
            panel7.Size = new Size(596, 73);
            panel7.TabIndex = 9;
            // 
            // txtOutputPath
            // 
            txtOutputPath.BackColor = Color.White;
            txtOutputPath.Dock = DockStyle.Left;
            txtOutputPath.Font = new Font("Segoe UI", 12F);
            txtOutputPath.Location = new Point(22, 37);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new Size(520, 29);
            txtOutputPath.TabIndex = 7;
            // 
            // button3
            // 
            button3.Cursor = Cursors.Hand;
            button3.Image = Properties.Resources.folder_icon;
            button3.Location = new Point(545, 36);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(29, 29);
            button3.TabIndex = 5;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(64, 64, 64);
            label2.Location = new Point(22, 8);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 6, 6, 6);
            label2.Size = new Size(164, 29);
            label2.TabIndex = 1;
            label2.Text = "Pasta de resultados:";
            // 
            // panel6
            // 
            panel6.Controls.Add(txtExcelPath);
            panel6.Controls.Add(button2);
            panel6.Controls.Add(label1);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 178);
            panel6.Margin = new Padding(0);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(22, 8, 8, 8);
            panel6.Size = new Size(596, 73);
            panel6.TabIndex = 11;
            // 
            // txtExcelPath
            // 
            txtExcelPath.BackColor = Color.White;
            txtExcelPath.Dock = DockStyle.Left;
            txtExcelPath.Font = new Font("Segoe UI", 12F);
            txtExcelPath.Location = new Point(22, 37);
            txtExcelPath.Name = "txtExcelPath";
            txtExcelPath.ReadOnly = true;
            txtExcelPath.Size = new Size(520, 29);
            txtExcelPath.TabIndex = 7;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Image = Properties.Resources.excel_icon_16_16;
            button2.Location = new Point(545, 36);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(29, 29);
            button2.TabIndex = 5;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(22, 8);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 6, 6, 6);
            label1.Size = new Size(117, 29);
            label1.TabIndex = 1;
            label1.Text = "Arquivo Excel:";
            // 
            // panel5
            // 
            panel5.Controls.Add(txtFolderPath);
            panel5.Controls.Add(button1);
            panel5.Controls.Add(label6);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 105);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(22, 8, 8, 8);
            panel5.Size = new Size(596, 73);
            panel5.TabIndex = 8;
            // 
            // txtFolderPath
            // 
            txtFolderPath.BackColor = Color.White;
            txtFolderPath.Dock = DockStyle.Left;
            txtFolderPath.Font = new Font("Segoe UI", 12F);
            txtFolderPath.Location = new Point(22, 37);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(520, 29);
            txtFolderPath.TabIndex = 7;
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Image = Properties.Resources.folder_icon;
            button1.Location = new Point(545, 36);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 5;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(64, 64, 64);
            label6.Location = new Point(22, 8);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Padding = new Padding(0, 6, 6, 6);
            label6.Size = new Size(92, 29);
            label6.TabIndex = 1;
            label6.Text = "Pasta raiz:";
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 80);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(596, 25);
            panel4.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(label4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(596, 80);
            panel2.TabIndex = 4;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Left;
            label4.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(46, 80, 159);
            label4.Location = new Point(141, 8);
            label4.Name = "label4";
            label4.Size = new Size(144, 64);
            label4.TabIndex = 1;
            label4.Text = "EDI AUDIT";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(button6);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(483, 8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(14);
            panel3.Size = new Size(105, 64);
            panel3.TabIndex = 1;
            // 
            // button6
            // 
            button6.BackColor = Color.Transparent;
            button6.BorderColor = Color.FromArgb(0, 123, 255);
            button6.Cursor = Cursors.Hand;
            button6.Dock = DockStyle.Right;
            button6.EnteredBorderColor = Color.FromArgb(0, 98, 204);
            button6.EnteredColor = Color.FromArgb(0, 105, 217);
            button6.Font = new Font("Microsoft Sans Serif", 12F);
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.ImageAlign = ContentAlignment.MiddleCenter;
            button6.InactiveColor = Color.FromArgb(0, 123, 255);
            button6.Location = new Point(55, 14);
            button6.Name = "button6";
            button6.PressedBorderColor = Color.FromArgb(0, 92, 191);
            button6.PressedColor = Color.FromArgb(0, 98, 204);
            button6.Size = new Size(36, 36);
            button6.TabIndex = 1;
            button6.TextAlignment = StringAlignment.Center;
            button6.Click += button6_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Image = Properties.Resources.logo_novo;
            pictureBox1.Location = new Point(8, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(133, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // frmHome
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(46, 80, 159);
            ClientSize = new Size(612, 432);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmHome";
            Padding = new Padding(8);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EDI AUDIT - Home";
            Load += frmHome_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pcbLoading).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label4;
        private Panel panel3;
        private ReaLTaiizor.Controls.Button button6;
        private PictureBox pictureBox1;
        private Button button1;
        private Panel panel4;
        private Panel panel5;
        private TextBox txtFolderPath;
        private Label label6;
        private Panel panel6;
        private TextBox txtExcelPath;
        private Button button2;
        private Label label1;
        private Panel panel7;
        private TextBox txtOutputPath;
        private Button button3;
        private Label label2;
        private ReaLTaiizor.Controls.Button button4;
        private PictureBox pcbLoading;
        private ReaLTaiizor.Controls.MaterialCheckBox ckbFixIt;
    }
}