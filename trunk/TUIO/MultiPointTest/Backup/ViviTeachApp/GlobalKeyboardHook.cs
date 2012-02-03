using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CloudPaperApp
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class KeyboardHookStruct
    {
        public int vkCode;

        public int scanCode;

        public int flags;

        public int time;

        public int dwExtraInfo;

    }

    public class GlobalKeyboardHook
    {
        private const int HC_ACTION = 0;
        private const int LLKHF_EXTENDED = 0x01;
        public const int LLKHF_ALTDOWN = 0x20;
        private const int VK_T = 0x54;
        private const int VK_P = 0x50;
        private const int VK_W = 0x57;
        private const int VK_TAB = 0x9;
        public const int VK_CONTROL = 0x11;
        public const int WM_KEYDOWN = 0x100; 
        public const int WM_KEYUP = 0x101; 
        public const int WM_KEYPRESS = 0x102; 
    


       
        public static void InstallHook(MyFunc func)
        {
            HotKeyFunc = func;

            Process curProcess = Process.GetCurrentProcess();
            ProcessModule curModule = curProcess.MainModule;

            m_KbdHookProc = new HookProc(KeyboardHookProc);

            int m_HookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, m_KbdHookProc,GetModuleHandle(curModule.ModuleName), 0);



            if (m_HookHandle == 0)
            {
                System.Windows.Forms.MessageBox.Show("呼叫 SetWindowsHookEx 失敗!");
                return;
            }
        }
        public static void UnInstallHook()
        {
            if (m_HookHandle != 0)
            {
                bool ret = UnhookWindowsHookEx(m_HookHandle);

                if (ret == false)
                {
                   System.Windows.Forms.MessageBox.Show("呼叫 UnhookWindowsHookEx 失敗!");

                    return;

                }

                m_HookHandle = 0;

                //button1.Text = "設置鍵盤掛鉤";

            }
        }

        public delegate void MyFunc(KeyboardHookStruct struct1,int wParam);
        public static MyFunc HotKeyFunc = null;

        public static int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            //if (CV.ProjectVar.IsClose)
            //{
            //    UnInstallHook();
            //    return CallNextHookEx(m_HookHandle, nCode, wParam, lParam);
            //}

            // 當按鍵按下及鬆開時都會觸發此函式，這裡只處理鍵盤按下的情形。
            bool isPressed = (lParam.ToInt32() & 0x80000000) == 0;

            if (nCode < 0 || !isPressed)
            {
                return CallNextHookEx(m_HookHandle, nCode, wParam, lParam);
            }


            KeyboardHookStruct struct1 = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
            
           // if ((wParam == 0x100) || (wParam == 260))
            if (wParam == WM_KEYDOWN || wParam == WM_KEYUP)
            {
                
                HotKeyFunc(struct1,wParam);

              
                //Console.wr
                //Console.WriteLine((System.Windows.Forms.Keys)struct1.vkCode + "--->" + struct1.vkCode);
                //PrintScreen--->44
                //F5--->116
                //F11--->122
            }


            int fuck = 0;

            try
            {
                fuck = CallNextHookEx(m_HookHandle, nCode, wParam, lParam);
            }
            catch { 
            }
            return fuck;

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);


        public class KeyboardInfo
        {

            private KeyboardInfo() { }



            [DllImport("user32")]

            private static extern short GetKeyState(int vKey);


            public static KeyStateInfo GetKeyState(System.Windows.Forms.Keys key)
            {

                int vkey = (int)key;



                if (key == System.Windows.Forms.Keys.Alt)
                {

                    vkey = 0x12;    // VK_ALT

                }



                short keyState = GetKeyState(vkey);

                int low = Low(keyState);

                int high = High(keyState);

                bool toggled = (low == 1);

                bool pressed = (high == 1);



                return new KeyStateInfo(key, pressed, toggled);

            }



            private static int High(int keyState)
            {

                if (keyState > 0)
                {

                    return keyState >> 0x10;

                }

                else
                {

                    return (keyState >> 0x10) & 0x1;

                }



            }



            private static int Low(int keyState)
            {

                return keyState & 0xffff;

            }

        }

        public struct KeyStateInfo
        {

            System.Windows.Forms.Keys m_Key;

            bool m_IsPressed;

            bool m_IsToggled;



            public KeyStateInfo(System.Windows.Forms.Keys key, bool ispressed, bool istoggled)
            {

                m_Key = key;

                m_IsPressed = ispressed;

                m_IsToggled = istoggled;

            }



            public static KeyStateInfo Default
            {

                get
                {

                    return new KeyStateInfo(System.Windows.Forms.Keys.None, false, false);
                }

            }



            public System.Windows.Forms.Keys Key
            {

                get { return m_Key; }

            }



            public bool IsPressed
            {

                get { return m_IsPressed; }

            }



            public bool IsToggled
            {

                get { return m_IsToggled; }

            }

        }





        #region hide

        private const int WH_KEYBOARD_LL = 13;

        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        private static HookProc m_KbdHookProc;
        private static int m_HookHandle;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // 設置掛鉤.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        // 將之前設置的掛鉤移除。記得在應用程式結束前呼叫此函式.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        // 呼叫下一個掛鉤處理常式（若不這麼做，會令其他掛鉤處理常式失效）.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        #endregion
    }
}
