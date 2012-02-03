using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace CloudPaperApp
{
    public class SFXWrapper
    {
        #region °ò¥»
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static IntPtr LoadLib(string dll)
        {
            IntPtr instance = LoadLibrary(dll);
            return instance;
        }

        private static Delegate GetAddress(IntPtr instance, string functionname, Type t)
        {
            IntPtr addr = GetProcAddress(instance, functionname);
            if (addr == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(addr, t);
        }

        private static void FreeLib(IntPtr instance)
        {
            FreeLibrary(instance);
        }
        #endregion


        private IntPtr instance = IntPtr.Zero;
       
        public SFXWrapper(string dll)
        {
            instance = LoadLib(dll);
        }

        public void Dispose()
        {
            try
            {
                FreeLib(instance);
            }
            catch { }
        }



        public delegate void ProgressCallBack(int iProgress);
        //public ProgressCallBack ProgressHandler = null;
        public event ProgressCallBack OnProgress;

        private delegate void MyAction1(string path);
        private delegate void MyAction3(string path,string alias,string parent);
        
        private delegate void DoWorkDelegate(ProgressCallBack callback);//C++ Method Return ProtoType


        public void SetSFXLoader(string path)
        {
            try
            {
                MyAction1 method = (MyAction1)GetAddress(instance, "SetSFXLoader", typeof(MyAction1));
                method(path);
            }
            catch (Exception ee)
            {
                Console.WriteLine("ee=" + ee.ToString());
            }
        }

        public void SetOutputFile(string path)
        {
            try
            {
                MyAction1 method = (MyAction1)GetAddress(instance, "SetOutputFile", typeof(MyAction1));
                method(path);
            }
            catch
            {
            }
        }

        public void AddFile(string path,string alias,string parent)
        {
            if (System.IO.Directory.Exists(path)) {
                //Console.WriteLine("no folder");
                AddFolder(path, parent);
                return;
            }

            if (!System.IO.File.Exists(path)) return;
            
           
            try
            {
                MyAction3 method = (MyAction3)GetAddress(instance, "AddFile", typeof(MyAction3));
                method(path,alias,parent);
            }
            catch
            {
            }
           
            //Console.WriteLine(path + "," + parent);
        }

        private void AddFolder(string path,string parent)
        {
            System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(path);
            if (!dirinfo.Exists) return;

            foreach (FileInfo fileinfo in dirinfo.GetFiles())
            {
                AddFile(fileinfo.FullName,"", parent);
            }

            foreach (DirectoryInfo dir in dirinfo.GetDirectories())
            {
                if (parent.Length == 0)
                {
                    AddFolder(dir.FullName, dir.Name);
                }
                else {
                    AddFolder(dir.FullName, parent + "\\" + dir.Name);
                }
            }

            /*
            foreach (string file in System.IO.Directory.GetFiles())
            {
                AddFile(file, parent);
            }
            
            foreach (string dir in System.IO.Directory.GetDirectories())
            {
                AddFile(file, parent + "\\" + );
            }
            */

        }

        public void DoZip()
        {
            try
            {
                DoWorkDelegate method = (DoWorkDelegate)GetAddress(instance, "DoZip", typeof(DoWorkDelegate));
                method(CallBackFunc);
                //company = Marshal.PtrToStringAnsi(method());
            }
            catch(Exception ee)
            {
                Console.WriteLine("ee=" + ee.ToString());
            }
          
        }

        public void CallBackFunc(int iProgress)
        {
            if (OnProgress != null) {
                OnProgress(iProgress);
            }
            //Console.WriteLine("iProgress=" + iProgress);
        }
    }
}
