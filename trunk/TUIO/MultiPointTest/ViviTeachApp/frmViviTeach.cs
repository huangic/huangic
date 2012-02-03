using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Xml;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Threading;
using OSC.NET;
using System.Windows.Forms;
using MT;


namespace CloudPaperApp
{
    public partial class frmViviTeach : Form
    {
       

        public frmViviTeach()
        {

            CV.s_frmCloudPaper = this;
            //CV.s_frmCloudPaper.Text = CV.ProjectVar.DiskCompany + " - " + CV.ProjectVar.DiskName;
            CV.s_frmCloudPaper.Text = CV.ProjectVar.DiskName;


        
            InitializeComponent();
            
            FlashInit();

            Utilities.CancelReadonly(CV.ProjectVar.TEMP_PATH);
            Utilities.CheckDeleteDirectory(CV.ProjectVar.TEMP_PATH, true);

            
            
         
            if (CV.ProjectVar.Debug)
            {
                new Debug().Show();
            }


            initCursor();

         
        
        }

      

        private void LoadFlash()
        {

         
            string path = Application.StartupPath + "\\ViviTeach.swf";
            if (File.Exists(path))
            {
                this.axShockwaveFlash1.Movie = path;

             
            }
        }



     


        public ViviTeachFlashCall FlashApp = new ViviTeachFlashCall();

        public void FlashCall(string func, List<string> list)
        {
            FlashApp.FlashCall(func, list);
           
        }


       

   
        
        public bool ForceClose = false;//強制關閉
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LoadFlash();
        }

       
    
     
        private void FlashInit()
        {
            // MessageBox.Show("14", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            // 
            // axShockwaveFlash1
            // 
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.axShockwaveFlash1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axShockwaveFlash1.Enabled = true;
            this.axShockwaveFlash1.Location = new System.Drawing.Point(0, 0);
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            //this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.Size = new System.Drawing.Size(320, 240);
            this.axShockwaveFlash1.TabIndex = 0;
            this.axShockwaveFlash1.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(axShockwaveFlash1_FlashCall);



            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(this.axShockwaveFlash1);
          

            // MessageBox.Show("15", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

       
        public void FlashMethod_CallFunction(string cfunc)
        {

            Environment.CurrentDirectory = Application.StartupPath;

            try
            {
                this.axShockwaveFlash1.CallFunction(cfunc);
            }
            catch(Exception ee)
            {
                MessageBox.Show("錯誤回報:"+ Environment.NewLine+ cfunc,"提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void axShockwaveFlash1_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
            string request = e.request;
            Debug.Trace(request);


            XmlDocument document = new XmlDocument();
            document.LoadXml(request);

            // Get all the arguments
            XmlNodeList args = document.GetElementsByTagName("arguments");

            string find = "<invoke name=\"";
            int indx1 = request.IndexOf(find) + find.Length;
            int indx2 = request.IndexOf("\"", indx1);
            string func = request.Substring(indx1, indx2 - indx1);
            
            List<string> list = new List<string>();
            for (int i = 0; i < args[0].ChildNodes[0].ChildNodes.Count; i++) 
            {
                //Console.WriteLine("args[" + i + "]=" + );
                list.Add(args[0].ChildNodes[0].ChildNodes[i].InnerText);
            }

            try
            {
                FlashCall(func, list);
            }
            catch(Exception ee)
            {
                MessageBox.Show("axShockwaveFlash1_FlashCall="+ee.ToString());
            }
        }


        #region Method


     

      
      

      

     

        #endregion

        #region Propertities

      

        //SYSTEM MESSAGE
        private const int GWL_WNDPROC = -4;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_RESTORE = 0xF120;

        private int HotKeyCode = 0;

        //Delegage Function
        //http://hi.baidu.com/liulei_413/blog/item/26737b340ca63f47241f1446.html
        public delegate IntPtr FlaWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private IntPtr OldWndProc = IntPtr.Zero;
        private FlaWndProc Wpr = null;

       

        //Other
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private Rectangle DefaultBound = Rectangle.Empty;

        #endregion

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            

           // PointF pt = PointToClient(Cursor.Position);

            int XX = e.X; 
            int YY = e.Y;



            this.FlashApp.App2Flash("MouseDown","X="+XX + "&Y="+YY);



            //TUIO Mouse Down;
            Point position = new Point(e.X, e.Y);

            TCursor _newCursor = new TCursor(position, 0, _cursorSessionCounter,this.DeviceId);
            if (!_cursors.ContainsKey(this.DeviceId))
            {
                _cursors.Add(this.DeviceId, _newCursor);
                _cursorSessionCounter++;
            }

            SendStatusUpdate();


        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int XX = e.X;
                int YY = e.Y;
                Console.WriteLine(e.X + "," + e.Y);

                this.FlashApp.App2Flash("MouseMove", "X=" + XX + "&Y=" + YY);
                
                 Point position = new Point(e.X, e.Y);
                //TUIO Move

                 if (_cursors.ContainsKey(this.DeviceId))
                {
                    TCursor c = _cursors[this.DeviceId];
                    c.Position = position;

                    SendStatusUpdate();
                }



            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            int XX = e.X;
            int YY = e.Y;

            this.FlashApp.App2Flash("MouseUp", "X=" + XX + "&Y=" + YY);
           

            //TUIO Remove Cursor

            _cursors.Remove(this.DeviceId);

            SendStatusUpdate();



        }

        private Dictionary<string, TCursor> _cursors;
       
        private int _cursorSessionCounter;
        public int _messageCounter = 0;


        public string DeviceId = "TestMouse";

        private void initCursor(){
            _cursors = new Dictionary<string, TCursor>();
        }

        private void SendStatusUpdate()
        {
            OSCBundle bundle = new OSCBundle();

            OSCMessage message = new OSCMessage("/tuio/2Dcur");
            message.Append("source");
            message.Append("TUIO");



            bundle.Append(message);
            //_oscTransmitter.Send(message);

            message = new OSCMessage("/tuio/2Dcur");
            message.Append("alive");

            foreach (TCursor c in _cursors.Values)
            {
                message.Append(c.SessionID);
            }

            bundle.Append(message);
            //_oscTransmitter.Send(message);

            foreach (TCursor c in _cursors.Values)
            {
                float xPos = c.Position.X;
                float yPos = c.Position.Y;

                message = new OSCMessage("/tuio/2Dcur");
                message.Append("set");
                message.Append(c.SessionID);
                message.Append(xPos);
                message.Append(yPos);
                message.Append(0.0f);
                message.Append(0.0f);
                message.Append(0.0f);
                message.Append(c.DeviceId);

                bundle.Append(message as OSCPacket);
            }

            message = new OSCMessage("/tuio/2Dcur");
            message.Append("fseq");
            message.Append(_messageCounter);
            _messageCounter++;

            bundle.Append(message as OSCPacket);

            //Send Extenal interface;

            this.FlashApp.App2Flash("TUIO",Convert.ToBase64String(bundle.BinaryData));

        }


    }
}