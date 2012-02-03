using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace CloudPaperApp
{
    public class ViviTeachFlashCall
    {
        public void FlashCall(string func, List<string> list) 
        {
           
            switch (func)
            {
                case "CallBackFunc": 
                {
                    Flash2App_CallBackFunc(list);
                    //Console.WriteLine("FlashCall=" + list.Count);
                    break;    
                }
                case "AskSaveDialog":
                {
                    Flash2App_AskSaveDialog(list);
                    break;
                }
                case "SaveFileDialog": 
                {
                    Flash2App_SaveFileDialog(list);
                    break;
                }
                 case "OpenFileDialog":
                {
                    Flash2App_OpenFileDialog(list);

                    break;
                }
                 case "MessageBox.Show":
                {
                    MessageBox.Show(list[0], list[1], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                case "CaptureDesktop":
                {
                    Flash2App_CaptureDesktop(list);
                    break;
                }
                case "Close":
                {
                    Flash2App_Close(list);
                   
                    break;
                }
                case "FlashInit":
                    {
                        CV.ProjectVar.FlashInit = true;

                        StringBuilder sb = new StringBuilder();
                        sb.Append("&TEMP_PATH=" + CV.ProjectVar.TEMP_PATH.Replace("\\", "/").Replace("//", "/"));
                        sb.Append("&APP_PATH=" + CV.ProjectVar.APP_PATH.Replace("\\", "/").Replace("//", "/"));
                        if (CV.ProjectVar.NXB_PATH != null) 
                        {
                            sb.Append("&NXB_PATH=" + CV.ProjectVar.NXB_PATH);
                        }
                        

                        App2Flash("FlashInit", sb.ToString());

                     
                        break;
                    }
                case "FlashReady":
                    {
                        //do nothing
                        break;
                    }
              
                case "WriteLine":
                    {
                        Debug.WriteLine(list[0]);
                        break;
                    }
               
                default:
                    FlashCall2(func, list);
                    break;

            };
        }

        public void FlashCall2(string func, List<string> list)
        {
            switch (func)
            {
                case "Directory.Copy":
                    {
                        Core.DirectoryCopy(list[0], list[1]);
                        break;
                    }
                case "Directory.Move":
                    {
                        Core.DirectoryMove(list[0], list[1]);
                        break;
                    }
                case "File.CopyList":
                    {
                        Core.FileCopyList(list[0], list[1]);
                        break;
                    }
                case "File.Copy":
                    {
                        Core.FileCopy(list[0], list[1]);
                        break;
                    }
                case "File.Move":
                    {
                        Core.FileMove(list[0], list[1]);
                        break;
                    }
                case "File.Delete":
                    {
                        Core.FileDelete(list[0]);
                        break;
                    }
                case "ZIP.ExecZip":
                    {
                        Core.ZIP_ExecZip(list[0], list[1].Equals("1"), list[2]);
                        break;
                    }
                case "ZIP.UnZip":
                    {

                        Core.ZIP_UnZip(list[0], list[1], list[2]);
                        break;
                    }
                case "SFX.ExecZip":
                    {
                        Core.SFX_ExecZip(list[0], list[1]);
                        break;
                    }
                case "OnRecordAudioStart":
                    {
                        Core.OnRecordAudioStart(list[0]);
                        break;
                    }
                case "OnRecordAudioStop":
                    {
                        Core.OnRecordAudioStop();
                        break;
                    }
                case "CommandLine":
                    {
                        Utilities.ProcessStart(list[0], list[1]);
                        break;
                    }
                case "OpenHttp":
                    {
                        frmHttpDialog dlg = new frmHttpDialog();
                        string filename = "";
                        if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                        {
                            filename = dlg.FileName;
                            if (filename.Length == 0) return;

                            if (filename.IndexOf("http://") == -1)
                            {
                                filename = "http://" + filename;
                            }

                            filename = filename.Replace("\\", "/");//.Replace("//", "/");
                        }
                        this.App2Flash("OpenHttp_CallBack", filename);//跟OpenMedia一模一樣

                        break;
                    }
                case "DoLink":
                    {
                        Environment.CurrentDirectory = Application.StartupPath;
                        string filename = list[0];

                        if (filename.IndexOf("http") == 0)
                        {
                            //url
                        }
                        else
                        {
                            
                            filename = Path.GetFullPath(filename);
                        }


                        string ext = Path.GetExtension(filename).ToLower();

                        if (ext.Equals(".nxe") || ext.Equals(".nxp"))
                        {
                            if (Utilities.InstallNXTudio() == false)
                            {
                                this.App2Flash("Show_ContactIRS", "");
                                return;
                            }
                        }

                        //MessageBox.Show(filename);
                        //if (File.Exists(filename)) {
                        Utilities.ProcessStart(filename, "");
                        //}
                        //Debug.Trace(list[0]);
                        break;
                    }
            
                case "BackgroundImage":
                    {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "Image Files (*.jpg, *.bmp, *.gif, *.png)|*.jpg; *.bmp; *.gif; *.png";
                        dlg.Title = "選取一張圖片";
                        string filename = "";
                        if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                        {
                            filename = dlg.FileName; // 選擇的完整路徑
                            filename = filename.Replace("\\", "/").Replace("//", "/");
                        }

                        this.App2Flash("BackgroundImage_CallBack", filename);


                        //MessageBox.Show(list[0].ToString());
                        //Debug.Trace(list[0]);
                        break;
                    }

                case "BackgroundFlash":
                    {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "Flash Files (*.swf)|*.swf";
                        dlg.Title = "選取檔案";
                        string filename = "";
                        if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                        {
                            filename = dlg.FileName; // 選擇的完整路徑
                            filename = filename.Replace("\\", "/").Replace("//", "/");
                        }

                        this.App2Flash("BackgroundFlash_CallBack", filename);


                        //MessageBox.Show(list[0].ToString());
                        //Debug.Trace(list[0]);
                        break;
                    }
                case "SerializeToXml":
                    {
                        //Debug.Trace("=====================");
                        //Debug.Trace(list[0]);
                        //Debug.Trace(list[1]);
                        //string path = CV.ProjectVar.TempPath + "\\"+ list[0] + ".xml";
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(list[1]);
                        SerializeToXml(list[0], doc);

                        break;
                    }
                case "SaveXmlToFile":
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(list[1]);
                        SaveXmlToFile(list[0], doc);

                        break;
                    }
                case "SaveImageToFile":
                    {
                        SaveImageToFile(list[0], list[1]);
                        break;
                    }
                case "SaveScreenShotToFile":
                    {
                        Core.SaveScreenShotToFile(list[0], list[1], list[2]);
                        break;
                    }
                case "ExportToImage":
                    {
                        ExportToImage(list[0]);
                        break;
                    }
                case "ImportImage":
                    {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "Image Files (*.jpg, *.bmp, *.gif, *.png)|*.jpg; *.bmp; *.gif; *.png";
                        dlg.Title = "選取一張圖片";
                        string filename = "";
                        if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                        {
                            filename = dlg.FileName; // 選擇的完整路徑
                            filename = filename.Replace("\\", "/").Replace("//", "/");
                        }

                        this.App2Flash("ImportImage_CallBack", filename);


                        //MessageBox.Show(list[0].ToString());
                        //Debug.Trace(list[0]);
                        break;
                    }
                case "ImportFlash":
                    {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "Flash Files (*.swf, *.flv)|*.swf; *.flv";
                        dlg.Title = "選取檔案";
                        string filename = "";
                        if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                        {
                            filename = dlg.FileName; // 選擇的完整路徑
                            filename = filename.Replace("\\", "/").Replace("//", "/");
                        }

                        this.App2Flash("ImportFlash_CallBack", filename);
                        break;
                    }
                case "ImportVideo":
                    {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "Video Files (*.mpg, *.avi, *.mp4, *.wmv, *.rmvb)|*.mpg; *.avi;*.mp4; *.wmv; *.rmvb;";
                        dlg.Title = "選取檔案";
                        string filename = "";
                        if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                        {
                            filename = dlg.FileName; // 選擇的完整路徑
                            filename = filename.Replace("\\", "/").Replace("//", "/");
                        }

                        this.App2Flash("ImportVideo_CallBack", filename);
                        break;
                    }

            };
        }

        private void SaveXmlToFile(string path, XmlDocument xmlDoc)
        {
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //xmlDoc.AppendChild(xmlDeclaration); 

            path = path.Replace("/", "\\");
            path = Path.GetFullPath(path);
            string dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir) == false)
            {
                Utilities.CheckDeleteDirectory(dir, true);
            }
            //Utilities.CancelReadonly(path);
            xmlDoc.Save(path);



            using (TextWriter sw = new StreamWriter(path, false, Encoding.UTF8)) //Set encoding
            {
                //doc.Save(sw); 
                xmlDoc.Save(sw);
            }
        }

        private void SerializeToXml(string path, XmlDocument xmlDoc)
        {

            path = path.Replace("/", "\\");
            path = Path.GetFullPath(path);
            string dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir) == false)
            {
                Utilities.CheckDeleteDirectory(dir, true);
            }


            path = path.Replace("/", "\\");

            //Utilities.CancelReadonly(path);
            xmlDoc.Save(path);
        }

        private void SaveImageToFile(string filename, string base64string)
        {
            string dir = Path.GetDirectoryName(filename);
            if (Directory.Exists(dir) == false)
            {
                Utilities.CheckDeleteDirectory(dir, true);

            }

            if (filename == null || filename.Length <= 0) return;

            try
            {
                byte[] b = Convert.FromBase64String(base64string);
                MemoryStream ms = new MemoryStream(b);
                Bitmap bitmap = new Bitmap(ms);
                bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
            }
            catch
            {

            }
        }

        private void ExportToImage(string base64string)
        {

            string filename = null;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image File (*.png)|*.png";
            dlg.FileName = Utilities.NewGuid() + ".png";

            // Restores the selected directory, next time
            dlg.RestoreDirectory = true;

            // Startup directory
            dlg.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            // Show the dialog and process the result
            if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                filename = dlg.FileName;


            if (filename == null || filename.Length <= 0) return;

            try
            {
                byte[] b = Convert.FromBase64String(base64string);
                MemoryStream ms = new MemoryStream(b);
                Bitmap bitmap = new Bitmap(ms);
                bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
            }
            catch
            {

            }
        }

        private delegate void CallBackFuncDelegate(string callback);//C++ Method Return ProtoType

        private Dictionary<string, CallBackFuncDelegate> CallBackFuncList = new Dictionary<string, CallBackFuncDelegate>();
        public void App2Flash_SaveAndClose(List<string> param) 
        {
            /*
            string id = Utilities.NewGuid();

            if (CallBackFuncList.ContainsKey(id) == false) 
            {
                CallBackFuncList.Add(id, null);
            }

            if (param != null)
            {
                CV.TempVar.SetDetail("0", param[0]);
                CV.s_frmCloudPaper.ForceClose = Boolean.Parse((string)CV.TempVar.GetVar("0", "ForceClose"));
            }

            CallBackFuncList[id] = new CallBackFuncDelegate(SaveAndClose_IsSaveDirty);
          

            App2Flash("IsSaveDirty", "CallFuncID=" + id);
              */

            App2Flash("SaveAndClose", "");
        }

        private void SaveAndClose_IsSaveDirty(string result) 
        {
            /*
            bool dirty = Boolean.Parse(result);
            MessageBox.Show("SaveAndClose_IsSaveDirty=" + dirty);
            if (dirty == false)
            {
                CV.s_frmCloudPaper.ForceClose = true;//強制關閉,不再詢問
                CV.s_frmCloudPaper.Close();

                return;
            }


            App2Flash("SaveProjectAndClose", "");
           */
        }


        public void Flash2App_OpenFileDialog(List<string> param)
        {

            string filename = null;
            string id = param[3];//CallFuncID

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = param[0];
            dlg.FileName = param[1];
            dlg.Filter = param[2];


            // Restores the selected directory, next time
            dlg.RestoreDirectory = true;

            // Startup directory
            //dlg.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            // Show the dialog and process the result
            if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                filename = dlg.FileName;


            if (filename == null || filename.Length <= 0) return;

            filename = filename.Replace("\\", "/").Replace("//", "/");
           // this.FlashApp.App2Flash("OpenFileDialog_CallBack", filename);

            string abc = "Result=" + filename + "&CallFuncID=" + id;
            App2Flash("CallBackFunc", abc);

        }


        private void Flash2App_SaveFileDialog(List<string> param) 
        {
            string filename = null;

            string title = param[0];
            string savename = param[1];
            string filter = param[2];
            //MessageBox.Show(param[3]);
            //MessageBox.Show(param[4]);
            bool overwrite = Boolean.Parse(param[3]);
            string id = param[4];//CallFuncID
            

            string initdir = Path.GetFullPath(param[1]);
            if (File.Exists(initdir))
            {
                initdir = Path.GetDirectoryName(initdir);
            }
            else { 
               initdir = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }

            if (overwrite == false)
            {
                int loop=1;
                while (++loop<100)
                {
                    string path = initdir + "\\" + Path.GetFileName(savename);
                    bool yn = File.Exists(path + ".vtk");

                    //MessageBox.Show("path=" + path + "(" + yn + ")");
                    if (yn == false)
                    {
                        savename = path;
                        break;
                    }

                    if (loop < 10)
                    {
                        savename = "Untitled0" + loop;
                    }
                    else {
                        savename = "Untitled" + loop;
                    }
                }
            }


            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = title;
            dlg.FileName = Path.GetFileName(savename);
            dlg.Filter = filter;

           
            // Restores the selected directory, next time
            dlg.RestoreDirectory = true;

            // Startup directory
            dlg.InitialDirectory = initdir;
            // Show the dialog and process the result
            if (dlg.ShowDialog(CV.s_frmCloudPaper) == DialogResult.OK)
                filename = dlg.FileName;


            if (filename == null || filename.Length <= 0) return;

            filename = filename.Replace("\\", "/").Replace("//", "/");


            string abc = "Result=" + filename + "&CallFuncID=" + id;
            App2Flash("CallBackFunc", abc);
        }

        private void Flash2App_AskSaveDialog(List<string> param) 
        {
            
            string Result = "";

            string msg = param[0];//CallFuncID
            string title = param[1];//CallFuncID
            string id = param[2];//CallFuncID

            DialogResult dlg = MessageBox.Show(CV.s_frmCloudPaper, msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dlg == DialogResult.Cancel)
            {
                Result = "Cancel";
            }
            else if (dlg == DialogResult.No)
            {
                Result = "No";
            }
            else if ( dlg == DialogResult.Yes)
            {
                Result = "Yes";
            }

            string abc = "Result=" + Result + "&CallFuncID=" + id;
            //MessageBox.Show(abc);
            App2Flash("CallBackFunc", abc);

        }

        private void Flash2App_CallBackFunc(List<string> list) 
        {

            string[] param = list[0].Split('&');

            string Result = "";

            foreach (string args in param) 
            {
                //MessageBox.Show(args);
                string[] kv = args.Split('=');
                if (kv.Length != 2) continue;

                string k = kv[0];
                string v = kv[1];

                if (k.Equals("CallFuncID"))
                {
                    CallBackFuncList[v](Result);
                }
                else {
                    Result = v;
                }

            }
        }

        private void Flash2App_CaptureDesktop(List<string> param) 
        {
            if (CV.s_frmCloudPaper.InvokeRequired)
            {
                CV.s_frmCloudPaper.Invoke(new MyAction1<List<string>>(Flash2App_CaptureDesktop), param);
                return;
            }


            CV.s_frmCloudPaper.Opacity = 0.98;
            
            
            Thread thread = new Thread(new ThreadStart(delegate()
            {
                Thread.Sleep(100);
                Flash2App_CaptureDesktopThread(param);
            }
            ));
            thread.Start();

          
          
        }
        private void Flash2App_CaptureDesktopThread(List<string> param) 
        {
            if (CV.s_frmCloudPaper.InvokeRequired)
            {
                CV.s_frmCloudPaper.Invoke(new MyAction1<List<string>>(Flash2App_CaptureDesktopThread), param);
                return;
            }


            string Result = CV.ProjectVar.TEMP_PATH + "\\capture" + Environment.TickCount + ".png";
            string id = param[0];//CallFuncID


            Bitmap bitmap = Utilities.CaptureWindow(false);
            bitmap.Save(Result, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();

            CV.s_frmCloudPaper.Opacity = 1;

            string abc = "Result=" + Result + "&CallFuncID=" + id;
            //MessageBox.Show(abc);
            App2Flash("CallBackFunc", abc);
        }


        private void Flash2App_Close(List<string> param) 
        {
            if (CV.s_frmCloudPaper.InvokeRequired) 
            {
                CV.s_frmCloudPaper.Invoke(new MyAction1<List<string>>(Flash2App_Close),param);
                return;
            }

            if (param != null)
            {
                CV.TempVar.SetDetail("0", param[0]);
                CV.s_frmCloudPaper.ForceClose = Boolean.Parse((string)CV.TempVar.GetVar("0", "ForceClose"));
            }

            CV.s_frmCloudPaper.Close();
        }

        public void Flash2App_OpenNXB(string filename) 
        {
            if (CV.s_frmCloudPaper.InvokeRequired) 
            {
                CV.s_frmCloudPaper.Invoke(new MyAction1<string>(Flash2App_OpenNXB), filename);
                return;
            }

            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(filename);
            string base64 = Convert.ToBase64String(toEncodeAsBytes);
            base64 = base64.Replace("=", "*");

            App2Flash("OpenNXB", "Result=" + base64);
        }
       


        private StringBuilder SyncCommandString = null;
        public void App2Flash(string cmd, string args)
        {

            if (SyncCommandString == null)
            {
                SyncCommandString = new StringBuilder();
                SyncCommandString.Append("<invoke name=\"App2Flash\" returntype=\"xml\">");
                SyncCommandString.Append("<arguments>");
                SyncCommandString.Append("<string>$cmd</string>");
                SyncCommandString.Append("<string>$args</string>");
                SyncCommandString.Append("</arguments>");
                SyncCommandString.Append("</invoke>");
            }

            //Console.WriteLine(SyncCommandString.ToString());

            string cfunc = SyncCommandString.ToString();
            cfunc = cfunc.Replace("$cmd", cmd);
            cfunc = cfunc.Replace("$args", args);
            //Console.WriteLine(cfunc);
            Debug.Trace(cfunc);

            CV.s_frmCloudPaper.FlashMethod_CallFunction(cfunc);


            //this.FlashMethod_CallFunction(cfunc);
        }
        

    }
}
