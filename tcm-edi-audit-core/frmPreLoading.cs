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
    public partial class frmPreLoading : Form
    {
        public frmPreLoading()
        {
            InitializeComponent();
        }

        private async void frmPreLoading_Load(object sender, EventArgs e)
        {
            //await Task.Delay(1);

            //try
            //{
            //    await Task.Run(() => {
            //        frmHome frmHome = new frmHome();
            //        frmHome.Show();
            //    });
            //    this.Close();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Erro ao iniciar o sistema: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    this.Close();

            //}
        }
    }
}

//this.Invoke((MethodInvoker)(() =>
//{
//    button4.Enabled = false;
//    button4.Text = "Processando...";
//    pcbLoading.Visible = true;
//}));

//await Task.Delay(1);

//try
//{
//    var filesValidated = await Task.Run(() => ValidateFiles(_localSettings.TryFixIt));

//    frmValidatorResult frmValidatorResult = new frmValidatorResult(filesValidated, _localSettings);

//    frmValidatorResult.ShowDialog();
//}
//catch (Exception ex)
//{
//    MessageBox.Show($"Erro: {ex.Message}");
//}
//finally
//{
//    this.Invoke((MethodInvoker)(() =>
//    {
//        button4.Text = "AUDITAR";
//        button4.Enabled = true;
//        pcbLoading.Visible = false;
//    }));
//}