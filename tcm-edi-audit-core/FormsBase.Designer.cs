namespace tcm_edi_audit_core
{
    partial class FormsBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormsBase));
            panel1 = new Panel();
            button7 = new ReaLTaiizor.Controls.Button();
            pictureBox2 = new PictureBox();
            button4 = new ReaLTaiizor.Controls.Button();
            button3 = new ReaLTaiizor.Controls.Button();
            panel2 = new Panel();
            label4 = new Label();
            panel3 = new Panel();
            button6 = new ReaLTaiizor.Controls.Button();
            label3 = new Label();
            button2 = new ReaLTaiizor.Controls.Button();
            label2 = new Label();
            button1 = new ReaLTaiizor.Controls.Button();
            label1 = new Label();
            button5 = new ReaLTaiizor.Controls.Button();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button7);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(8, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 434);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // button7
            // 
            button7.BackColor = Color.Transparent;
            button7.BorderColor = Color.FromArgb(108, 117, 125);
            button7.Cursor = Cursors.Hand;
            button7.EnteredBorderColor = Color.FromArgb(84, 91, 98);
            button7.EnteredColor = Color.FromArgb(90, 98, 104);
            button7.Font = new Font("Microsoft Sans Serif", 12F);
            button7.Image = null;
            button7.ImageAlign = ContentAlignment.MiddleCenter;
            button7.InactiveColor = Color.FromArgb(108, 117, 125);
            button7.Location = new Point(305, 321);
            button7.Name = "button7";
            button7.PressedBorderColor = Color.FromArgb(78, 85, 91);
            button7.PressedColor = Color.FromArgb(84, 91, 98);
            button7.Size = new Size(153, 65);
            button7.TabIndex = 7;
            button7.Text = "FECHAR";
            button7.TextAlignment = StringAlignment.Center;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.circle_green_16_16;
            pictureBox2.Location = new Point(100, 124);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 50);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BorderColor = Color.FromArgb(220, 53, 69);
            button4.Cursor = Cursors.Hand;
            button4.EnteredBorderColor = Color.FromArgb(189, 33, 48);
            button4.EnteredColor = Color.FromArgb(200, 35, 51);
            button4.Font = new Font("Microsoft Sans Serif", 12F);
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.ImageAlign = ContentAlignment.MiddleCenter;
            button4.InactiveColor = Color.FromArgb(220, 53, 69);
            button4.Location = new Point(237, 234);
            button4.Name = "button4";
            button4.PressedBorderColor = Color.FromArgb(178, 31, 45);
            button4.PressedColor = Color.FromArgb(189, 33, 48);
            button4.Size = new Size(43, 39);
            button4.TabIndex = 5;
            button4.TextAlignment = StringAlignment.Center;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.BorderColor = Color.FromArgb(40, 167, 69);
            button3.Cursor = Cursors.Hand;
            button3.EnteredBorderColor = Color.FromArgb(30, 126, 52);
            button3.EnteredColor = Color.FromArgb(33, 136, 56);
            button3.Font = new Font("Microsoft Sans Serif", 12F);
            button3.Image = null;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.InactiveColor = Color.FromArgb(40, 167, 69);
            button3.Location = new Point(132, 321);
            button3.Name = "button3";
            button3.PressedBorderColor = Color.FromArgb(28, 116, 48);
            button3.PressedColor = Color.FromArgb(30, 126, 52);
            button3.Size = new Size(153, 65);
            button3.TabIndex = 4;
            button3.Text = "SALVAR";
            button3.TextAlignment = StringAlignment.Center;
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
            panel2.Size = new Size(784, 80);
            panel2.TabIndex = 0;
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
            panel3.Controls.Add(label3);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(button5);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(521, 8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(14);
            panel3.Size = new Size(255, 64);
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
            button6.Location = new Point(65, 14);
            button6.Name = "button6";
            button6.PressedBorderColor = Color.FromArgb(0, 92, 191);
            button6.PressedColor = Color.FromArgb(0, 98, 204);
            button6.Size = new Size(36, 36);
            button6.TabIndex = 1;
            button6.TextAlignment = StringAlignment.Center;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Right;
            label3.Location = new Point(101, 14);
            label3.Name = "label3";
            label3.Size = new Size(16, 36);
            label3.TabIndex = 7;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BorderColor = Color.FromArgb(108, 117, 125);
            button2.Cursor = Cursors.Hand;
            button2.Dock = DockStyle.Right;
            button2.EnteredBorderColor = Color.FromArgb(84, 91, 98);
            button2.EnteredColor = Color.FromArgb(90, 98, 104);
            button2.Font = new Font("Microsoft Sans Serif", 12F);
            button2.Image = Properties.Resources.minimize_icon_24_24;
            button2.ImageAlign = ContentAlignment.MiddleCenter;
            button2.InactiveColor = Color.FromArgb(108, 117, 125);
            button2.Location = new Point(117, 14);
            button2.Name = "button2";
            button2.PressedBorderColor = Color.FromArgb(78, 85, 91);
            button2.PressedColor = Color.FromArgb(84, 91, 98);
            button2.Size = new Size(36, 36);
            button2.TabIndex = 6;
            button2.TextAlignment = StringAlignment.Center;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Right;
            label2.Location = new Point(153, 14);
            label2.Name = "label2";
            label2.Size = new Size(8, 36);
            label2.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BorderColor = Color.FromArgb(108, 117, 125);
            button1.Cursor = Cursors.Hand;
            button1.Dock = DockStyle.Right;
            button1.EnteredBorderColor = Color.FromArgb(84, 91, 98);
            button1.EnteredColor = Color.FromArgb(90, 98, 104);
            button1.Font = new Font("Microsoft Sans Serif", 12F);
            button1.Image = Properties.Resources.maximize_icon_24_24;
            button1.ImageAlign = ContentAlignment.MiddleCenter;
            button1.InactiveColor = Color.FromArgb(108, 117, 125);
            button1.Location = new Point(161, 14);
            button1.Name = "button1";
            button1.PressedBorderColor = Color.FromArgb(78, 85, 91);
            button1.PressedColor = Color.FromArgb(84, 91, 98);
            button1.Size = new Size(36, 36);
            button1.TabIndex = 4;
            button1.TextAlignment = StringAlignment.Center;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Right;
            label1.Location = new Point(197, 14);
            label1.Name = "label1";
            label1.Size = new Size(8, 36);
            label1.TabIndex = 3;
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.BorderColor = Color.FromArgb(108, 117, 125);
            button5.Cursor = Cursors.Hand;
            button5.Dock = DockStyle.Right;
            button5.EnteredBorderColor = Color.FromArgb(84, 91, 98);
            button5.EnteredColor = Color.FromArgb(90, 98, 104);
            button5.Font = new Font("Microsoft Sans Serif", 12F);
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.ImageAlign = ContentAlignment.MiddleCenter;
            button5.InactiveColor = Color.FromArgb(108, 117, 125);
            button5.Location = new Point(205, 14);
            button5.Name = "button5";
            button5.PressedBorderColor = Color.FromArgb(78, 85, 91);
            button5.PressedColor = Color.FromArgb(84, 91, 98);
            button5.Size = new Size(36, 36);
            button5.TabIndex = 2;
            button5.TextAlignment = StringAlignment.Center;
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
            // FormsBase
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(46, 80, 159);
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormsBase";
            Padding = new Padding(8);
            Text = "FormsBase";
            Load += FormsBase_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pictureBox1;
        private ReaLTaiizor.Controls.Button button5;
        private ReaLTaiizor.Controls.Button button6;
        private Label label3;
        private ReaLTaiizor.Controls.Button button2;
        private Label label2;
        private ReaLTaiizor.Controls.Button button1;
        private Label label1;
        private Label label4;
        private ReaLTaiizor.Controls.Button button4;
        private ReaLTaiizor.Controls.Button button3;
        private PictureBox pictureBox2;
        private ReaLTaiizor.Controls.Button button7;
    }
}