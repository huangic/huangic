using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Checksums;


namespace CloudPaperApp
{
    public class CZip
    {
        ArrayList m_filelist = new ArrayList();
        private string m_savepath = "./test.nxp";
        private string m_zipfile = "";
        private string m_password;
        public event ZipEventHandler ZipCompleteEvent;
        public event ZipEventHandler ZipCompressionFinished;
        public event ZipEventHandler ZipCompressionStarted;
        public event ZipEventHandler ZipCompressing;
        public delegate void ZipEventHandler(object sender, ZipEventArgs e);

        private void OnComplete(List<string> list)
        {
            if (this.ZipCompleteEvent != null)
                this.ZipCompleteEvent(this, new ZipEventArgs(list));
        }

        private void OnCompressionStarted(long totalFileSize, long currentFileSize, string currentFileName)
        {
            if (this.ZipCompressionStarted != null)
                this.ZipCompressionStarted(this, new ZipEventArgs(totalFileSize, currentFileSize, currentFileName));
        }

        private void OnCompressing(long totalFileSize, long currentFileSize, string currentFileName)
        {
            if (this.ZipCompressing != null)
                this.ZipCompressing(this, new ZipEventArgs(totalFileSize, currentFileSize, currentFileName));
        }

        private void OnCompressionFinished(long totalFileSize, long currentFileSize, string currentFileName)
        {
            if (this.ZipCompressionFinished != null)
                this.ZipCompressionFinished(this, new ZipEventArgs(totalFileSize, currentFileSize, currentFileName));
        }



        public ArrayList FileList
        {
            get
            {
                return m_filelist;
            }
            set
            {
                m_filelist = value;
            }
        }

        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
            }
        }

        public string OutputPath
        {
            get
            {
                return m_savepath;
            }
            set
            {
                m_savepath = value;
            }
        }

        public string ZipFilePath
        {
            get
            {
                return m_zipfile;
            }
            set
            {
                m_zipfile = value;
            }
        }

        public void AddFile(string filepath, string aliasname, string parent)
        {
            string[] list = new string[] { filepath, aliasname, parent };
            this.FileList.Add(list);
        }

        public void ExecZip()
        {
            try
            {
                bool res;
                ZipOutputStream s = new ZipOutputStream(File.Create(this.OutputPath));
                s.SetLevel(0);
                long totalFileSize = this.FileList.Count;
                long currentFileSize = 0;

                for (int i = 0; i < this.FileList.Count; i++)
                {
                    
                    string[] data = (string[])this.FileList[i];
                    string realfile = data[0];
                    string aliasname = data[1];
                    string parent = data[2];
                    
                    currentFileSize++;
                    OnCompressionStarted(totalFileSize, currentFileSize, realfile);

                    if (aliasname == null || aliasname.Length == 0)
                    {
                        aliasname = Path.GetFileName(realfile);
                    }

                    if (Directory.Exists(realfile))
                    {
                        //¬O¥Ø¿ý
                        //System.Windows.Forms.MessageBox.Show("yes=" + file);
                        res = ZipFileDictory(realfile, s, parent);
                    }
                    else
                    {
                        //¬OÀÉ®×
                        //System.Windows.Forms.MessageBox.Show("no=" + file);
                        res = ZipFile(realfile, aliasname, s, parent);
                    }

                    OnCompressionFinished(totalFileSize, currentFileSize, realfile);
                }
                OnComplete(null);
                s.Finish();
                s.Close();
            }
            catch (Exception ee)
            {
                Utilities.SaveErrorLog("ExecZip", ee.Message);
                //CV.ProjectVar.IsSaveError = true;
                //System.Windows.Forms.MessageBox.Show("fuck="+ee.ToString());
            }
        }

        public void ExecUnZip(object status)
        {
            ExecUnZip();
        }

        public void ExecTest() 
        {
            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            FileStream streamWriter = null;


            FileInfo fileinfo = new FileInfo(this.ZipFilePath);
            //totalFileSize = fileinfo.Length;

            try
            {
                FileStream stream = new FileStream(this.ZipFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                ZipOutputStream s2 = new ZipOutputStream(stream);
                s2.Finish();
                s2.Close();

            }
            catch(Exception ee)
            {
                Console.WriteLine("shit="+ee.ToString());
            
            }
            finally
            {


                try
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Close();
                        streamWriter = null;
                    }
                }
                catch
                {

                }




                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }


        public void ExecUnZip()
        {
            int t1 = Environment.TickCount;

            List<string> errFile = new List<string>();
        

            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            FileStream streamWriter = null;


            FileInfo fileinfo = new FileInfo(this.ZipFilePath);
          


            try
            {
                s = new ZipInputStream(File.OpenRead(this.ZipFilePath));

                if (this.Password != null || this.Password.Length != 0)
                    s.Password = this.Password;

                while ((theEntry = s.GetNextEntry()) != null)
                {

                    if (theEntry.Name != String.Empty)
                    {
                        long totalFileSize = 0;
                        long currentFileSize = 0;
                      

                        if (!Directory.Exists(this.OutputPath))
                        {
                            Directory.CreateDirectory(this.OutputPath);
                        }

                        fileName = Path.Combine(this.OutputPath, theEntry.Name);
                        ///§PŠÊ¤å¥ó¸ô’¦¬O§_¬O¤å¥óƒH
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        OnCompressionStarted(0, 0, fileName);


                        try
                        {

                            streamWriter = File.Create(fileName);
                            //streamWriter = new FileStream(fileName, FileMode.Create);

                            int size = 10485760;//1mb
                            //int size = 1024;
                            byte[] data = null;
                            totalFileSize = theEntry.Size;
                            //Console.WriteLine("totalfilesize=" +totalFileSize);

                            if (totalFileSize > 10485760)
                            {
                                size = 10485760;//10mb
                                //size = 4096 * 2;//8k
                                data = new byte[size];
                            }
                            else {
                                //size = 4096;
                                size = 1048576;
                                data = new byte[size];
                            }

                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                    

                                    currentFileSize += size;
                                    OnCompressing(totalFileSize, currentFileSize, fileName);

                                }
                                else
                                {
                                    break;
                                }
                            }
                            streamWriter.Close();



                        }
                        catch
                        {
                            //System.Windows.Forms.MessageBox.Show("error");
                            errFile.Add(fileName);
                        }

                        OnCompressionFinished(0, 0, fileName);

                    }
                }//end while


            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("ex=" + ee.ToString());
            }
            finally
            {


                try
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Close();
                        streamWriter = null;
                    }
                }
                catch
                {

                }




                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            int t2 = Environment.TickCount;
            Console.WriteLine("t2=" + (t2 - t1));


            OnComplete(errFile);
        }

        #region ÀËµøÀÉ®×

        public List<string> ViewFileBySubName(string sub)
        {
            List<string> filelist = new List<string>();

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(this.ZipFilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsFile)
                    {
                        string[] subname = theEntry.Name.Split(new char[] { '.' });
                        if (subname.Length > 1 && subname[subname.Length - 1].ToLower().Equals(sub))
                        {
                            byte[] data = new byte[4096];
                            int size = s.Read(data, 0, data.Length);
                            string filecontent = "";
                            while (size > 0)
                            {
                                filecontent += Encoding.Default.GetString(data, 0, size);
                                size = s.Read(data, 0, data.Length);
                            }
                            filelist.Add(filecontent);
                        }
                    }
                }

                s.Close();
            }


            return filelist;
        }

        public List<string> ViewFileToDiskBySubName(string sub)
        {
            List<string> filelist = new List<string>();
            string returnFileName = "";
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(this.ZipFilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsFile)
                    {
                        string[] subname = theEntry.Name.Split(new char[] { '.' });
                        if (subname.Length > 1 && subname[subname.Length - 1].ToLower().Equals(sub))
                        {
                            returnFileName = System.IO.Path.GetTempPath() + theEntry.Name;

                            using (FileStream streamWriter = File.Create(returnFileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            filelist.Add(returnFileName);
                            /*
                            byte[] data = new byte[4096];
                            int size = s.Read(data, 0, data.Length);
                            string filecontent = "";
                            while (size > 0)
                            {
                                filecontent += Encoding.Default.GetString(data, 0, size);
                                size = s.Read(data, 0, data.Length);
                            }
                            filelist.Add(filecontent);
                            */
                        }
                    }
                }

                s.Close();
            }


            return filelist;
        }


        public string ViewFile(string filename)
        {
            byte[] data = new byte[4096];
            string filecontent = "";
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(this.ZipFilePath)))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    if (theEntry.IsFile && theEntry.Name.Equals(filename))
                    {

                        int size = s.Read(data, 0, data.Length);
                        while (size > 0)
                        {
                            filecontent += Encoding.Default.GetString(data, 0, size);
                            size = s.Read(data, 0, data.Length);
                        }


                        break;
                    }
                }

                s.Close();
            }

            return filecontent;
        }

        public string ViewFileToDisk(string filename)
        {
            string returnFileName = "";
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(this.ZipFilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsFile && theEntry.Name.Equals(filename))
                    {
                        returnFileName = System.IO.Path.GetTempPath() + theEntry.Name;
                        using (FileStream streamWriter = File.Create(returnFileName))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    }
                }

                s.Close();
            }
            return returnFileName;
        }

        #endregion

        #region À£ÁY
        private  bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {

                //„Ç«Ø…å«e¤å¥óƒH
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));  //¥[¤W ¡§/¡¨ ¤~…Ñ…å¦¨¬O¤å¥óƒH„Ç«Ø
                s.PutNextEntry(entry);
                s.Flush();


                //¥ý‰ÍŠD¤å¥ó¡A¦A‡cŠÐ‰ÍŠD¤å¥óƒH 
                filenames = Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    //¥´…{‰ÍŠD¤å¥ó
                    fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(file)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("ee1=" + ee.ToString());
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }


            folders = Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }

            return res;
        }

        private  bool ZipFile(string RealFile,string AliasName, ZipOutputStream s, string ParentFolderName)
        {

            FileStream ZipFile = null;
            ZipEntry ZipEntry = null;
            bool res = true;
            try
            {

                if (ParentFolderName.Length != 0)
                {
                    //„Ç«Ø…å«e¤å¥óƒH
                    ZipEntry = new ZipEntry(ParentFolderName + "/");  //¥[¤W ¡§/¡¨ ¤~…Ñ…å¦¨¬O¤å¥óƒH„Ç«Ø
                    s.PutNextEntry(ZipEntry);
                    s.Flush();

                    ZipEntry = new ZipEntry(ParentFolderName + "/" + AliasName);
                    s.PutNextEntry(ZipEntry);
                }
                else
                {
                    ZipEntry = new ZipEntry(AliasName);
                    s.PutNextEntry(ZipEntry);
                }


                ZipFile = File.OpenRead(RealFile);


                byte[] buffer = null;

                const int MaxFileSize = 1024*1024*10;//10MB
                if (ZipFile.Length > MaxFileSize)
                {
                    //Console.WriteLine("filesize=" + ZipFile.Length);
                    //³æÀÉ¶W¹L10MB
                    buffer = new byte[MaxFileSize];
                    int bytesRead=0;
                    long bytesSum=0;
                    long bytesCount = ZipFile.Length;
                    do
                    {
                        bytesRead = ZipFile.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, bytesRead);
                        bytesSum += bytesRead;
                        this.OnCompressing(bytesCount, bytesSum, RealFile);
                    } while (bytesRead > 0);
                    ZipFile.Close();
                }
                else
                {
                    buffer =new byte[ZipFile.Length];
                    ZipFile.Read(buffer, 0, buffer.Length);
                    ZipFile.Close();
                    s.Write(buffer, 0, buffer.Length);
                }


            



                
            }
            catch(Exception ee)
            {
                Console.WriteLine("ee2=" + ee.ToString());
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }

                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            return res;
        }



        #endregion


    }


    public class ZipEventArgs : EventArgs
    {
        private List<string> m_List;
        private int percentDone;
        private string downloadState;
        private long totalFileSize;
        private long currentFileSize;
        private string filename;
        private Exception ex;

        public long TotalFileSize
        {
            get { return totalFileSize; }
            set { totalFileSize = value; }
        }


        public long CurrentFileSize
        {
            get { return currentFileSize; }
            set { currentFileSize = value; }
        }

        public int PercentDone
        {
            get { return percentDone; }
            set { percentDone = value; }
        }


        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }

        public ZipEventArgs(long totalFileSize, long currentFileSize,string filename)
        {
            this.totalFileSize = totalFileSize;
            this.currentFileSize = currentFileSize;
            this.FileName = filename;

            this.percentDone = (int)((((double)currentFileSize) / totalFileSize) * 100);
        }


        public ZipEventArgs(List<string> list)
        {
            this.m_List = list;
        }

        public List<string> ErrorFileList
        {
            get
            {
                return this.m_List;
            }
        }


    }



}
