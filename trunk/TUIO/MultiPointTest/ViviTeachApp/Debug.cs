using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudPaperApp
{
    public partial class Debug : Form
    {
        public static Debug Instance = null;
        public static string Password = "";
        public Debug()
        {
            Instance = this;
            InitializeComponent();

            this.txtPassword.Text = Password;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
        }

        public static void Error(Exception e,string msg) 
        {
            MessageBox.Show(msg+Environment.NewLine + Environment.NewLine + e.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //flash trace
        public static void WriteLine(string msg) 
        {
            if (Debug.Instance == null) return;

            Debug.Instance.txtTrace.AppendText(msg + Environment.NewLine);
        
        }

        
        public static void WriteLine2(string msg)
        {
            if (Debug.Instance == null)
            {
                return;
            }

            Debug.Instance.textBox2.AppendText(msg + Environment.NewLine);
        }
        
        
        //flash call c#
        public static void Trace(string msg) 
        {
            if (Debug.Instance == null) return;

            if (Debug.Instance.chkLowMsg.Checked)
            {
                Debug.Instance.textBox1.AppendText(msg + Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmHttpDialog dlg = new frmHttpDialog();
            dlg.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CV.s_frmCloudPaper.FlashApp.App2Flash("ScreenShot", "");
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            this.txtTrace.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.txtTrace.Text);
        }
    }
}