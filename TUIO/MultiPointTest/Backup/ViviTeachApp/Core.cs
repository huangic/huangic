using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CloudPaperApp
{
    public delegate bool MyActionX();
    public delegate void MyAction0();
    public delegate void MyAction1<T1>(T1 t1);
    public delegate void MyAction2<T1, T2>(T1 t1, T2 t2);
    public delegate object MyActionX1<T1>(T1 t1);
    public delegate object MyActionX2<T1, T2>(T1 t1, T2 t2);

    public class Core
    {

        public static void DirectoryCopy(string src, string dest_dir)
        {
            Debug.WriteLine2("DirectoryCopy=" + src + "," + dest_dir);
            if (Directory.Exists(src)) 
            {
                if (Directory.Exists(dest_dir) == false)
                {
                    Directory.CreateDirectory(dest_dir);
                }
                Utilities.copyDirectory(src, dest_dir);
                Utilities.CancelReadonly(dest_dir);
                
            }
        }

        public static void DirectoryMove(string src_dir, string dest_dir)
        {
            Debug.WriteLine2("DirectoryMove=" + src_dir + "," + dest_dir);


            /*
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(m_outputfilename2, savename, true);
                move = true;
            }
            catch (Exception ee)
            {
                //continue;
                MessageBox.Show(ee.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            
            try
            {
                if (Directory.Exists(dest_dir))
                {
                    Directory.Delete(dest_dir, true);
                }
            }
            catch(Exception ee)
            {
                Debug.WriteLine2("DirectoryMove1=" + ee.ToString());
                //System.Windows.Forms.MessageBox.Show("DirectoryMove1=" + ee.ToString());
            }

            try
            {
                Utilities.MoveDirectory(src_dir, dest_dir);
            }
            catch (Exception ee)
            {
                Debug.WriteLine2("DirectoryMove2=" + ee.ToString());
                //System.Windows.Forms.MessageBox.Show("DirectoryMove2=" + ee.ToString());
            }

          

            /*
      try
      {
          //Directory.Move(src_dir, dest_dir);
          Utilities.MoveDirectory(src_dir, dest_dir);
      }
      catch (Exception ee)
      {
          Debug.WriteLine2("DirectoryMove=" + src_dir + "," + dest_dir + "-->"+ee.ToString());
          //System.Windows.Forms.MessageBox.Show("DirectoryMove2=" + ee.ToString());
      }
      */
        }

        public static void FileMove(string src, string dest)
        {
            Debug.WriteLine2("FileMove=" + src + "," + dest);

            if (File.Exists(src) == false) return;

            try
            {
                File.Move(src, dest);
                File.SetAttributes(dest, FileAttributes.Normal);
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("FileMove=" + ee.ToString());
            }
        }

        public static void FileCopy(string src, string dest)
        {
            if (File.Exists(src) == false) return;

            File.Copy(src, dest, true);
        }

        public static void FileCopyList(string filelist, string dest_dir)
        {

            Debug.WriteLine2("FlleCopy=" + dest_dir);

            if (Directory.Exists(dest_dir) == false)
            {
                Utilities.CheckDeleteDirectory(dest_dir, true);
            }

            //Save Board
            foreach (string file in filelist.Split(','))
            {
                if (file.Length < 2) continue;
                //string newfile = file.Replace("//", "/").Replace("/", "\\");

                try
                {

                    FileInfo file1 = new FileInfo(file);
                    FileInfo file2 = new FileInfo(dest_dir + "\\" + file1.Name);

                    bool copy = false;
                    if (file1.Exists)
                    {
                        copy = true;
                        if (file2.Exists)
                        {
                            copy = file1.Length != file2.Length;
                        }
                    }

                    if (copy)
                    {
                        Debug.WriteLine2("copy to=" + file2.FullName);
                        file1.CopyTo(file2.FullName, true);
                    }
                    else
                    {
                        Debug.WriteLine2("no cope=" + file2.FullName);
                    }
                }catch(Exception ee){
                    System.Windows.Forms.MessageBox.Show("FileCopyList="+ file + Environment.NewLine +ee.ToString() );
                }
            }

         
        }

        public static void FileDelete(string filelist)
        {
            foreach (string file in filelist.Split(','))
            {
                if (file.Length < 2) continue;
                if (File.Exists(file) == true) 
                {
                    try
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                    catch(Exception ee)
                    {
                        Debug.Error(ee, "FileDelete.DeleteFile("+file +")");
                    }
                }
            }
        }
        
        //private static tessnet2.Tesseract ocr = null;
        public static string DoOCR(string base64string) 
        {
            string ocrstr = "";
            /*
            try
            {
                //System.Windows.Forms.MessageBox.Show("do ocr="+base64string);
                if (ocr == null)
                {
                    string filename = System.Windows.Forms.Application.StartupPath + @"\OCRData"; 
                    string defaultCharList = "0123456789";
                    //System.Windows.Forms.MessageBox.Show("a");    
                    ocr = new tessnet2.Tesseract();//声明一个OCR类
                    //System.Windows.Forms.MessageBox.Show("b");    
                    //ocr.SetVariable("tessedit_char_whitelist", defaultCharList);//设置识别变量，当前只能识别数字及英文字符。
                    //System.Windows.Forms.MessageBox.Show("c");    
                    ocr.Init(filename, "eng", true);
                    
                    //System.Windows.Forms.MessageBox.Show("d");    
                    //应用当前语言包。注，Tessnet2是支持多国语的。语言包下载链接：http://code.google.com/p/tesseract-ocr/downloads/list

                }

            }
            catch(Exception ee){
                System.Windows.Forms.MessageBox.Show("ee=" + ee.ToString());
            }
          
            try
            {
                ocr.Clear();
                byte[] b = Convert.FromBase64String(base64string);
                MemoryStream ms = new MemoryStream(b);
                Bitmap bitmap  = new Bitmap(ms);


                Bitmap bp = bitmap.Clone() as Bitmap;//识别图像
                Bitmap bp2 = bitmap.Clone() as Bitmap;

                List<tessnet2.Word> result = ocr.DoOCR(bp, Rectangle.Empty);//执行识别操作
                foreach (tessnet2.Word word in result)//遍历识别结果。
                {
                    ocrstr += word.Text;
                }
            }
            catch
            {
            }

            System.Windows.Forms.MessageBox.Show("ocr=" + ocrstr);
            */

            return ocrstr;
        }

        public static void SFX_ExecZip(string dir,string saveto) 
        {
            string sfx_lib = System.Windows.Forms.Application.StartupPath + "\\SFXLib.dll";
            string sfx_exe= System.Windows.Forms.Application.StartupPath + "\\SFXLoader.exe";
            //System.Windows.Forms.MessageBox.Show(sfx_lib + "==" + File.Exists(sfx_lib));
            //System.Windows.Forms.MessageBox.Show(sfx_exe + "==" +File.Exists(sfx_exe));


            dir = dir.Replace("//", "\\");
            dir = dir.Replace("\\\\", "\\");


            saveto = saveto.Replace("//", "\\");
            saveto = saveto.Replace("\\\\", "\\");



            SFXWrapper sfx = new SFXWrapper(sfx_lib);
            sfx.SetSFXLoader(sfx_exe);
            sfx.SetOutputFile(saveto);
            sfx.AddFile(dir, "", "");
            sfx.OnProgress += new SFXWrapper.ProgressCallBack(delegate(int iProgress)
            {
                Debug.WriteLine2(iProgress+"");
            });

            sfx.DoZip();
            sfx.Dispose();

        }
        
        public static void ZIP_ExecZip(string dir,bool childern,string saveto) 
        {
            CZip zip = new CZip();
            zip.Password = "";

            if (childern == false)
            {
                zip.AddFile(dir, "", "");
            }
            else 
            {
                foreach (string file in Directory.GetFiles(dir)) 
                {
                    zip.AddFile(file, "", "");
                }
                foreach (string subdir in Directory.GetDirectories(dir))
                { 
                    zip.AddFile(subdir, "", "");
                }
            }

            zip.OutputPath = saveto;
            zip.ExecZip();
        }

        public static void ZIP_UnZip(string zipfile,string dir, string func_id)
        {
            Debug.WriteLine2("ZIPUnZip(" + zipfile + "," + dir);

            try
            {
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch(Exception ee){
                Debug.WriteLine2("ZIPUnZip=" + ee.ToString());
            }

            CZip zip = new CZip();
            zip.Password = "";
            zip.ZipFilePath = zipfile;
            zip.OutputPath = dir;
           
            zip.ZipCompleteEvent +=new CZip.ZipEventHandler(delegate(object sender, ZipEventArgs e)
            {
                Debug.WriteLine2("ZipCompleteEvent=" + dir);

                string abc = "Result=" + dir + "&CallFuncID=" + func_id;

                CV.s_frmCloudPaper.FlashApp.App2Flash("CallBackFunc", abc);
            });
            zip.ExecUnZip();
        }

        

        



        public static void OnRecordAudioStart(string filename) 
        {
            AudioRecord.OnRecordStart(filename);
        }
        public static void OnRecordAudioStop()
        {
            AudioRecord.OnRecordStop();
        }



        public static void SaveScreenShotToFile(string filename, string base64string, string quility)
        {
            // Change the current directory.             
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

           // System.Windows.Forms.MessageBox.Show(filename);
            string dir = Path.GetDirectoryName(filename);
            if (Directory.Exists(dir) == false)
            {
                Utilities.CheckDeleteDirectory(dir, true);

            }

            if (filename == null || filename.Length <= 0) return;

            filename = Path.GetFullPath(filename);
            Console.WriteLine(filename);

            try
            {
                byte[] b = Convert.FromBase64String(base64string);
                MemoryStream ms = new MemoryStream(b);
                Bitmap large = new Bitmap(ms);
                Bitmap small = Utilities.ResizeBitmap(large, 144, 108, large.Width, large.Height);

                ImageCodecInfo jpegCodec = Utilities.GetEncoderInfo("image/jpeg");// Jpeg image codec 
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Convert.ToInt32(quility));// Encoder parameter for image quality 
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
               
                
                large.Save(filename, jpegCodec, encoderParams);
                large.Dispose();

                string smallfilename = filename.Replace(".jpg", ".png");
                small.Save(smallfilename,jpegCodec,encoderParams);
                small.Dispose();

            }
            catch
            {

            }
        }



    }
}
