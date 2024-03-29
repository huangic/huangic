using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;


namespace CloudPaperApp
{
    public class API
    {
        /*
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOREDRAW = 0x0008;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public const uint SWP_HIDEWINDOW = 0x0080;
        public const uint SWP_NOCOPYBITS = 0x0100;
        public const uint SWP_NOOWNERZORDER = 0x0200;
        */

        #region WM
        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOREDRAW = 0x0008;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_FRAMECHANGED = 0x0020;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_NOOWNERZORDER = 0x0200;
        public const int SWP_NOSENDCHANGING = 0x0400;

        public const int WM_NOTIFY = 0x4E;
        public const int EN_REQUESTRESIZE = 0x701;

        #region SendInput Constants
        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;
        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const uint KEYEVENTF_KEYUP = 0x0002;
        public const uint KEYEVENTF_UNICODE = 0x0004;
        public const uint KEYEVENTF_SCANCODE = 0x0008;
        public const uint XBUTTON1 = 0x0001;
        public const uint XBUTTON2 = 0x0002;
        public const uint MOUSEEVENTF_MOVE = 0x0001;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const uint MOUSEEVENTF_XDOWN = 0x0080;
        public const uint MOUSEEVENTF_XUP = 0x0100;
        public const uint MOUSEEVENTF_WHEEL = 0x0800;
        public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        #endregion


        public const int WM_COPYDATA = 0x4A;

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MOUSEMOVE = 0x0200;

        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x0102;

        public static readonly uint WM_SYSCOMMAND = 0x0112;
        public static readonly int SC_MOVE = 61456;
        public static readonly int HTCAPTION = 2;


        public const int ULW_COLORKEY = 1;
        public const int ULW_ALPHA = 2;
        public const int ULW_OPAQUE = 4;
        public const byte AC_SRC_OVER = 0;
        public const byte AC_SRC_ALPHA = 1;


        public const string VistaStartMenuCaption = "Start";
        public static IntPtr vistaStartMenuWnd = IntPtr.Zero;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        public delegate bool EnumThreadProc(IntPtr hwnd, IntPtr lParam);

        public static Int32 ERROR_NO_MORE_FILES = 259;

        public static Int32 LINE_LEN = 256;

        public static Int32 DIGCF_DEFAULT = 0x00000001;  // only valid with DIGCF_DEVICEINTERFACE
        public static Int32 DIGCF_PRESENT = 0x00000002;
        public static Int32 DIGCF_ALLCLASSES = 0x00000004;
        public static Int32 DIGCF_PROFILE = 0x00000008;
        public static Int32 DIGCF_DEVICEINTERFACE = 0x00000010;

        public static Int32 SPINT_ACTIVE = 0x00000001;
        public static Int32 SPINT_DEFAULT = 0x00000002;
        public static Int32 SPINT_REMOVED = 0x00000004;
        #endregion

        #region enum

        public enum Bool
        {
            False = 0,
            True,
        }

        public enum BinaryRasterOperations
        {

            R2_BLACK = 1,
            R2_NOTMERGEPEN = 2,
            R2_MASKNOTPEN = 3,
            R2_NOTCOPYPEN = 4,
            R2_MASKPENNOT = 5,
            R2_NOT = 6,
            R2_XORPEN = 7,
            R2_NOTMASKPEN = 8,
            R2_MASKPEN = 9,
            R2_NOTXORPEN = 10,
            R2_NOP = 11,
            R2_MERGENOTPEN = 12,
            R2_COPYPEN = 13,
            R2_MERGEPENNOT = 14,
            R2_MERGEPEN = 15,
            R2_WHITE = 16
        }

        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source                   */
            SRCPAINT = 0x00EE0086, /* dest = source OR dest           */
            SRCAND = 0x008800C6, /* dest = source AND dest          */
            SRCINVERT = 0x00660046, /* dest = source XOR dest          */
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )   */
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)             */
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)     */
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest     */
            PATCOPY = 0x00F00021, /* dest = pattern                  */
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo                   */
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest         */
            DSTINVERT = 0x00550009, /* dest = (NOT dest)               */
            BLACKNESS = 0x00000042, /* dest = BLACK                    */
            WHITENESS = 0x00FF0062, /* dest = WHITE                    */
            CAPTUREBLT = 1073741824
        };

        public enum VK : ushort
        {
            SHIFT = 0x10,
            CONTROL = 0x11,
            MENU = 0x12,
            ESCAPE = 0x1B,
            BACK = 0x08,
            TAB = 0x09,
            RETURN = 0x0D,
            PRIOR = 0x21,
            NEXT = 0x22,
            END = 0x23,
            HOME = 0x24,
            LEFT = 0x25,
            UP = 0x26,
            RIGHT = 0x27,
            DOWN = 0x28,
            SELECT = 0x29,
            PRINT = 0x2A,
            EXECUTE = 0x2B,
            SNAPSHOT = 0x2C,
            INSERT = 0x2D,
            DELETE = 0x2E,
            HELP = 0x2F,
            NUMPAD0 = 0x60,
            NUMPAD1 = 0x61,
            NUMPAD2 = 0x62,
            NUMPAD3 = 0x63,
            NUMPAD4 = 0x64,
            NUMPAD5 = 0x65,
            NUMPAD6 = 0x66,
            NUMPAD7 = 0x67,
            NUMPAD8 = 0x68,
            NUMPAD9 = 0x69,
            MULTIPLY = 0x6A,
            ADD = 0x6B,
            SEPARATOR = 0x6C,
            SUBTRACT = 0x6D,
            DECIMAL = 0x6E,
            DIVIDE = 0x6F,
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            OEM_1 = 0xBA,   // ',:' for US
            OEM_PLUS = 0xBB,   // '+' any country
            OEM_COMMA = 0xBC,   // ',' any country
            OEM_MINUS = 0xBD,   // '-' any country
            OEM_PERIOD = 0xBE,   // '.' any country
            OEM_2 = 0xBF,   // '/?' for US
            OEM_3 = 0xC0,   // '`~' for US
            MEDIA_NEXT_TRACK = 0xB0,
            MEDIA_PREV_TRACK = 0xB1,
            MEDIA_STOP = 0xB2,
            MEDIA_PLAY_PAUSE = 0xB3,
            LWIN = 0x5B,
            RWIN = 0x5C
        }

        #endregion

        #region Struct


        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }
            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }
            public Size Size { get { return new Size(Width, Height); } }

            public Point Location { get { return new Point(Left, Top); } }
            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }
            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }
            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                ^ ((Width << 0x1a) | (Width >> 6))
                ^ ((Height << 7) | (Height >> 0x19));
            }
           
            public static implicit operator Rectangle(RECT rect)
            {
                return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
            public static implicit operator RECT(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
          
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        // <StructLayout(LayoutKind.Sequential, , 1)> _
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 cx, Int32 cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        // <StructLayout(LayoutKind.SequentialPack = 1)> _
        public struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            /// <summary>
            /// Size of structure in bytes
            /// </summary>
            public Int32 cbSize;
            /// <summary>
            /// GUID of the device interface class
            /// </summary>
            public Guid ClassGuid;
            /// <summary>
            /// Handle to this device instance
            /// </summary>
            public Int32 DevInst;
            /// <summary>
            /// Reserved; do not use. 
            /// </summary>
            public UIntPtr Reserved;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA
        {
            /// <summary>
            /// Size of the structure, in bytes
            /// </summary>
            public Int32 cbSize;
            /// <summary>
            /// GUID of the device interface class
            /// </summary>
            public Guid InterfaceClassGuid;
            /// <summary>
            /// 
            /// </summary>
            public Int32 Flags;
            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public IntPtr Reserved;

        };


        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DRVINFO_DATA
        {
            public Int32 cbSize;
            public Int32 DriverType;
            public UIntPtr Reserved;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String Description;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String MfgName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String ProviderName;
            public FILETIME DriverDate;
            public Int64 DriverVersion;
        };

        public struct NMHDR
        {
            /// <summary>
            /// Window handle to the control sending a message.
            /// </summary>
            public int hwndFrom;
            /// <summary>
            /// Identifier of the control sending a message.
            /// </summary>
            public int idFrom;
            /// <summary>
            /// Notification code. This member can be a control-specific notification code or it can be one of the common notification codes.
            /// </summary>
            public int code;
        }

        /// <summary>
        /// 需要改變大小
        /// </summary>
        public struct REQSIZE
        {
            /// <summary>
            /// NMHDR結構
            /// </summary>
            public NMHDR nmhdr;
            /// <summary>
            /// RECT結構
            /// </summary>
            public RECT rect;
        }
        #endregion

        #region gdi32.dll
        //GDI32
        [DllImport("gdi32.dll", ExactSpelling=true, SetLastError=true)]
        public static extern bool BitBlt(IntPtr hObject,
                                   int nXDest,
                                   int nYDest,
                                   int nWidth,
                                   int nHeight,
                                   IntPtr hObjSource,
                                   int nXSrc,
                                   int nYSrc,
                                   TernaryRasterOperations dwRop);


        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int
        wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);



        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, int lpInitData);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int CreatePen(short nPenStyle, short nWidth, int crColor);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int DeleteDC(IntPtr hdc);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(IntPtr hObject);

     

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);


        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int LineTo(IntPtr hdc, int x, int y);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int MoveToEx(IntPtr hdc, int x, int y, int lpPoint);

     
        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int SelectObject(IntPtr hdc, int hObject);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [PreserveSig]
        [DllImport("gdi32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern bool SetROP2(IntPtr hdc, BinaryRasterOperations nDrawingType);

        [PreserveSig]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x,int y, int cx, int cy, int wFlags);

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int nBkMode);

        [DllImport("gdi32.dll")]
        public static extern UInt32 SetBkColor(IntPtr hwnd, UInt32 unColor);

        [DllImport("gdi32.dll")]
        public static extern int MaskBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int hbmMask, int xMask, int yMask, uint dwRop);

        #endregion

        #region user32.dll
        //user32.dll

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr iParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)] // used for button-down & button-up
        public static extern int PostMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
      
        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

       
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [PreserveSig]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int RedrawWindow(IntPtr hwnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint fuRedraw);

        [PreserveSig]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int ReleaseDC(IntPtr hwnd, int hdc);

        [DllImport("user32")]
        public static extern int PrintWindow(IntPtr hWnd, IntPtr dc, uint flags);


        [PreserveSig]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [PreserveSig]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetDesktopWindow();

        [PreserveSig]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi)]
        public static extern int InvalidateRect(int hwnd, int lpRect, int bErase);


        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumThreadWindows(int threadId, EnumThreadProc pfnEnum, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool MoveWindow(
            IntPtr hWnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool bRepaint);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);

        #endregion

        #region setupapi.dll

        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevsEx(IntPtr ClassGuid, [MarshalAs(UnmanagedType.LPStr)]String enumerator, IntPtr hwndParent, Int32 Flags, IntPtr DeviceInfoSet, [MarshalAs(UnmanagedType.LPStr)]String MachineName, IntPtr Reserved);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, IntPtr InterfaceClassGuid, Int32 MemberIndex, ref  SP_DEVINFO_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, Int32 MemberIndex, ref  SP_DEVINFO_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiClassNameFromGuid(ref Guid ClassGuid, StringBuilder className, Int32 ClassNameSize, ref Int32 RequiredSize);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiGetClassDescription(ref Guid ClassGuid, StringBuilder classDescription, Int32 ClassDescriptionSize, ref Int32 RequiredSize);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, Int32 DeviceInstanceIdSize, ref Int32 RequiredSize);

        [DllImport("kernel32.dll")]
        public static extern Int32 GetLastError();

        #endregion

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, frmViviTeach.FlaWndProc wndProc);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


        [DllImport("Iphlpapi.dll")]
        public static extern int SendARP(Int32 dest, Int32 host, ref Int32 mac, ref Int32 length);

        [DllImport("Ws2_32.dll")]
        public static extern Int32 inet_addr(string ip);

        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);


        [DllImport("winmm.dll")]
        public static extern long mciSendString(string strCommand,StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }



        public enum Key
        {
            //      鼠标动代码:
            move = 0x0001,
            leftdown = 0x0002,
            leftup = 0x0004,
            rightdown = 0x0008,
            rightup = 0x0010,
            middledown = 0x0020,
            //键盘动作代码:
            VK_LBUTTON = 1,      //鼠标左键 
            VK_RBUTTON = 2,　    //鼠标右键 
            VK_CANCEL = 3,　　　 //Ctrl+Break(通常不需要处理) 
            VK_MBUTTON = 4,　　  //鼠标中键 
            VK_BACK = 8, 　　　  //Backspace 
            VK_TAB = 9,　　　　  //Tab 
            VK_CLEAR = 12,　　　 //Num Lock关闭时的数字键盘5 
            VK_RETURN = 13,　　　//Enter(或者另一个) 
            VK_SHIFT = 16,　　　 //Shift(或者另一个) 
            VK_CONTROL = 17,　　 //Ctrl(或者另一个） 
            VK_MENU = 18,　　　　//Alt(或者另一个) 
            VK_PAUSE = 19,　　　 //Pause 
            VK_CAPITAL = 20,　　 //Caps Lock 
            VK_ESCAPE = 27,　　　//Esc 
            VK_SPACE = 32,　　　 //Spacebar 
            VK_PRIOR = 33,　　　 //Page Up 
            VK_NEXT = 34,　　　　//Page Down 
            VK_END = 35,　　　　 //End 
            VK_HOME = 36,　　　　//Home 
            VK_LEFT = 37,　　　  //左箭头 
            VK_UP = 38,　　　　  //上箭头 
            VK_RIGHT = 39,　　　 //右箭头 
            VK_DOWN = 40,　　　  //下箭头 
            VK_SELECT = 41,　　  //可选 
            VK_PRINT = 42,　　　 //可选 
            VK_EXECUTE = 43,　　 //可选 
            VK_SNAPSHOT = 44,　　//Print Screen 
            VK_INSERT = 45,　　　//Insert 
            VK_DELETE = 46,　　  //Delete 
            VK_HELP = 47,　　    //可选 
            VK_NUM0 = 48,        //0
            VK_NUM1 = 49,        //1
            VK_NUM2 = 50,        //2
            VK_NUM3 = 51,        //3
            VK_NUM4 = 52,        //4
            VK_NUM5 = 53,        //5
            VK_NUM6 = 54,        //6
            VK_NUM7 = 55,        //7
            VK_NUM8 = 56,        //8
            VK_NUM9 = 57,        //9
            VK_A = 65,          //A
            VK_B = 66,          //B
            VK_C = 67,          //C
            VK_D = 68,          //D
            VK_E = 69,          //E
            VK_F = 70,          //F
            VK_G = 71,          //G
            VK_H = 72,          //H
            VK_I = 73,          //I
            VK_J = 74,          //J
            VK_K = 75,          //K
            VK_L = 76,          //L
            VK_M = 77,          //M
            VK_N = 78,          //N
            VK_O = 79,          //O
            VK_P = 80,          //P
            VK_Q = 81,          //Q
            VK_R = 82,          //R
            VK_S = 83,          //S
            VK_T = 84,          //T
            VK_U = 85,          //U
            VK_V = 86,          //V
            VK_W = 87,          //W
            VK_X = 88,          //X
            VK_Y = 89,          //Y
            VK_Z = 90,          //Z
            VK_NUMPAD0 = 96,    //0
            VK_NUMPAD1 = 97,    //1
            VK_NUMPAD2 = 98,    //2
            VK_NUMPAD3 = 99,    //3
            VK_NUMPAD4 = 100,    //4
            VK_NUMPAD5 = 101,    //5
            VK_NUMPAD6 = 102,    //6
            VK_NUMPAD7 = 103,    //7
            VK_NUMPAD8 = 104,    //8
            VK_NUMPAD9 = 105,    //9
            VK_NULTIPLY = 106,　 //数字键盘上的* 
            VK_ADD = 107,　　　　//数字键盘上的+ 
            VK_SEPARATOR = 108,　//可选 
            VK_SUBTRACT = 109,　 //数字键盘上的- 
            VK_DECIMAL = 110,　　//数字键盘上的. 
            VK_DIVIDE = 111,　　 //数字键盘上的/
            VK_F1 = 112,
            VK_F2 = 113,
            VK_F3 = 114,
            VK_F4 = 115,
            VK_F5 = 116,
            VK_F6 = 117,
            VK_F7 = 118,
            VK_F8 = 119,
            VK_F9 = 120,
            VK_F10 = 121,
            VK_F11 = 122,
            VK_F12 = 123,
            VK_NUMLOCK = 144,　　//Num Lock 
            VK_SCROLL = 145, 　  // Scroll Lock 
            middleup = 0x0040,
            xdown = 0x0080,
            xup = 0x0100,
            wheel = 0x0800,
            virtualdesk = 0x4000,
            absolute = 0x8000
        }
   

    }
}
