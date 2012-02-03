using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Diagnostics;
namespace CloudPaperApp
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = Application.StartupPath;
         
            CV.ProjectVar = new ProjectVariable();
            CV.RuntimeVar = new RuntimeVariable();

            CV.ProjectVar.APP_PATH = Application.StartupPath;
            CV.ProjectVar.TEMP_PATH = CV.ProjectVar.DATA_PATH + "\\Temp";


            if (args != null && args.Length >=1)
            {
                CV.ProjectVar.NXB_PATH = args[0];
            }


            if (Utilities.IsExistProce())
            {
                if (args != null && args.Length > 0)
                {
                    IntPtr hwnd = API.FindWindow(null, "ViviTek Interactive Whiteboard");
                    if (hwnd.ToInt32() > 0)
                    {
                        Utilities.SendMessageToApplication(hwnd, CV.ProjectVar.NXB_PATH);
                    }
                }
                else
                {
                    MessageBox.Show("ViviTek Interactive Whiteboard has been running!", "ViviTek Workspace", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }

                return;
            }


        
            if(true)
            {
                try
                {

                  
                    Application.Run(new frmViviTeach());

                    //Application.Run(new Form1());
                }
                catch(Exception ee){
                    MessageBox.Show(ee.ToString());
                }

              

            }

        }

        


        private static void SerializeToXml(string path, XmlDocument xmlDoc)
        {
            string typename = null;
            if (typename == null) 
            { 
                XmlNodeList search = xmlDoc.GetElementsByTagName("TypeName");
                if (search.Count > 0)
                {
                    typename = search[0].ChildNodes[0].Value;
                }
            }

            if (typename.Equals("CImage"))
            {
                #region CImage

                XmlNodeList search = xmlDoc.GetElementsByTagName("Base64");
                if (search.Count > 0)
                {
                    string base64string = search[0].ChildNodes[0].Value;

                    byte[] b = Convert.FromBase64String(base64string);
                    MemoryStream ms = new MemoryStream(b);
                    Bitmap bitmap = new Bitmap(ms);
                    string SourceFile = CV.ProjectVar.TEMP_PATH + "\\temp.png";
                    bitmap.Save(SourceFile, System.Drawing.Imaging.ImageFormat.Png);
                    bitmap.Dispose();
                    xmlDoc.FirstChild.RemoveChild(search[0]);

                    XmlElement elem = xmlDoc.CreateElement("SourceFile");
                    elem.InnerText = SourceFile;
                    xmlDoc.FirstChild.AppendChild(elem);
                }

                #endregion

            }

            path = path.Replace("/", "\\");
            xmlDoc.Save(path);
        }
    }
}