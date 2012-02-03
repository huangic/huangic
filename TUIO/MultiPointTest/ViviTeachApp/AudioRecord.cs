using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Yeti.MMedia.Mp3;
using Yeti.MMedia;
using WaveLib;
using System.Threading;
using System.Windows.Forms;
namespace CloudPaperApp
{
    public class AudioRecord
    {
        public static AudioRecord mInstance = null;
        private static string mFileName = null;
        private static bool mRecording = false;

        public static void OnRecordStart(string filename) 
        {
            Debug.WriteLine2("AudioRecord.OnRecordStart(" + filename + ")");
            WaveLib.clsRecDevices device = new WaveLib.clsRecDevices();
            if (device.Count == 0)
            {
                MessageBox.Show("No Microphone Device.","Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                return;
            }

            AudioRecord.mRecording = true;
            AudioRecord.mFileName = filename;
            AudioRecord.mInstance = new AudioRecord();
            AudioRecord.mInstance.Start();
        }

        public static void OnRecordStop()
        {
            AudioRecord.mRecording = false;
            AudioRecord.mInstance.Stop();
        }

        private void Start() 
        {
            Stop();

            WaitCallback RecordAVIThreadCallBack2 = new WaitCallback(Run);
            ThreadPool.QueueUserWorkItem(RecordAVIThreadCallBack2, "Thread2");

        }

        private void Stop()
        {
            Debug.WriteLine2("Stop");
            RecordStop();
        }

        private void Run(object states)
        {
            Debug.WriteLine2("Run");
            try
            {
                WaveFormat fmt = new WaveFormat(44100, 16, 2); // this line was already there, so add the following right after...
                this.Fifo = new FifoStream();
                this.AudioWriter = new Mp3Writer(System.IO.File.Create(AudioRecord.mFileName), fmt);
                this.AudioRecorder = new WaveInRecorder(-1, fmt, 16384, 3, new BufferDoneEventHandler(Recording));
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
                RecordStop();
            }
        }

        private void Recording(IntPtr data, int size)
        {
            try
            {
                if (m_RecBuffer == null || m_RecBuffer.Length < size)
                    m_RecBuffer = new byte[size];
                
                Marshal.Copy(data, m_RecBuffer, 0, size);
                Fifo.Write(m_RecBuffer, 0, m_RecBuffer.Length); // this line was already there, so add the following right after...
                AudioWriter.Write(m_RecBuffer, 0, m_RecBuffer.Length); // recording
            }
            catch { }
        }

        private void RecordStop()
        {
            Debug.WriteLine2("RecordStop()");

            //MessageBox.Show("RecordAudioStop1");
            try
            {
                if (this.AudioRecorder != null)
                {
                    this.AudioRecorder.Dispose(); // this line was already there, so add the following right after...
                }
            }
            catch
            {
            }
            finally {
                this.AudioRecorder = null;
            }
            Debug.WriteLine2("RecordStop()");
            try
            {
                if (this.AudioWriter != null)
                {
                    this.AudioWriter.Close(); // recording
                }
            }
            catch
            {
            }
            finally {
                this.AudioWriter = null;
            }
            Debug.WriteLine2("RecordStop()");
            try
            {
                this.Fifo = null;
            }
            catch { }
            Debug.WriteLine2("RecordStop()");
        }




        private Mp3Writer AudioWriter = null;
        private WaveInRecorder AudioRecorder = null;
        private FifoStream Fifo = new FifoStream();
        private byte[] m_RecBuffer = null;

        


    }
}
