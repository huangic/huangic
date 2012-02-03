/*
  http://www.codeproject.com/KB/cs/Enum_Recording_Devices.aspx
      clsRecDevices recDev = new clsRecDevices();
            for (int i = 0; i < recDev.Count; i++)
            {
                this.textBox1.AppendText(recDev[i] + Environment.NewLine);
            }
*/



using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace WaveLib
{
    

    class clsRecDevices
    {      
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct WaveInCaps
        {
            public short wMid;
            public short wPid;
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szPname;
            public uint dwFormats;
            public short wChannels;
            public short wReserved1;
        }      

        [DllImport("winmm.dll")]
        public static extern int waveInGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "waveInGetDevCaps")]
        public static extern int waveInGetDevCapsA(int uDeviceID, ref WaveInCaps lpCaps, int uSize);

        ArrayList arrLst = new ArrayList();

        int position = -1;

        public int Count
        {            
            get {return arrLst.Count;}
        }

        public string this[int indexer]
        {
            get{return (string)arrLst[indexer];}
        }

        public clsRecDevices()
        {
            int waveInDevicesCount = waveInGetNumDevs();
            if (waveInDevicesCount > 0)
            {
                for (int uDeviceID = 0; uDeviceID < waveInDevicesCount; uDeviceID++)
                {
                    WaveInCaps waveInCaps = new WaveInCaps();
                    waveInGetDevCapsA(uDeviceID,ref waveInCaps,Marshal.SizeOf(typeof(WaveInCaps)));
                    arrLst.Add(new string(waveInCaps.szPname).Remove(new string(waveInCaps.szPname).IndexOf('\0')).Trim());                    
                }
            }
        }        
    }
}
