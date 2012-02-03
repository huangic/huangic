using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace CloudPaperApp
{
    public class CV
    {

        public static ProjectVariable ProjectVar = null;
        public static RuntimeVariable RuntimeVar = null;

        public static frmViviTeach s_frmCloudPaper = null;
        public static CloudPlus.ToolsForm CloudPludWindow = null;

        public static TempMemoryVariable TempVar = new TempMemoryVariable();
    }

    public class ProjectVariable 
    {
        public int LimitCount = 1000;//32;
        public bool DemoVersion = true;

        public bool APP_NXBOARD = false;
        public string APP_NXBOARD_PATH = null;

        public bool APP_PREVIEW = false;
        public bool APP_WIZARD = false;
        public bool APP_CloudPlus = true;
        public int JpegQuility = 20;
        

        public bool Debug = false;
        public bool FullScreen = true;

        public string DiskName = "";
        //public string DiskIcon = "";
        public string DiskCompany = "";
        public string DATA_PATH = "";

        public bool FlashInit = false;
        public string APP_PATH = "";
        public string TEMP_PATH ="";
        public string NXB_PATH = "";

        public string Version = "";
        public string Password = "";



      
    }

    public class RuntimeVariable 
    {
       

        public RuntimeVariable()
        {
            LoadConfig();//Åª¤JINIÀÉ
        }

        private void SearchNode(XmlNodeList node,string tag) 
        {
        
        }

        public void LoadConfig()
        {
            string filepath = System.Windows.Forms.Application.StartupPath + "\\Runtime.xml";
            string temp = null;

            //System.Windows.Forms.MessageBox.Show("filepath1=" + filepath);
            if (File.Exists(filepath) == false) return;
           // System.Windows.Forms.MessageBox.Show("filepath2=" + filepath);
            this.RuntimeSetting = new XmlDocument();
            this.RuntimeSetting.Load(filepath);

            XmlNodeList search = null;
           // System.Windows.Forms.MessageBox.Show("ChildNodes=" + this.RuntimeSetting.ChildNodes.Count);
          
            foreach (XmlElement element in this.RuntimeSetting.ChildNodes[1].ChildNodes)
            {
                if (element.Name == "Version") 
                {
                    CV.ProjectVar.Version = element.InnerText;
                }
                else if (element.Name == "Password")
                {
                    CV.ProjectVar.Password = element.InnerText;
                }
                else if (element.Name == "Settings") 
                {
                    search = element.GetElementsByTagName("Debug");
                    if (search.Count > 0)
                    {
                        CV.ProjectVar.Debug = Convert.ToBoolean(search[0].InnerText);
                    }
                    search = element.GetElementsByTagName("Wizard");
                    if (search.Count > 0)
                    {
                        CV.ProjectVar.APP_WIZARD = Convert.ToBoolean(search[0].InnerText);
                    }

                    //System.Windows.Forms.MessageBox.Show(" CV.ProjectVar.Debug =" + CV.ProjectVar.Debug);
                }
                else if (element.Name == "DiskInfo")
                {
                    search = element.GetElementsByTagName("DiskData");
                    if (search.Count > 0)
                    {
                        CV.ProjectVar.DATA_PATH = search[0].InnerText;
                    }

                    search = element.GetElementsByTagName("DiskCompany");
                    if (search.Count > 0)
                    {
                        CV.ProjectVar.DiskCompany = search[0].InnerText;
                    }

                    search = element.GetElementsByTagName("DiskName");
                    if (search.Count > 0)
                    {
                        CV.ProjectVar.DiskName = search[0].InnerText;
                    }
                    
                }
                else if (element.Name == "Options")
                {
                    search = element.GetElementsByTagName("UsingCloudPlus");
                    if (search.Count > 0)
                    {
                        CV.ProjectVar.APP_CloudPlus = Convert.ToBoolean(search[0].InnerText);
                    }
                }
                          
            }

        }

        public XmlDocument RuntimeSetting
        {
            get { return m_RuntimeSetting; }
            set { m_RuntimeSetting = value; }
        }
        private XmlDocument m_RuntimeSetting = null;
    }
}
