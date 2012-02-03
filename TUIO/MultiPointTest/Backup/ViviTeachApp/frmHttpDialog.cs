using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudPaperApp
{
    public partial class frmHttpDialog : Form
    {
        public frmHttpDialog()
        {
            InitializeComponent();

            SetLanguage();
        }

        private void SetLanguage()
        {
            /*
            CV.LanguageVar.SetLanguage(System.Threading.Thread.CurrentThread);

            this.Text = CV.LanguageVar.LanguageManager.GetString("frmHttpSetting.Text");
            this.lbl_title1.Text = CV.LanguageVar.LanguageManager.GetString("frmHttpSetting.lbl_title1");
            this.lbl_title2.Text = CV.LanguageVar.LanguageManager.GetString("frmHttpSetting.lbl_title2");
            this.btn_ok.Text = CV.LanguageVar.LanguageManager.GetString("frmHttpSetting.btn_ok");
            this.btn_cancle.Text = CV.LanguageVar.LanguageManager.GetString("frmHttpSetting.btn_cancle");
             * */
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            string tag = null;

            if (sender is Button) tag = (sender as Button).Tag.ToString();

            if (tag == null) return;

            if (tag.Equals("OK")) {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (tag.Equals("Cancle"))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        public string FileName
        {
            get {
                return this.textBox1.Text;
            }
        }

    }
}