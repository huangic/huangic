using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Windows.Forms;


namespace CloudPaperApp
{
	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
   
	public class Utilities
    {

        #region Rotation Math

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static RectangleF DiffXY(RectangleF iBound, PointF C, float R,RectangleF gBound)
        {
            PointF P = iBound.Location;
            P.X += iBound.Width / 2;
            P.Y += iBound.Height / 2;

            //Starlife Calculator
            PointF nP = RotatePoints(new PointF[] { P }, gBound, R)[0];
            nP.X -= iBound.Width / 2;
            nP.Y -= iBound.Height / 2;
            iBound.Location = nP;

            /*
            //Jerry Calculator
            double radius = Math.Sqrt(Math.Pow(C.X - P.X, 2) + Math.Pow(C.Y - P.Y, 2));
            double b = P.X - C.X;
            double a = C.Y - P.Y;
            double angle = Math.Atan2(a, b) / (Math.PI / 180d);
            double newAngle = angle - R;

            double sin = Math.Sin((newAngle) / 180d * Math.PI);
            double cos = Math.Cos((newAngle) / 180d * Math.PI);
            double difY = a - (sin * radius);
            double difX = (cos * radius) - b;

            //double difY = P.X * Math.Cos(newAngle) - P.Y * Math.Sin(newAngle);
            //double difX = P.Y * Math.Cos(newAngle) + P.X * Math.Sin(newAngle);

            //PointF newp = RotatePoints(new PointF[] { P }, bound, R);

            iBound.X += (float)difX;
            iBound.Y += (float)difY;
            */
            return iBound;
        }

        public static PointF[] RotatePoints(PointF[] pts, RectangleF bound, float Rotation)
        {
            //轉換座標
            Matrix mx = new Matrix();
            PointF centerPoint = new PointF(bound.X + (bound.Width / 2), bound.Y + (bound.Height / 2));
            mx.RotateAt(Rotation, centerPoint);
            mx.TransformPoints(pts);
            mx.Dispose();

            return pts;
        }


        public static bool CircleInRegion(RectangleF rect, PointF point)
        {
           
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(rect);
            //gp.Widen(Pens.Black);
            Region rg = new Region(gp);
            bool yn = rg.IsVisible(point);

            rg.Dispose();
            gp.Dispose();

            return yn;

        }

        public static bool CircleInRegion(RectangleF rect, RectangleF sel_rect)
        {

            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(rect);
            //gp.Widen(Pens.Black);
            Region rg = new Region(gp);
            bool yn = rg.IsVisible(sel_rect);
            rg.Dispose();
            gp.Dispose();

            return yn;

        }

        public static bool LineInRegion(PointF[] pts, PointF point,Pen pen)
        {

            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(pts);
            gp.Widen(pen);
            Region rg = new Region(gp);
            bool yn = rg.IsVisible(point);
            rg.Dispose();
            gp.Dispose();


            return yn;
        }

        public static bool LineInRegion(PointF[] pts, RectangleF sel_rect, Pen pen)
        {

            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(pts);
            gp.Widen(pen);
            Region rg = new Region(gp);
            bool yn = rg.IsVisible(sel_rect);
            rg.Dispose();
            gp.Dispose();

            return yn;
        }


        public static bool PolygonInRegion(PointF[] pts, PointF point)
        {



            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(pts);
            //gp.Widen(Pens.Black);
            Region rg = new Region(gp);
            bool yn = rg.IsVisible(point);
            rg.Dispose();
            gp.Dispose();

        
            return yn;
        }

        public static bool PolygonInRegion(PointF[] pts, RectangleF sel_rect)
        {
            
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(pts);
            //gp.Widen(Pens.Black);
            Region rg = new Region(gp);
            bool yn = rg.IsVisible(sel_rect);
            rg.Dispose();
            gp.Dispose();

            return yn;
        }

      

        public static double PointToAngle(PointF origin, PointF target)
        {
            double angle = 0;
            target.X = target.X - origin.X;
            target.Y = target.Y - origin.Y;
            angle = Math.Atan2(target.Y, target.X) / (Math.PI / 180);
            return angle;
        }

        public static PointF RotateMousePointer(Point pt, Rectangle allbound, RectangleF bound, float Rotation)
        {
            return RotateMousePointer(new PointF(pt.X, pt.Y), allbound, bound, Rotation);
        }

        public static PointF RotateMousePointer(PointF pt, Rectangle allbound, RectangleF bound, float Rotation)
        {
            Rotation = Rotation % 360;

            //Rectangle bound = CV.s_frmDrawSurface.ClientRectangle;//總寬度
            //Rectangle bound2 = rect;

            //// Backtrack the mouse...
            PointF[] pts = new PointF[] { pt };
            Matrix mx = new Matrix();
            mx.Translate(-allbound.Width / 2, -allbound.Height / 2, MatrixOrder.Append);
            PointF rotationCenter = new PointF(bound.X + (bound.Width / 2), bound.Y + (bound.Height / 2));//中心點
            mx.RotateAt(Rotation, rotationCenter);
            mx.Translate(allbound.Width / 2, allbound.Height / 2, MatrixOrder.Append);
            mx.Scale(1, 1, MatrixOrder.Append);
            mx.Invert();
            mx.TransformPoints(pts);


            return pts[0];
        }

        public static PointF RotateMousePointer(Point p, RectangleF bound, float Rotation)
        {
            return RotateMousePointer(new PointF(p.X, p.Y), bound, Rotation);
        }

        public static PointF RotateMousePointer(PointF p, RectangleF bound, float Rotation)
        {
            Rotation = Rotation % 360;

            //Rectangle bound = CV.s_frmCurrentSurface.ClientRectangle;
            //Rectangle bound = this.Bound;
            //// Backtrack the mouse...
            PointF[] pts = new PointF[] { p };
            Matrix mx = new Matrix();
            //mx.Translate(-CV.s_frmCurrentSurface.ClientSize.Width / 2, -CV.s_frmCurrentSurface.ClientSize.Height / 2);            
            PointF rotationCenter = new PointF(bound.X + (bound.Width / 2), bound.Y + (bound.Height / 2));
            mx.RotateAt(Rotation, rotationCenter);
            //mx.Rotate((float)m_currentRotation, MatrixOrder.Append);
            //mx.Translate(CV.s_frmCurrentSurface.ClientSize.Width / 2 , CV.s_frmCurrentSurface.ClientSize.Height / 2 );
            //mx.Scale(1, 1, MatrixOrder.Append);
            //mx.Invert();
            mx.TransformPoints(pts);


            return pts[0];
        }

        public static Rectangle GetFuckBounds(int[] LValue, int[] TValue, int[] RValue, int[] BValue)
        {
            Rectangle rect = Rectangle.Empty;


            int minX = BinaryReduce<int>(Math.Min, LValue);
            int minY = BinaryReduce<int>(Math.Min, TValue);
            int maxX = BinaryReduce<int>(Math.Max, RValue);
            int maxY = BinaryReduce<int>(Math.Max, BValue);


            rect = Rectangle.FromLTRB(minX, minY, maxX, maxY);
            return rect;
        }



        //取得旋轉過後的Rectangle
        public static Rectangle GetRotatedBounds(Rectangle rect,Point centerPt, double angle)
        {
            // Create rotation matrix
            Matrix mx = new Matrix();

            //Point pt = new Point(rect.X+rect.Width/2 , rect.Y + rect.Height/2);
            mx.RotateAt((float)angle, centerPt);

            // Get bounds of the original unrotated image
            //Rectangle bmpBounds = new Rectangle(loc, new Size(size.Width, size.Height));

           
            // Construct rectangle with these bounds
            Point[] pts = {
                new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Top),
                new Point(rect.Right, rect.Bottom),
                new Point(rect.Left, rect.Bottom)};

            // Rotate the points of the bounds
            mx.TransformPoints(pts);
            mx.Dispose();

            // Get min/max values of the new bounds
            int maxX = BinaryReduce<int>(Math.Max, pts[0].X, pts[1].X, pts[2].X, pts[3].X);
            int minX = BinaryReduce<int>(Math.Min, pts[0].X, pts[1].X, pts[2].X, pts[3].X);
            int maxY = BinaryReduce<int>(Math.Max, pts[0].Y, pts[1].Y, pts[2].Y, pts[3].Y);
            int minY = BinaryReduce<int>(Math.Min, pts[0].Y, pts[1].Y, pts[2].Y, pts[3].Y);


            //ArrayList list = new ArrayList();
            //list.Add(points);
            //list.Add(new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY)));
            // Return resulting bounding rectangle
            //return list;
            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
           
        }

        public delegate T BinaryFunction<T>(T a, T b);

        public static T BinaryReduce<T>(BinaryFunction<T> function, params T[] values)
        {
            if (values.Length == 0)
                return default(T);

            if (values.Length == 1)
                return values[0];

            T result = values[0];
            for (int i = 1; i < values.Length; i++)
                result = function(result, values[i]);

            return result;
        }

        public static Size rotateAngle(Size size, double angle)
        {
            const double pi2 = Math.PI / 2.0;

            // Why can't C# allow these to be const, or at least readonly
            // *sigh*  I'm starting to talk like Christian Graus :omg:
            double oldWidth = (double)size.Width;
            double oldHeight = (double)size.Height;

            // Convert degrees to radians
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;

            // Ensure theta is now [0, 2pi)
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight; // The newWidth/newHeight expressed as ints

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;

            // We need to calculate the sides of the triangles based
            // on how much rotation is being done to the bitmap.
            //   Refer to the first paragraph in the explaination above for 
            //   reasons why.
            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);




            return new Size(nWidth, nHeight);
            //return new Rectangle(points.X,points.Y, nWidth, nHeight);
        }

     
        #endregion

        #region other

        public static Rectangle ScreenArea{
            get { return Screen.PrimaryScreen.Bounds; }
        }

        public static Rectangle ScreenWorkingArea
        {
            get { return Screen.PrimaryScreen.WorkingArea; }
        }

        public static string TimeFormat(float time)
        {
            StringBuilder builder = new StringBuilder();

            int t = (int)Math.Floor(time);

            int hour = (int)(t / 3600);
            int minute = ((int)(t / 60)) % 60;
            int sec = (int)(t % 60);

            builder.Append(string.Format("{0:00}", hour));
            builder.Append(":");
            builder.Append(string.Format("{0:00}", minute));
            builder.Append(":");
            builder.Append(string.Format("{0:00}", sec));
            //format = System.DateTime.FromFileTime(t).ToString("yyyy-MM-dd hh:mm:ss");

            return builder.ToString();
        }

        public static RectangleF ConverShapeToPPTSize(float x, float y, float w, float h)
        {
            /*
             * 25.4公分=720
             * 19.05公分=540

             * w 1cm = 28.346
             * h 1cm = 28.346
            
             * 1024/720=1.4222222222222222222222222222222
         */
            RectangleF rect = RectangleF.Empty;
            rect.X = (float)Math.Round(x / 1.4222222222222222222222222222222f, 3);
            rect.Y = (float)Math.Round(y / 1.4222222222222222222222222222222f, 3);
            rect.Width = (float)Math.Round(w / 1.4222222222222222222222222222222f);
            rect.Height = (float)Math.Round(h / 1.4222222222222222222222222222222f);

            return rect;
        }

        public static int GetQuadrant(double angle)
        {
            //象限角，又稱象限
            if (angle >= 0 && angle <= 90) return 1;
            if (angle >= 90 && angle <= 180) return 2;
            if (angle >= 180 && angle <= 270) return 3;
            if (angle >= 270 && angle <= 360) return 4;

            return 5;
        }
        
        public static int GetQuadrantPositive(int from,int to)
        {
            int positive = -1;//非0即真

            if (from == 4 && to == 1)
            {
                positive = 1; //+++
            }
            else if (from == 4 && to == 2)
            {
                positive = 1;//+++
            }
            else if (from == 1 && to == 4)
            {
                positive = 0;//---
            }
            else if (from == 1 && to == 3)
            {
                positive = 0;//---
            }
            else if (from == 2 && to == 4)
            {
                positive = 0;//---
            }
            else if (from == 3 && to == 1)
            {
                positive = 1;// +++
            }
          

            return positive;
        }


        public static float GetHeight(Font f, Graphics g)
        {
            return (g.DpiX * f.SizeInPoints / 72);
        }

        public static string RegexNumber(string str)
        {
            string result = null;
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(str, @"\-?\d+");
            if (m.Success)
            {
                result = m.Value;
            }
            return result;
        }

        public static bool RegexIP(string ip)
        {
            bool result = false;
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(ip, @"(([0-1]?[0-9]{1,2}\.)|(2[0-4][0-9]\.)|(25[0-5]\.)){3}(([0-1]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))");
            if (m.Success)
            {
                result = true;
            }
            return result;
        }

        public static List<int> PrintRange(string str)
        {
            string[] tempstr = str.Split(new char[] { ',' });
            List<int> values = new List<int>();
            for (int i = 0; i < tempstr.Length; i++)
            {
                if (tempstr[i].Length != 0)
                {
                    string[] tempstr2 = tempstr[i].Split(new char[] { '-' });
                    if (tempstr2.Length > 1)
                    {
                        int start = Convert.ToInt32(tempstr2[0]);
                        int end = Convert.ToInt32(tempstr2[1]);
                        for (int j = start; j <= end; j++)
                        {
                            values.Add(j);
                        }
                    }
                    else
                    {
                        values.Add(Convert.ToInt32(tempstr[i]));
                    }
                }
            }

            values.Sort();
            return values;
        }

        public static string RegexDelEnter(string src) {
            //string str = "\n\n\nleading\n\n\n\n\n\n\n\n\n\n\n\n";
            string str2 = Regex.Replace(src, @"^\n+", "");
            string str3 = Regex.Replace(str2, @"\n+$", "");

            return str3;
        }

        public static string ReadTagName(string source, string str)
        {
            string sPlaceh = "<" + str + ">";
            string sPlacel = "</" + str + ">";

            int index = source.IndexOf(sPlaceh);
            int index2 = source.IndexOf(sPlacel);
            int len = source.Length;
            string t = "";
            if ((index != -1) && (index2 != -1))
            {
                t = source.Substring(index + sPlaceh.Length, len - ((index + sPlaceh.Length) + (len - index2)));
            }
            else
            {
                t = "";
            }
            return t;
        }

        public static string StringXNumber(string str,int n) {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < n; i++) {
                builder.Append(str);
            }

            return builder.ToString();
        }

        public static int GetPDFPageCount(string filename){
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader r = new StreamReader(fs);
            string pdfText = r.ReadToEnd();
            Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
            MatchCollection matches = rx1.Matches(pdfText);
            r.Close();
            fs.Close();
            return  Convert.ToInt32(matches.Count.ToString());
        }

        public static int[] RandomValue(int minValue, int maxValue, int count)
        {
            int[] intList = new int[maxValue];
            for (int i = 0; i < maxValue; i++)
            {
                intList[i] = i + minValue;
            }
            int[] intRet = new int[count];
            int n = maxValue;
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = rand.Next(0, n);
                intRet[i] = intList[index];
                intList[index] = intList[--n];
            }

            return intRet;
        }

        public static string getRealPath(string globalpath, string filename)
        {
            string result = null;
            FileInfo fileinfo = null;
            try
            {
                fileinfo = new FileInfo(filename);
                if (fileinfo.Exists)
                {
                    result = fileinfo.FullName;
                }
                else
                {
                    filename = filename.Replace("/", "\\");
                    string[] path = filename.Split('\\');
                    fileinfo = new FileInfo(globalpath + "\\" + path[path.Length - 1]);
                    if (fileinfo.Exists)
                    {
                        result = fileinfo.FullName;
                    }
                    else
                    {
                        fileinfo = new FileInfo(globalpath + "\\" + filename);
                        if (fileinfo.Exists)
                        {
                            result = fileinfo.FullName;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
                result = null;
            }

            return result;

        }

        public static string convertToPath(string str) {
            string tttt = "";
            string[] path = str.Split('/');
            if (path.Length <= 1)
            {
                path = str.Split('\\');
            }
            for (int i = 0; i < path.Length - 1; i++)
            {
                tttt += path[i] + "\\";
            }
            return tttt;
        }


        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {



            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;

            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }
         
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
           
        }

        public static Rectangle GetNormalizedRectangle(Rectangle rect)
        {
            int x1 = rect.Left;
            int y1 = rect.Top;
            int x2 = rect.Right;
            int y2 = rect.Bottom;

            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public static bool KillProce(string proc)
        {
            bool kill = false;
            try
            {
                Process[] processes = Process.GetProcessesByName(proc);
                foreach (Process process in processes)
                {
                    process.Kill();
                    kill = true;
                }
            }
            catch { }
            return kill;
        }
        public static Process FindProce(string proc)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(proc);
                foreach (Process process in processes)
                {
                    return process;
                }
            }
            catch { }
            return null;
        }

        public static IntPtr ProcessHwnd = IntPtr.Zero;
        public static bool IsExistProce()
        {

            bool IsExist = false;
            try
            {

                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);

                //Loop   through   the   running   processes   in   with   the   same   name   
                foreach (Process process in processes)
                {
                    //Ignore   the   current   process   
                    if (process.Id != current.Id)
                    {

                        ////Make   sure   that   the   process   is   running   from   the   exe   file.   
                        if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\").Equals(current.MainModule.FileName))
                        {
                            ProcessHwnd = process.Handle;

                            IsExist = true;
                            break;
                            //process.Kill();
                            //Return   the   other   process   instance.   
                            //return process;
                            //return null;
                        }
                    }
                }
            }
            catch { }
            return IsExist;

        }

        public static void SendMessageToApplication(IntPtr destHandle,string strSend)
        {

            strSend += '\0';
            
            API.COPYDATASTRUCT cds = new API.COPYDATASTRUCT();
            cds.dwData = IntPtr.Zero;
            cds.cbData = strSend.Length + 1;
            cds.lpData = Marshal.StringToCoTaskMemAnsi(strSend);
           

            IntPtr iPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cds));
            Marshal.StructureToPtr(cds, iPtr, true);

            API.SendMessage(destHandle, (uint)API.WM_COPYDATA, 0, iPtr);


            // Don't forget to free the allocated memory 
            Marshal.FreeCoTaskMem(cds.lpData);
            Marshal.FreeCoTaskMem(iPtr);


            //SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);
         
        }


        public static void SetStartMenuVisibility(bool show)
        {

            // get taskbar window
            IntPtr taskBarWnd = API.FindWindow("Shell_TrayWnd", null);

            // try it the WinXP way first...
            IntPtr startWnd = API.FindWindowEx(taskBarWnd, IntPtr.Zero, "Button", "Start");
            if (startWnd == IntPtr.Zero)
            {
                // no chance, we need to to it the hard way...
                startWnd = GetVistaStartMenuWnd(taskBarWnd);
            }

            API.ShowWindow(taskBarWnd, show ? API.SW_SHOW : API.SW_HIDE);
            API.ShowWindow(startWnd, show ? API.SW_SHOW : API.SW_HIDE);
        }

        private static IntPtr GetVistaStartMenuWnd(IntPtr taskBarWnd)
        {
            // get process that owns the taskbar window
            int procId;
            API.GetWindowThreadProcessId(taskBarWnd, out procId);

            Process p = Process.GetProcessById(procId);
            if (p != null)
            {
                // enumerate all threads of that process...
                foreach (ProcessThread t in p.Threads)
                {
                    
                    API.EnumThreadWindows(t.Id, MyEnumThreadWindowsProc, IntPtr.Zero);
                }
            }
            return API.vistaStartMenuWnd;
        }
        private static bool MyEnumThreadWindowsProc(IntPtr hWnd, IntPtr lParam)
        {
            StringBuilder buffer = new StringBuilder(256);
            if (API.GetWindowText(hWnd, buffer, buffer.Capacity) > 0)
            {
                if (buffer.ToString() == API.VistaStartMenuCaption)
                {
                    API.vistaStartMenuWnd = hWnd;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region image process

        public static Bitmap rotateImage(Bitmap image, double angle)
        {
            //http://www.codeproject.com/KB/graphics/rotateimage.aspx
            if (image == null)
                throw new ArgumentNullException("image");

            const double pi2 = Math.PI / 2.0;

            // Why can't C# allow these to be const, or at least readonly
            // *sigh*  I'm starting to talk like Christian Graus :omg:
            double oldWidth = (double)image.Width;
            double oldHeight = (double)image.Height;

            // Convert degrees to radians
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;

            // Ensure theta is now [0, 2pi)
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight; // The newWidth/newHeight expressed as ints

            #region Explaination of the calculations
            /*
			 * The trig involved in calculating the new width and height
			 * is fairly simple; the hard part was remembering that when 
			 * PI/2 <= theta <= PI and 3PI/2 <= theta < 2PI the width and 
			 * height are switched.
			 * 
			 * When you rotate a rectangle, r, the bounding box surrounding r
			 * contains for right-triangles of empty space.  Each of the 
			 * triangles hypotenuse's are a known length, either the width or
			 * the height of r.  Because we know the length of the hypotenuse
			 * and we have a known angle of rotation, we can use the trig
			 * function identities to find the length of the other two sides.
			 * 
			 * sine = opposite/hypotenuse
			 * cosine = adjacent/hypotenuse
			 * 
			 * solving for the unknown we get
			 * 
			 * opposite = sine * hypotenuse
			 * adjacent = cosine * hypotenuse
			 * 
			 * Another interesting point about these triangles is that there
			 * are only two different triangles. The proof for which is easy
			 * to see, but its been too long since I've written a proof that
			 * I can't explain it well enough to want to publish it.  
			 * 
			 * Just trust me when I say the triangles formed by the lengths 
			 * width are always the same (for a given theta) and the same 
			 * goes for the height of r.
			 * 
			 * Rather than associate the opposite/adjacent sides with the
			 * width and height of the original bitmap, I'll associate them
			 * based on their position.
			 * 
			 * adjacent/oppositeTop will refer to the triangles making up the 
			 * upper right and lower left corners
			 * 
			 * adjacent/oppositeBottom will refer to the triangles making up 
			 * the upper left and lower right corners
			 * 
			 * The names are based on the right side corners, because thats 
			 * where I did my work on paper (the right side).
			 * 
			 * Now if you draw this out, you will see that the width of the 
			 * bounding box is calculated by adding together adjacentTop and 
			 * oppositeBottom while the height is calculate by adding 
			 * together adjacentBottom and oppositeTop.
			 */
            #endregion

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;

            // We need to calculate the sides of the triangles based
            // on how much rotation is being done to the bitmap.
            //   Refer to the first paragraph in the explaination above for 
            //   reasons why.
            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);

            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                // This array will be used to pass in the three points that 
                // make up the rotated image
                Point[] points;

                /*
                 * The values of opposite/adjacentTop/Bottom are referring to 
                 * fixed locations instead of in relation to the
                 * rotating image so I need to change which values are used
                 * based on the how much the image is rotating.
                 * 
                 * For each point, one of the coordinates will always be 0, 
                 * nWidth, or nHeight.  This because the Bitmap we are drawing on
                 * is the bounding box for the rotated bitmap.  If both of the 
                 * corrdinates for any of the given points wasn't in the set above
                 * then the bitmap we are drawing on WOULDN'T be the bounding box
                 * as required.
                 */
                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[] { 
											 new Point( (int) oppositeBottom, 0 ), 
											 new Point( nWidth, (int) oppositeTop ),
											 new Point( 0, (int) adjacentBottom )
										 };

                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[] { 
											 new Point( nWidth, (int) oppositeTop ),
											 new Point( (int) adjacentTop, nHeight ),
											 new Point( (int) oppositeBottom, 0 )						 
										 };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[] { 
											 new Point( (int) adjacentTop, nHeight ), 
											 new Point( 0, (int) adjacentBottom ),
											 new Point( nWidth, (int) oppositeTop )
										 };
                }
                else
                {
                    points = new Point[] { 
											 new Point( 0, (int) adjacentBottom ), 
											 new Point( (int) oppositeBottom, 0 ),
											 new Point( (int) adjacentTop, nHeight )		
										 };
                }

                g.DrawImage(image, points);
            }

            return rotatedBmp;
        }

        public static Bitmap myGetThumbnailImage2(Image oImg, int ThumbWidth, int ThumbHeight)
        {
            int intwidth = ThumbWidth;
            int intheight = ThumbHeight;
            //System.Drawing.Image oImg = System.Drawing.Image.FromFile(SourceFile);

            //小圖

            //int intwidth, intheight;

            //if (oImg.Width > oImg.Height)
            //{

            //    if (oImg.Width > ThumbWidth)
            //    {

            //        intwidth = ThumbWidth;

            //        intheight = (oImg.Height * ThumbWidth) / oImg.Width;

            //    }

            //    else
            //    {

            //        intwidth = oImg.Width;

            //        intheight = oImg.Height;

            //    }

            //}

            //else
            //{

            //    if (oImg.Height > ThumbHeight)
            //    {

            //        intwidth = (oImg.Width * ThumbHeight) / oImg.Height; intheight = ThumbHeight;

            //    }

            //    else
            //    {

            //        intwidth = oImg.Width; intheight = oImg.Height;

            //    }

            //}

            //构造一个指定宽高的Bitmap 

            Bitmap bitmay = new Bitmap(intwidth, intheight, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(bitmay);
            g.Clear(Color.White);


            //用指定的颜色填充Bitmap 

            //g.Clear(myColor);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //开始画图 
            g.DrawImage(oImg, new Rectangle(0, 0, intwidth, intheight), new Rectangle(0, 0, oImg.Width, oImg.Height), GraphicsUnit.Pixel);

            //bitmay.Save(strSavePathFile, System.Drawing.Imaging.ImageFormat.Jpeg);


            g.Dispose();

            //bitmay.Dispose();

            //oImg.Dispose();
            return bitmay;



        }

        public static Bitmap CopyBitmapFormClipboard()
        {
            Bitmap clip = null;
            object obj = Clipboard.GetData(DataFormats.Bitmap);
            if (obj != null)
            {
                clip = (Bitmap)obj;
            }
            return clip;
        }

        public static Bitmap PrintScreen()
        {
            Bitmap result = null;

            uint intReturn = 0;
            API.INPUT structInput;
            structInput = new API.INPUT();
            structInput.type = (int)1;
            structInput.ki.wScan = 0;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 0;
            structInput.ki.dwExtraInfo = IntPtr.Zero;

            ////Press Alt Key
            //structInput.ki.wVk = (ushort)NativeWIN32.VK.MENU;
            //intReturn = NativeWIN32.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));

            // Key down the actual key-code
            structInput.ki.wVk = (ushort)API.VK.SNAPSHOT;//vk;
            intReturn = API.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));

            //// Key up the actual key-code
            //structInput.ki.dwFlags = NativeWIN32.KEYEVENTF_KEYUP;
            //structInput.ki.wVk = (ushort)NativeWIN32.VK.SNAPSHOT;//vk;
            //intReturn = NativeWIN32.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));

            ////Keyup Alt
            //structInput.ki.dwFlags = NativeWIN32.KEYEVENTF_KEYUP;
            //structInput.ki.wVk = (ushort)NativeWIN32.VK.MENU;
            //intReturn = NativeWIN32.SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
            Application.DoEvents();
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Bitmap))
            {
                result = (Bitmap)data.GetData(DataFormats.Bitmap, true);
            }

            return result;
        }

        public static Bitmap CaptureControl(IntPtr ptr,Rectangle bounds) {
            Bitmap desktopBMP = null;
            try
            {
                IntPtr handle = ptr;
                IntPtr hdcSrc = API.GetWindowDC(handle);
                int width = bounds.Width;
                int height = bounds.Height;

                // create a device context we can copy to
                IntPtr hdcDest = API.CreateCompatibleDC(hdcSrc);
                // create a bitmap we can copy it to,
                // using GetDeviceCaps to get the width/height
                IntPtr hBitmap = API.CreateCompatibleBitmap(hdcSrc, width, height);
                // select the bitmap object
                IntPtr hOld = API.SelectObject(hdcDest, hBitmap);
                // bitblt over

                API.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, bounds.Left, bounds.Top, CopyPixelOperation.SourceCopy);
                // restore selection
                API.SelectObject(hdcDest, hOld);
                // clean up 
                API.DeleteDC(hdcDest);
                API.ReleaseDC(handle, hdcSrc);
                // get a .NET image object for it
                Image img = Image.FromHbitmap(hBitmap);
                // free up the Bitmap object
                API.DeleteObject(hBitmap);
                desktopBMP = (Bitmap)img;
                img = null;

            }
            catch (Exception ee)
            {
                MessageBox.Show("desktopBMP=" + ee.ToString());
            }

            return desktopBMP;
        }

        public static Bitmap CaptureWindow(bool alpha, Rectangle bounds)
        {
            Bitmap desktopBMP = null;
            if (alpha)
            {
                try
                {
                    IntPtr handle = API.GetDesktopWindow();
                    IntPtr hdcSrc = API.GetWindowDC(handle);
                    int width = bounds.Width;
                    int height = bounds.Height;

                    // create a device context we can copy to
                    IntPtr hdcDest = API.CreateCompatibleDC(hdcSrc);
                    // create a bitmap we can copy it to,
                    // using GetDeviceCaps to get the width/height
                    IntPtr hBitmap = API.CreateCompatibleBitmap(hdcSrc, width, height);
                    // select the bitmap object
                    IntPtr hOld = API.SelectObject(hdcDest, hBitmap);
                    // bitblt over

                    API.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, bounds.Left, bounds.Top, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
                    // restore selection
                    API.SelectObject(hdcDest, hOld);
                    // clean up 
                    API.DeleteDC(hdcDest);
                    API.ReleaseDC(handle, hdcSrc);
                    // get a .NET image object for it
                    Image img = Image.FromHbitmap(hBitmap);
                    // free up the Bitmap object
                    API.DeleteObject(hBitmap);
                    desktopBMP = (Bitmap)img;
                    img = null;

                }
                catch (Exception ee)
                {
                    MessageBox.Show("desktopBMP=" + ee.ToString());
                }
            }
            else
            {
                Graphics g;


                desktopBMP = new Bitmap(bounds.Width, bounds.Height);
                g = Graphics.FromImage(desktopBMP);

                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                g.Dispose();
            }

            return desktopBMP;
        }

        public static Bitmap CaptureWindow(bool alpha)
        {
            Rectangle bounds = Utilities.ScreenArea;
            //System.Windows.Forms.Screen.GetBounds(System.Windows.Forms.Screen.GetBounds(Point.Empty))
            return CaptureWindow(alpha, bounds);
        }

        #endregion

        #region Register
        public static string ReadRegisterKey(string type, string SubKey, string KeyName)
        {
            // Opening the registry key
            // Setting
            RegistryKey rk = null;
            if (type.Equals("HKLM"))
            {
                rk = Registry.LocalMachine;
            }
            else if (type.Equals("HKCU"))
            {
                rk = Registry.CurrentUser;
            }
            else if (type.Equals("HKCR"))
            {
                rk = Registry.ClassesRoot;
            }

            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(SubKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }


        public static bool WriteRegisterKey(string type, string SubKey, string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = null;
                if (type.Equals("HKLM"))
                {
                    rk = Registry.LocalMachine;
                }
                else if (type.Equals("HKCU"))
                {
                    rk = Registry.CurrentUser;
                }
                else if (type.Equals("HKCR"))
                {
                    rk = Registry.ClassesRoot;
                }

                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(SubKey);
                // Save the value
                sk1.SetValue(KeyName, Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        #endregion

        #region Device

        public static String GetClassNameFromGuid(Guid guid)
        {
            StringBuilder strClassName = new StringBuilder(0);
            Int32 iRequiredSize = 0;
            Int32 iSize = 0;
            Int32 iRet = API.SetupDiClassNameFromGuid(ref guid, strClassName, iSize, ref iRequiredSize);
            strClassName = new StringBuilder(iRequiredSize);
            iSize = iRequiredSize;
            iRet = API.SetupDiClassNameFromGuid(ref guid, strClassName, iSize, ref iRequiredSize);
            if (iRet == 1)
            {
                return strClassName.ToString();
            }

            return String.Empty;
        }

        public static String GetClassDescriptionFromGuid(Guid guid)
        {
            StringBuilder strClassDesc = new StringBuilder(0);
            Int32 iRequiredSize = 0;
            Int32 iSize = 0;
            Int32 iRet = API.SetupDiGetClassDescription(ref guid, strClassDesc, iSize, ref iRequiredSize);
            strClassDesc = new StringBuilder(iRequiredSize);
            iSize = iRequiredSize;
            iRet = API.SetupDiGetClassDescription(ref guid, strClassDesc, iSize, ref iRequiredSize);
            if (iRet == 1)
            {
                return strClassDesc.ToString();
            }

            return String.Empty;
        }

        #endregion

        #region other

        public static string EncodeString(string toEncode)
        {
            try
            {
                byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(toEncode);
                return Convert.ToBase64String(toEncodeAsBytes);
            }
            catch (Exception ex)
            {
                //do your error handling here
            }

            return "";
        }

        public static string DecodeString(string toDecrypt)
        {
            try
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(toDecrypt.Replace(" ", "+"));
                return Encoding.UTF8.GetString(encodedDataAsBytes);
            }
            catch (Exception ex)
            {
                //do your error handling here
            }

            return "";
        }


        public static string MD52(string pwd)
        {
            

            byte[] Original = Encoding.Default.GetBytes(pwd); //將字串來源轉為Byte[] 
            System.Security.Cryptography.MD5 s1 = System.Security.Cryptography.MD5.Create(); //使用MD5 
            byte[] Change = s1.ComputeHash(Original);//進行加密 
            return Convert.ToBase64String(Change);//將加密後的字串從byte[]轉回string
        }


        public static string MD5(string pwd)
        {


            byte[] b = System.Text.Encoding.Default.GetBytes(pwd);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;


            //byte[] Original = Encoding.Default.GetBytes(pwd); //將字串來源轉為Byte[] 
            //System.Security.Cryptography.MD5 s1 = System.Security.Cryptography.MD5.Create(); //使用MD5 
            //byte[] Change = s1.ComputeHash(Original);//進行加密 
            //return Convert.ToBase64String(Change);//將加密後的字串從byte[]轉回string
        }


        public static void PlaySound(string filepath)
        {
            if (filepath != null && filepath.Length > 0)
            {
                try
                {
                    API.mciSendString("close \"" + filepath + "\"", null, 0, IntPtr.Zero);
                    API.mciSendString("open \"" + filepath + "\"", null, 0, IntPtr.Zero);
                    API.mciSendString("play \"" + filepath + "\"", null, 0, IntPtr.Zero);
                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.ToString());
                }
            }
        }


        public static void StopSound(string filepath)
        {
            try
            {
                API.mciSendString("close \"" + filepath + "\"", null, 0, IntPtr.Zero);
            }
            catch { }
        }

     
        public static void WriteToLog(string msg)
        {

            string dir = Application.StartupPath + "\\log";
            string filename = dir + "\\log_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            if (System.IO.Directory.Exists(dir) == false)
            {
                System.IO.Directory.CreateDirectory(dir);
            }


            StreamWriter filewriter = null;
            try
            {
                filewriter = new StreamWriter(filename, true);
                filewriter.WriteLine("[" + DateTime.Now + "]" + Environment.NewLine + msg);
                filewriter.WriteLine();
                filewriter.WriteLine();
                filewriter.Close();
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.ToString());
            }
            filewriter = null;
        }

        public static void CheckDeleteDirectory(string cpath,bool del)
        {
            //(1).先刪掉目錄
            try
            {
                if (del && System.IO.Directory.Exists(cpath))
                {
                    System.IO.Directory.Delete(cpath, true);
                }
            }
            catch(Exception ee)
            {
                //MessageBox.Show("CheckDeleteDirectory1="+ee.ToString());
            }

            try
            {
                if (!System.IO.Directory.Exists(cpath))
                {
                    System.IO.Directory.CreateDirectory(cpath);
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show("CheckDeleteDirectory2=" + ee.ToString());
            }
        }


         // Copy directory structure recursively
        public static void copyDirectory(string Src,string Dst)
        {
            String[] Files;

            if(Dst[Dst.Length-1]!=Path.DirectorySeparatorChar)  Dst+=Path.DirectorySeparatorChar;
            if(!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);

            Files=Directory.GetFileSystemEntries(Src);
            //Files = Directory.GetFiles(Src);

            foreach(string src in Files)
            {
                if (src.IndexOf(".svn") != -1) continue;
                
                // Sub directories
                if (Directory.Exists(src))
                {
                    copyDirectory(src, Dst + Path.GetFileName(src));
                }
                else
                {
                    try
                    {
                        string dest = Dst + Path.GetFileName(src);
                        File.Copy(src, dest, true);
                        //File.SetAttributes(dest, FileAttributes.Normal);
                        Debug.WriteLine2(src + " to " + dest);

                    }catch(Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
                    }
                }
                
            }

        }

        public static void MoveDirectory(string source, string target) 
        {
            Stack stack = new Stack();
            stack.Push(new string[] { source, target }); 
            
            while (stack.Count > 0) 
            { 
                string[] arr = (string[])stack.Pop();

                try
                {
                    if (Directory.Exists(arr[1]) == false)
                    {
                        Directory.CreateDirectory(arr[1]);
                    }
                }
                catch { }
 
                foreach (string file in Directory.GetFiles(arr[0], "*.*")) 
                {
                    string targetFile = Path.Combine(arr[1], Path.GetFileName(file));

                    if (File.Exists(targetFile))
                    {
                        try
                        {
                            File.Delete(targetFile);
                        }
                        catch {
                            Console.WriteLine("Error=File.Delete(" + targetFile + ");");
                        }
                    }

                    try
                    {
                        //File.Move(file, targetFile);
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(file, targetFile, true);
                    }
                    catch { 
                        Console.WriteLine("Error=File.Move("+file+","+ targetFile+");");
                    }
                } 
                
                foreach (string folder in Directory.GetDirectories(arr[0])) 
                {
                    stack.Push(new string[]{
                        folder, 
                        Path.Combine(arr[1], Path.GetFileName(folder))
                    });
                }
            }
            try
            {
                Directory.Delete(source, true);
            }
            catch {
                Console.WriteLine("Error=Directory.Delete("+source + ", true);");
            }
        } 


        public static void CancelReadonly(string path) 
        {
            if (Directory.Exists(path) == false) return;
            /*
            if (Directory.Exists(path)==false)
            {
                File.SetAttributes(path, FileAttributes.Normal);  
                return;
            }

            foreach (string Folder in Directory.GetDirectories(path)) 
            {
                CancelReadonly(Folder);       
            }
            foreach (string file in Directory.GetFiles(path))    
            {
                FileInfo fi = new FileInfo(file);
                File.SetAttributes(file, FileAttributes.Normal);  
                //File.Delete(file);       
            }         
            //Directory.Delete(pFolderPath);
            */

          

            foreach (string subDir in Directory.GetDirectories(path)) 
            {
                CancelReadonly(subDir); 
            }

            //if (Directory.Exists(path))
            {
              
                //MessageBox.Show(path);
                foreach (string file in Directory.GetFiles(path))
                {
                    //MessageBox.Show(file);
                    try
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                    }
                    catch {
                        Debug.WriteLine2("CancelReadonly-->Error=" + file);
                    }
                    //file.Delete(); 
                }
            }
        }


        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        } 




        public static bool InstallPrinter()
        {
            bool yn = false;


            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (printer.Trim().Equals("NXPrinter Capture")) {
                    yn = true;
                    break;
                }
            }

            return yn;
        }

        public static bool InstallNXTudio() 
        {

            string InstallDir = null;
            try
            {
                InstallDir = Utilities.ReadRegisterKey("HKLM", @"Software\NXTudy\", "InstallDir");
            }
            catch(Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }

            return (InstallDir!=null);
        }

        public static int InstallWord()
        {
            int docversion = -1;

            RegistryKey regkey;
            string[] regkeynames = null;

            if (docversion == -1)
            {
                //Office 2003
                regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\11.0\Word");
                if (regkey != null)
                {
                    regkeynames = regkey.GetSubKeyNames();
                    if (regkeynames != null && regkeynames.Length > 0)
                    {
                        docversion = 2003;
                    }
                }
            }

            if (docversion == -1)
            {
                //Office 2007
                regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\12.0\Word");
                if (regkey != null)
                {
                    regkeynames = regkey.GetSubKeyNames();
                    if (regkeynames != null && regkeynames.Length > 0)
                    {
                        docversion = 2007;
                    }
                }
            }

            return docversion;
        }

        public static int InstallExcel()
        {
            int docversion = -1;

            RegistryKey regkey;
            string[] regkeynames = null;

            if (docversion == -1)
            {
                //Office 2003
                regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\11.0\Excel");
                if (regkey != null)
                {
                    regkeynames = regkey.GetSubKeyNames();
                    if (regkeynames != null && regkeynames.Length > 0)
                    {
                        docversion = 2003;
                    }
                }
            }

            if (docversion == -1)
            {
                //Office 2007
                regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\12.0\Excel");
                if (regkey != null)
                {
                    regkeynames = regkey.GetSubKeyNames();
                    if (regkeynames != null && regkeynames.Length > 0)
                    {
                        docversion = 2007;
                    }
                }
            }

            return docversion;
        }

        public static int InstallPowerPoint()
        {
            int pptversion = -1;

            RegistryKey regkey;
            string[] regkeynames = null;

            if (pptversion == -1)
            {
                //Office 2003
                regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\11.0\PowerPoint");
                if (regkey != null)
                {
                    regkeynames = regkey.GetSubKeyNames();
                    if (regkeynames != null && regkeynames.Length > 0)
                    {
                        pptversion = 2003;
                    }
                }
            }

            if (pptversion == -1)
            {
                //Office 2007
                regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\12.0\PowerPoint");
                if (regkey != null)
                {
                    regkeynames = regkey.GetSubKeyNames();
                    if (regkeynames != null && regkeynames.Length > 0)
                    {
                        pptversion = 2007;
                    }
                }
            }

            return pptversion;
        }

        public static int guidcount = 0;
        public static string NewGuid()
        {
            guidcount++;
            string result = DateTime.Now.Ticks.ToString();
            Random myRandom = new Random(unchecked((int)DateTime.Now.Ticks));
            long intRandom = myRandom.Next(1, 1000);

            result += intRandom;
            result += guidcount;
            return result;
        }

        #endregion


        public static string RegularPath(string path) 
        {
            return path.Replace("/", "\\").Replace("\\\\","\\");
        }


        public static void OnScreenKeyboard() 
        {


            string windir = Environment.GetEnvironmentVariable("WINDIR");
            string osk = null;

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "sysnative"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "system32"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
            {
                osk = "osk.exe";
            }

            Process.Start(osk);

        
        }


   
        public static int ParseRGB(Color color)
        {
            return (int)(
                ((uint)color.B << 24) | 
                 (ushort)(((ushort)color.G << 16) | ((ushort)color.R << 8)));
        }



        #region Font Calculate

        public static Bitmap AppropriateSize(Graphics mg, Color c, string str, Font font)
        {
            Bitmap bmp = null;

            if (mg != null)
            {

                Size size = Size.Empty;

                //if (str.Length == 1)
                //{

                //    SizeF strSize1 = mg.MeasureString("一", font);// 先取得一個字的大小
                //    SizeF strSize2 = mg.MeasureString("一二", font);// 接著取得兩個字的大小
                //    int charWidth = (int)(strSize2.Width - strSize1.Width);// 相減得字元寬度
                //    int charHeight = (int)(strSize2.Height - strSize1.Height);// 相減得字元寬度
                //    size = new Size(charWidth, charHeight);
                //    bmp = new Bitmap((int)(size.Width), (int)(size.Height));
                //}
                //else
                //{
                size = Size.Ceiling(mg.MeasureString(str, font));
                bmp = new Bitmap((int)(size.Width), (int)(size.Height));
                //}

                Graphics g = Graphics.FromImage(bmp);

                //設置高質量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //設置高質量,低速度呈現平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                PointF ptf = new PointF((bmp.Width - size.Width) / 2, (bmp.Height - size.Height) / 2);
                ptf.Y += 4;

                //g.DrawString(str, font, Brushes.White, new PointF(ptf.X - 1, ptf.Y));
                //g.DrawString(str, font, Brushes.White, new PointF(ptf.X + 1, ptf.Y));
                //g.DrawString(str, font, Brushes.White, new PointF(ptf.X, ptf.Y - 1));
                //g.DrawString(str, font, Brushes.White, new PointF(ptf.X, ptf.Y + 1));
                g.DrawString(str, font, new SolidBrush(c), ptf);

                g.Dispose();

            }

            //return (g.DpiX * font.SizeInPoints/72);//GetHeight



            return bmp;
        }

        public static void AppropriateString(string str, Bitmap bmp)
        {

            Graphics g = Graphics.FromImage(bmp);

            //設置高質量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //設置高質量,低速度呈現平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //size
            SizeF sizef = SizeF.Empty;
            //Font f = new Font(FontFamily.GenericSerif, 15);
            Font f = new Font("標楷體", 15);
            Font f2 = Utilities.AppropriateFont(g, 10, 50, bmp.Size, str, f, out sizef);//計算最適高度
            PointF ptf = new PointF((bmp.Width - sizef.Width) / 2, (bmp.Height - sizef.Height) / 2);
            ptf.Y += 4;

            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X - 1, ptf.Y));
            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X + 1, ptf.Y));
            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X, ptf.Y - 1));
            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X, ptf.Y + 1));
            g.DrawString(str, f2, Brushes.White, ptf);

            g.Dispose();

        }

        public static Font AppropriateFontSize(Graphics g, string str, Color c, Size size)
        {

            //Graphics g = Graphics.FromImage(bmp);

            ////設置高質量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            ////設置高質量,低速度呈現平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //size
            SizeF sizef = SizeF.Empty;
            //Font f = new Font(FontFamily.GenericSerif, 15);
            Font f = new Font("標楷體", 15);
            Font f2 = Utilities.AppropriateFont(g, 10, 500, size, str, f, out sizef);//計算最適高度
            //PointF ptf = new PointF((bmp.Width - sizef.Width) / 2, (bmp.Height - sizef.Height) / 2);
            //ptf.Y += 4;

            //g.DrawString(str, f2, Brushes.White, new PointF(ptf.X - 1, ptf.Y));
            //g.DrawString(str, f2, Brushes.White, new PointF(ptf.X + 1, ptf.Y));
            //g.DrawString(str, f2, Brushes.White, new PointF(ptf.X, ptf.Y - 1));
            //g.DrawString(str, f2, Brushes.White, new PointF(ptf.X, ptf.Y + 1));
            //g.DrawString(str, f2, new SolidBrush(c), ptf);

            //g.Dispose();

            return f2;
        }

        public static Font AppropriateString(string str, Color c, Bitmap bmp)
        {

            Graphics g = Graphics.FromImage(bmp);

            //設置高質量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //設置高質量,低速度呈現平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //size
            SizeF sizef = SizeF.Empty;
            //Font f = new Font(FontFamily.GenericSerif, 15);
            Font f = new Font("標楷體", 15);
            Font f2 = Utilities.AppropriateFont(g, 10, 500, bmp.Size, str, f, out sizef);//計算最適高度
            PointF ptf = new PointF((bmp.Width - sizef.Width) / 2, (bmp.Height - sizef.Height) / 2);
            ptf.Y += 4;

            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X - 1, ptf.Y));
            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X + 1, ptf.Y));
            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X, ptf.Y - 1));
            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X, ptf.Y + 1));
            g.DrawString(str, f2, new SolidBrush(c), ptf);

            g.Dispose();

            return f2;
        }

        public static Font AppropriateFont(Graphics g, float minFontSize, float maxFontSize, Size layoutSize, string s, Font f, out SizeF extent)
        {
            /*參考資料
             * 計算字型最適高度方法
             * http://blog.paranoidferret.com/index.php/2007/10/10/csharp-tutorial-font-scaling/
             */

            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            extent = g.MeasureString(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            f = new Font(f.FontFamily, newSize, f.Style);
            extent = g.MeasureString(s, f);

            return f;
        }

        public static void AppropriateStringEng(string str, Bitmap bmp)
        {

            Graphics g = Graphics.FromImage(bmp);

            //設置高質量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //設置高質量,低速度呈現平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //size
            SizeF sizef = SizeF.Empty;
            //Font f = new Font(FontFamily.GenericSerif, 15);
            Font f = new Font("Arial Rounded MD Bold", 15);
            Font f2 = Utilities.AppropriateFont(g, 10, 50, bmp.Size, str, f, out sizef);//計算最適高度
            PointF ptf = new PointF((bmp.Width - sizef.Width) / 2, (bmp.Height - sizef.Height) / 2);
            ptf.Y += 4;

            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X - 1, ptf.Y));
            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X + 1, ptf.Y));
            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X, ptf.Y - 1));
            g.DrawString(str, f2, Brushes.Black, new PointF(ptf.X, ptf.Y + 1));
            g.DrawString(str, f2, Brushes.White, ptf);

            g.Dispose();

        }

        public static Font AppropriateString(string str, string family, Color c, Bitmap bmp, Point point)
        {

            Graphics g = Graphics.FromImage(bmp);

            //設置高質量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //設置高質量,低速度呈現平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //size
            SizeF sizef = SizeF.Empty;
            //Font f = new Font(FontFamily.GenericSerif, 15);

            if (family == null) family = "標楷體";
            Font f = new Font(family, 15);
            Font f2 = Utilities.AppropriateFont(g, 10, 500, bmp.Size, str, f, out sizef);//計算最適高度


            PointF ptf = PointF.Empty;


            if (point.X == -1 && point.Y == -1)
            {
                ptf = new PointF((bmp.Width - sizef.Width) / 2, (bmp.Height - sizef.Height) / 2);
                ptf.Y += 4;
            }
            else
            {
                ptf = point;
                ptf.Y += 4;
            }

            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X - 1, ptf.Y));
            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X + 1, ptf.Y));
            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X, ptf.Y - 1));
            g.DrawString(str, f2, Brushes.White, new PointF(ptf.X, ptf.Y + 1));
            g.DrawString(str, f2, new SolidBrush(c), ptf);

            g.Dispose();

            return f2;
        }

        #endregion


        public static void GetAllFiles(Dictionary<string, string> fileDict, string path, string zipRoot)
        {
            //zipRoot=Board1
            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                // Board1\aaaa.dat
                string r = zipRoot + "\\" + Path.GetFileName(file);
                //Console.WriteLine(file);
                fileDict.Add(file, r);
                //list.Add(file);
            }

            foreach (string dir in System.IO.Directory.GetDirectories(path))
            {
                GetAllFiles(fileDict, dir, zipRoot + "\\" + Path.GetFileName(dir));
            }


        }
   

        public static void DrawStringShodow(Graphics g ,string title,Font f ,Brush b1,Brush b2,PointF ptf)
        {
            g.DrawString(title, f, b2, new PointF(ptf.X - 1, ptf.Y));//左
            g.DrawString(title, f, b2, new PointF(ptf.X + 1, ptf.Y));//右
            g.DrawString(title, f, b2, new PointF(ptf.X, ptf.Y - 1));//上
            g.DrawString(title, f, b2, new PointF(ptf.X, ptf.Y + 1));//下

            g.DrawString(title, f, b2, new PointF(ptf.X - 1, ptf.Y-1));//左上
            g.DrawString(title, f, b2, new PointF(ptf.X + 1, ptf.Y-1));//右上
            g.DrawString(title, f, b2, new PointF(ptf.X - 1, ptf.Y + 1));//左下
            g.DrawString(title, f, b2, new PointF(ptf.X + 1, ptf.Y + 1));//右下

            g.DrawString(title, f, b1, ptf);

        }

        public static void DrawShodow(Graphics g,PointF pt, string title,Font font)
        {
            //http://www.codeproject.com/KB/GDI-plus/OutlineText.aspx?msg=3248023

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;



            //FontFamily fontFamily = new FontFamily("微軟正黑體");
            StringFormat strformat = new StringFormat();
            //string szbuf = "廖啟宏";

            GraphicsPath path = new GraphicsPath();
            path.AddString(title, font.FontFamily, (int)font.Style,font.Size, pt, strformat);

            for (int i = 1; i < 8; ++i)
            {

                Pen pen = new Pen(Color.FromArgb(32, Color.Black), i);
                pen.LineJoin = LineJoin.Round;
                g.DrawPath(pen, path);
                pen.Dispose();
            }

            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
            g.FillPath(brush, path);

            //brushWhite.Dispose();
            //fontFamily.Dispose();
            path.Dispose();
            brush.Dispose();
         


            //bmp.Save(@"C:\Documents and Settings\VM XP\桌面\abc.png", System.Drawing.Imaging.ImageFormat.Png);


        }

        public static void SetWindowBounds(Form frm,Rectangle rect)
        {
            //将MaximumSize设置更大
            if (frm.MaximumSize.Width < rect.Width)
                frm.MaximumSize =
                    new Size(rect.Width, frm.MaximumSize.Height);
            if (frm.MaximumSize.Height < rect.Height)
                frm.MaximumSize =
                    new Size(frm.MaximumSize.Width, rect.Height);

            
            API.MoveWindow(frm.Handle, rect.X, rect.Y,
                rect.Width, rect.Height, true);
            //frm.UpdateBounds();
        }

        #region Image Process

        #region 暫時隱藏好亂

        public static Bitmap GrayScaleImage(Bitmap bmpimg)
        {


            BitmapData data = bmpimg.LockBits(new Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        if (ptr[3] == 0)
                        {
                            //ptr[0] = 100;
                        }
                        else
                        {
                            int a = a = (ptr[0] + ptr[1] + ptr[2]) / 3;
                            ptr[0] = (byte)a;
                            ptr[1] = (byte)a;
                            ptr[2] = (byte)a;
                        }
                        ptr += 4;
                    }
                    ptr += remain;
                }
            }
            bmpimg.UnlockBits(data);

            return bmpimg;
        }

        public static string ColorMapping(Color color)
        {
            string title = color.Name.ToUpper();
            if (color.IsKnownColor)
            {
                title = color.ToKnownColor().ToString();
            }


            return title;
        }

        public static byte[] ReadImageToByte(Bitmap bmp)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();

                //Save bitmap into memory stream.
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch
            {
                throw new Exception(string.Format("Error opening/reading image"));
            }


            return ms.ToArray();
        }

        public static byte[] ReadImageToByte(string strImgPath)
        {
            FileStream fs = new FileStream(strImgPath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)br.BaseStream.Length);
            fs = null;
            br = null;
            return bytes;
        }

        public static Bitmap MergeImage(Bitmap back, Bitmap fore)
        {
            Graphics g = Graphics.FromImage(back);
            g.DrawImage(fore, new Rectangle(0, 0, back.Width, back.Height), new Rectangle(0, 0, fore.Width, fore.Height), GraphicsUnit.Pixel);
            g.Dispose();

            return back;
        }

        public static bool FindEmtpyColor(Bitmap bmp)
        {
            bool find = false;
            Bitmap bmpimg = bmp;
            BitmapData data = bmpimg.LockBits(new Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        if (ptr[3] != 0)
                        {
                            find = true;
                            break;
                            //ptr[0] = 100;
                        }
                        //else
                        //{
                        //    ptr[0] = color.B;
                        //    ptr[1] = color.G;
                        //    ptr[2] = color.R;
                        //}
                        ptr += 4;
                    }
                    ptr += remain;
                }
            }
            bmpimg.UnlockBits(data);

            return find;
        }

        public static void MergeColorImage(Bitmap back, Bitmap fore, Color color)
        {
            Bitmap bmpimg = back;
            BitmapData data = bmpimg.LockBits(new Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        if (ptr[3] == 0)
                        {
                            //ptr[0] = 100;
                        }
                        else
                        {
                            ptr[0] = color.B;
                            ptr[1] = color.G;
                            ptr[2] = color.R;
                        }
                        ptr += 4;
                    }
                    ptr += remain;
                }
            }
            bmpimg.UnlockBits(data);

            Graphics g = Graphics.FromImage(back);
            g.DrawImage(fore, Point.Empty);
            g.Dispose();
        }

        private static byte[] GradientRectangle(Bitmap m_store, int FlipHeight)
        {
            int bytes = m_store.Width * FlipHeight * 4;
            byte[] m_argbsource = new byte[bytes];
            Rectangle m_rectgradient = new Rectangle(0, 0, m_store.Width, FlipHeight);
            System.Drawing.Drawing2D.LinearGradientBrush m_brushgradient;
            m_brushgradient = new System.Drawing.Drawing2D.LinearGradientBrush(m_rectgradient, Color.FromArgb(150, Color.Black), Color.FromArgb(0, Color.White), 90F);
            Bitmap m_bmpgradient = new Bitmap(m_store.Width, FlipHeight);
            using (Graphics g = Graphics.FromImage(m_bmpgradient))
            {
                g.FillRectangle(m_brushgradient, m_rectgradient);
            }
            BitmapData m_datasource = m_bmpgradient.LockBits(m_rectgradient, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr m_ptrsource = m_datasource.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(m_ptrsource, m_argbsource, 0, bytes);

            return m_argbsource;
        }


      

        public static Bitmap MirrorImage(Bitmap m_store)
        {
            //http://www.codeproject.com/KB/graphics/ImageFlip.aspx

            //	Calculate new height
            int m_divider = 2;
            int m_heightflip = (m_store.Height / m_divider);//m_divider=1
            int m_height = (m_store.Height + m_heightflip);

            //	New image is self height plus a piece
            Bitmap m_double = new Bitmap(m_store.Width, m_height);

            Rectangle m_rectflip = new Rectangle(0, m_store.Height, m_store.Width, m_heightflip);

            using (Graphics g = Graphics.FromImage(m_double))
            {
                Image m_img = (Image)m_store.Clone();
                //	Draw normal image
                g.DrawImageUnscaled(m_store, 0, 0);
                //	Flip the clone
                m_img.RotateFlip(RotateFlipType.Rotate180FlipX);
                //	Draw the flipped clone, cropping at flip height
                g.DrawImage(m_img, 0, m_store.Height, new Rectangle(0, 0, m_store.Width, m_heightflip), GraphicsUnit.Pixel);
                //	Remove this
                m_img.Dispose();

                //////////////////////////////////////////////////////////////////////////
                //	Apply transparent mask
                BitmapData m_datadestin = m_double.LockBits(m_rectflip, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                //	Init the source and destination buffers
                int bytes = (m_store.Width * m_heightflip * 4);
                byte[] m_argbdestin = new byte[bytes];
                byte[] m_argbsource = GradientRectangle(m_store,m_heightflip);

                //	Point at destination, instead UNSAFE block
                IntPtr m_ptrdestin = m_datadestin.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(m_ptrdestin, m_argbdestin, 0, bytes);

                // Set every transparency value to mask.  
                for (int counter = 3; counter < m_argbsource.Length; counter += 4)
                    m_argbdestin[counter] = m_argbsource[counter];

                // Copy the ARGB values back to the bitmap
                System.Runtime.InteropServices.Marshal.Copy(m_argbdestin, 0, m_ptrdestin, bytes);

                // Unlock the bits.
                m_double.UnlockBits(m_datadestin);
                //////////////////////////////////////////////////////////////////////////

            }
            //	Reassign modified image to paintable one.
            return m_double;
        }

        public static Bitmap InvertImage(Bitmap bmpimg)
        {
            BitmapData data = bmpimg.LockBits(new Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        if (ptr[3] == 0)
                        {
                            //ptr[0] = 100;
                        }
                        else
                        {
                            ptr[0] = (byte)(255 - ptr[0]);
                            ptr[1] = (byte)(255 - ptr[1]);
                            ptr[2] = (byte)(255 - ptr[2]);
                        }
                        ptr += 4;
                    }
                    ptr += remain;
                }
            }
            bmpimg.UnlockBits(data);

            return bmpimg;
        }

        public static unsafe RectangleF EmtpyBitmap(Bitmap bmp, RectangleF rect)
        {
            if (rect == Rectangle.Empty) return RectangleF.Empty;

            int X = 0;
            int Y = 0;
            int W = 0;
            int H = 0;
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            try
            {

                byte* ptr = (byte*)(void*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                X = (int)Math.Floor(rect.X);
                Y = (int)Math.Floor(rect.Y);
                W = (int)Math.Ceiling(rect.Width);
                H = (int)Math.Ceiling(rect.Height);


                //initialize ptr
                ptr += ((data.Stride * Y) + (X * 4));
                //int value = *((int*)(ptr));

                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        ptr[3] = 0;
                        ptr += 4;
                    }
                    ptr += ((bmp.Width - W) * 4);
                    ptr += remain;
                }


            }
            catch { }
            bmp.UnlockBits(data);

            return new RectangleF(X, Y, W, H);
        }

        public static void CopyBitmap(Bitmap bmp, Bitmap mask, RectangleF dest, RectangleF src)
        {
            Graphics g = Graphics.FromImage(bmp);

            //設置高質量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //設置高質量,低速度呈現平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //圖象複製
            g.DrawImage(mask, dest, src, GraphicsUnit.Pixel);

            g.Dispose();
        }

        public static void CopyBitmap(Bitmap bmp, Bitmap mask, RectangleF dest)
        {
            RectangleF src = new RectangleF(0, 0, mask.Width, mask.Height);
            Graphics g = Graphics.FromImage(bmp);

            //設置高質量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //設置高質量,低速度呈現平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //圖象複製
            g.DrawImage(mask, dest, src, GraphicsUnit.Pixel);

            g.Dispose();
        }

        public static Bitmap ResizeBitmap(Bitmap mask, int w, int h, int W, int H)
        {
            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;//设置高质量插值法
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;//设置高质量,低速度呈现平滑程度
            g.DrawImage(mask, new Rectangle(0, 0, w, h), new Rectangle(0, 0, W, H), GraphicsUnit.Pixel);//设置高质量,低速度呈现平滑程度
            g.Dispose();
            return bmp;
        }

        #endregion


        public static Bitmap EmfToBitmap(string emfpath1, SizeF emfsize, SizeF destsize, Color transparentColor)
        {
            Image emf1 = Image.FromFile(emfpath1);
            Bitmap bmp1 = new Bitmap((int)Math.Ceiling(destsize.Width), (int)Math.Ceiling(destsize.Height));
            Graphics g = Graphics.FromImage(bmp1);
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(emf1, new RectangleF(0, 0, bmp1.Width, bmp1.Height), new RectangleF(0, 0, (int)emfsize.Width, (int)emfsize.Height), GraphicsUnit.Pixel);
                g.Dispose();
            }
            emf1.Dispose();
         
            //將預設背景變為透明
            bmp1.MakeTransparent(transparentColor);
            Utilities.AntiAliasBitmap(bmp1, transparentColor);


            return bmp1;
        }



      


        #region 測試

      
        public static unsafe void AntiAliasBitmap(Bitmap src_bmp, Color transparentColor)
        {
          
          
            //byte b255 = 255;
            int X = 0;
            int Y = 0;
            int W = src_bmp.Width;
            int H = src_bmp.Height;
            BitmapData src_data = src_bmp.LockBits(new Rectangle(0, 0, src_bmp.Width, src_bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            //BitmapData mix_data = mix_bmp.LockBits(new Rectangle(0, 0, mix_bmp.Width, mix_bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            try
            {

                byte* src_ptr = (byte*)(void*)src_data.Scan0;
                //byte* mix_ptr = (byte*)(void*)mix_data.Scan0;
                int src_remain = src_data.Stride - src_data.Width * 4;
                //int mix_remain = mix_data.Stride - mix_data.Width * 4;

                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        if (src_ptr[3] != 0)
                        {

                            if (CompareColor(src_ptr, 255, transparentColor.R, transparentColor.G, transparentColor.B))
                            {
                                src_ptr[3] = 0;//清除背景色
                            }
                            else
                            {
                                double RD = (double)Math.Abs(src_ptr[0] - transparentColor.R) / transparentColor.R;
                                double GD = (double)Math.Abs(src_ptr[1] - transparentColor.G) / transparentColor.G;
                                double BD = (double)Math.Abs(src_ptr[2] - transparentColor.B) / transparentColor.B;
                                double MaxD = Math.Max(Math.Max(RD, BD), GD);

                                if (MaxD < 0.8) //不透明像素
                                {
                                    RGB best = GetBestColor(src_data, j, i);

                                    int Alpha = (int)(MaxD * 255 * 2.5);
                                    Alpha = Math.Min(Alpha, 255);

                                    //int Hue = RGB_to_HSL(best.R, best.G, best.B);
                                    HSV hsv = RgbToHsv(best.R, best.G, best.B);
                                    if (hsv.H == 0 && hsv.V >=90 && Alpha<10)
                                    {
                                    }
                                    else {
                                        src_ptr[0] = best.R;
                                        src_ptr[1] = best.G;
                                        src_ptr[2] = best.B;

                                        //if(HSL is 白色)
                                        src_ptr[3] = (byte)Alpha;
                                    }
                                
                                }
                            }


                            #region 方法B

                            /*
                            double RD = (double)Math.Abs(src_ptr[0] - transparentColor.R) / transparentColor.R;
                            double GD = (double)Math.Abs(src_ptr[1] - transparentColor.G) / transparentColor.G;
                            double BD = (double)Math.Abs(src_ptr[2] - transparentColor.B) / transparentColor.B;
                            double MaxD = Math.Max(Math.Max(RD, BD), GD);

                            if (MaxD < 0.8) //不透明像素
                            {
                                int Alpha = (int)(MaxD * 255 * 2.5);
                                Alpha = Math.Min(Alpha, 255);
                                src_ptr[3] = (byte)Alpha;
                                //a.SetPixel(x, y, Color.FromArgb(Alpha, myColor.R, myColor.G, myColor.B));
                            }
                       */
                            #endregion

                            #region 方法C
                            
                            /*
                            if (CompareColor(src_ptr, 255, transparentColor.R, transparentColor.G, transparentColor.B))
                            {
                                src_ptr[3] = 0;
                            }
                            else
                            {
                               
                                byte[] cA = Utilities.GetPixels(src_data, j - 1, i);
                                byte[] cB = Utilities.GetPixels(src_data, j + 1, i);
                                byte[] cC = Utilities.GetPixels(src_data, j, i + 1);
                                byte[] cD = Utilities.GetPixels(src_data, j, i - 1);

                                double RD, GD, BD;

                                if (CompareColor(cA, 255, 33, 0, 33) || CompareColor(cB, 255, 33, 0, 33) ||
                                    CompareColor(cC, 255, 33, 0, 33) || CompareColor(cD, 255, 33, 0, 33) ||
                                    CompareColor(cA, 0, 33, 0, 33) || CompareColor(cB, 0, 33, 0, 33) ||
                                    CompareColor(cC, 0, 33, 0, 33) || CompareColor(cD, 0, 33, 0, 33))
                                {

                                    //Rectangle rect = Rectangle.FromLTRB(j - 2, i - 2, j + 2, i + 2);
                                    //byte[] color = AntiAliasColor(src_data, transparentColor, rect);//(1).在上下左右各取出9個點,找出原始色彩

                                    RD = Math.Abs(src_ptr[0] - 128);
                                    GD = Math.Abs(src_ptr[1] - 128);
                                    BD = Math.Abs(src_ptr[2] - 128);

                                    //int alpha = ((int)(RD + BD + GD) / 6 + 191);
                                    int alpha = ((int)(RD + BD + GD) / 6 + 80);//64+k不要超過255都可
                                    RGB best = GetBestColor(src_data, j, i);
                                    if (best != null)
                                    {
                                        src_ptr[0] = best.R;
                                        src_ptr[1] = best.G;
                                        src_ptr[2] = best.B;
                                    }
                                    src_ptr[3] = (byte)alpha;
                                   
                                }
                            }
                           */

                            #endregion

                        }
                        src_ptr += 4;
                        //mix_ptr += 4;

                    }//exit j for

                    src_ptr += ((src_bmp.Width - W) * 4);
                    //mix_ptr += ((mix_bmp.Width - W) * 4);
                    src_ptr += src_remain;
                    //mix_ptr += mix_remain;

                }//exit i for
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            src_bmp.UnlockBits(src_data);
            //mix_bmp.UnlockBits(mix_data);

        }

    
        public static RGB bestrgb = new RGB();
        public static HSV hsv = new HSV();

        public static unsafe RGB GetBestColor(BitmapData src_data, int pX, int pY)
        {

            //Rectangle rect = Rectangle.FromLTRB(pX - 1, pY -1, pX + 2, pY + 2);
            Rectangle rect = Rectangle.FromLTRB(pX - 2, pY - 2, pX + 4, pY + 4);
            //Rectangle rect = Rectangle.FromLTRB(pX - 3, pY - 3, pX + 6, pY + 6);

            if (rect.X <= 0) rect.X = 0;
            if (rect.Y <= 0)rect.Y = 0;

            //Rectangle rect = new Rectangle(pX-1,pY-1,3,3);
            //Console.WriteLine("rect=" + rect.ToString());
            int X = 0;
            int Y = 0;
            int W = 0;
            int H = 0;
            
            BitmapData data = src_data;
            try
            {

                byte* ptr = (byte*)(void*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                X = rect.X;
                Y = rect.Y;
                W = rect.Width;
                H = rect.Height;

                //initialize ptr
                ptr += ((data.Stride * Y) + (X * 4));

                int x,y;
                Dictionary<string, RGB> table = new Dictionary<string, RGB>();
                for ( y = Y; y < H+Y; y++)
                {
                    for ( x = X; x < W+X; x++)
                    {
                        //if (ptr[3] == 255 && x!=pX && y!=pY)
                        if (x == pX && y == pY)
                        {
                            //Console.WriteLine("skip");
                        }
                        else if (x < 0 || y < 0)
                        {

                        }
                        else if (ptr[3] == 255)
                        {
                            string key = ptr[0] + ":" + ptr[1] + ":" + ptr[2];
                            if (table.ContainsKey(key))
                            {
                                RGB rgb = table[key];
                                rgb.Count += 1;
                                //Console.WriteLine(rgb.ToString() + ",rgb.Count=" + rgb.Count);
                            }
                            else
                            {
                                RGB rgb = new RGB();
                                rgb.R = ptr[0];
                                rgb.G = ptr[1];
                                rgb.B = ptr[2];
                                rgb.Count = 1;
                                table.Add(key, rgb);
                                //Console.WriteLine(rgb.ToString() + ",rgb.Count=" + rgb.Count);
                            }
                        }
                        ptr += 4;
                    }
                    ptr += ((data.Width - W) * 4);
                    ptr += remain;
                }

                int bestvalue = -1;
                IDictionaryEnumerator enumerator = table.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    RGB rgb = (RGB)enumerator.Value;
                    //Console.WriteLine("-------------->" + rgb.ToString() +"=" + rgb.Count);
                    if (rgb.Count > bestvalue)
                    {
                        bestvalue = rgb.Count;
                        bestrgb = rgb;
                    }
                }
                //Console.WriteLine("==========================>" + bestrgb.ToString() + "=" + bestrgb.Count);
                table.Clear();
            }
            catch (Exception ee)
            {
                //Console.WriteLine(ee.ToString());
            }


            return bestrgb;
        }

        public static unsafe bool CompareColor(byte* pixel, byte A, byte R, byte G, byte B)
        {
            return (pixel[3] == A && pixel[0] == R && pixel[1] == G && pixel[2] == B);
        }

        public static bool CompareColor(byte[] pixel, byte A, byte R, byte G, byte B)
        {
            return (pixel[3]==A && pixel[0] == R && pixel[1] ==G && pixel[2] == B);
        }

        public static unsafe byte[] GetPixels(BitmapData src_data,int GetX,int GetY)
        {
            byte[] pixel = new byte[4];
            int X = 0;
            int Y = 0;
            int W = 0;
            int H = 0;
            BitmapData data = src_data;

            try
            {
                byte* ptr = (byte*)(void*)data.Scan0;
                int remain = data.Stride - data.Width * 4;

                X = GetX;
                Y = GetY;
              
                ptr += ((data.Stride * Y) + (X * 4));


                pixel[0] = ptr[0];
                pixel[1] = ptr[1];
                pixel[2] = ptr[2];
                pixel[3] = ptr[3];

            }
            catch {
                Console.WriteLine("error");
            }

            return pixel;
        }
       
        #endregion


        #region 另類演算法
         
        public static unsafe Rectangle FindBitmapSize(Bitmap src_bmp)
        {
            Size size = Size.Empty;
            Point pt = new Point(src_bmp.Width,src_bmp.Height);

            int X = 0;
            int Y = 0;
            int W = src_bmp.Width;
            int H = src_bmp.Height;
            BitmapData src_data = src_bmp.LockBits(new Rectangle(0, 0, src_bmp.Width, src_bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            try
            {
                byte* src_ptr = (byte*)(void*)src_data.Scan0;
                int src_remain = src_data.Stride - src_data.Width * 4;
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        if (src_ptr[3] != 0)
                        {
                            if(pt.X>j) pt.X=j;
                            if(pt.Y>i) pt.Y=i;
                            if (size.Width < j) size.Width = j;
                            if (size.Height < i) size.Height = i;
                        }
                        src_ptr += 4;

                    }//exit j for

                    src_ptr += ((src_bmp.Width - W) * 4);
                    src_ptr += src_remain;

                }//exit i for
            }
            catch (Exception ee)
            {
                //Console.WriteLine(ee.ToString());
            }
            src_bmp.UnlockBits(src_data);

            return new Rectangle(pt,size);
        }

        public static int RGB_to_HSL(int R, int G, int B)
        {
            //HSL hsl = new HSL();
            double H;

            int Max, Min, Diff, Sum;
            // Of our RGB values, assign the highest value to Max, and the Smallest to Min
            if (R > G) { Max = R; Min = G; }
            else { Max = G; Min = R; }
            if (B > Max) Max = B;
            else if (B < Min) Min = B;
            Diff = Max - Min;
            Sum = Max + Min;
            
            
            // Luminance - a.k.a. Brightness - Adobe photoshop uses the logic that the
            // site VBspeed regards (regarded) as too primitive = superior decides the 
            // level of brightness.
            //L = (double)Max / 255;
            // Saturation
            //if (Max == 0) S = 0; // Protecting from the impossible operation of division by zero.
            //else S = (double)Diff / Max; // The logic of Adobe Photoshops is this simple.


            // Hue  R is situated at the angel of 360 eller noll degrees; 
            //   G vid 120 degrees
            //   B vid 240 degrees
            double q;
            if (Diff == 0) q = 0; // Protecting from the impossible operation of division by zero.
            else q = (double)60 / Diff;

            if (Max == R)
            {
                if (G < B) H = (double)(360 + q * (G - B)) / 360;
                else H = (double)(q * (G - B)) / 360;
            }
            else if (Max == G) H = (double)(120 + q * (B - R)) / 360;
            else if (Max == B) H = (double)(240 + q * (R - G)) / 360;
            else H = 0.0;

            return (int)H;
        }
       
        public static HSV RgbToHsv(int R, int G, int B)
        {
            int H, S, V;

            int value = 0;            

            decimal min;
            decimal max;
            decimal delta;

            decimal R1 = Convert.ToDecimal(R) / 255;
            decimal G1 = Convert.ToDecimal(G) / 255;
            decimal B1 = Convert.ToDecimal(B) / 255;

            min = Math.Min(Math.Min(R1, G1), B1);
            max = Math.Max(Math.Max(R1, G1), B1);
            V = Convert.ToInt32(max * 100);
            delta = (max - min) * 100;

            if (max == 0 || delta == 0)
                S = 0;
            else
                S = Convert.ToInt32(delta / max);


            //if (S == 0) S = 100-V;

            hsv.H = RGB_to_HSL(R,G,B);
            hsv.S = S;
            hsv.V = V;

            return hsv;
            //return (int)S;
            //return (int)value;

        }

        public class RGB
        {
            public byte R;
            public byte G;
            public byte B;
            public int Count;

            public override string ToString()
            {
                return R + ":" + G + ":" + B;
            }
        }

        public struct HSV
        {
            public int H;
            public int S;
            public int V;
            public override string ToString()
            {
                return H + "," + S + "," + V;
            }
        };

       

        #endregion

        #endregion


        //註冊ICON

        public static bool IsRegisterIcon(string ext) 
        {
            string yn = ReadRegisterKey("HKCR", ext, "");
            return yn!=null;
        }
        public static void RegisterIcon(string name,string ext,string icopath,string exepath) 
        {
            //WriteRegisterKey(
            /*
            WriteRegStr HKCR ".nxb" "" "NXBoard_Project"
		    WriteRegStr HKCR "NXBoard_Project" "" "NXBoard_Project"
		    WriteRegDWORD HKCR "NXBoard_Project" "EditFlags" 0x00000000
		    WriteRegDWORD HKCR "NXBoard_Project" "BrowserFlags" 0x00000008
		    WriteRegStr HKCR "NXBoard_Project\DefaultIcon" "" '"$INSTDIR\nxb.ico"'
		    WriteRegStr HKCR "NXBoard_Project\shell" "" "open"
		    WriteRegStr HKCR "NXBoard_Project\shell\open\command" "" '"$INSTDIR\NXBoard\NXBoard.exe" "%1"'
            */

            try
            {
                WriteRegisterKey("HKCR", ext, "", name);
                WriteRegisterKey("HKCR", name, "", name);
                WriteRegisterKey("HKCR", name, "EditFlags", "0x00000000");
                WriteRegisterKey("HKCR", name, "BrowserFlags", "0x00000008");
                WriteRegisterKey("HKCR", name + @"\DefaultIcon", "", icopath);
                WriteRegisterKey("HKCR", name + @"\shell", "", "open");
                WriteRegisterKey("HKCR", name + @"\shell\open\command", "", exepath + " %1");

            }
            catch(Exception ee){
                Console.WriteLine("RegisterIcon="+ee.ToString());
            }
        }

        public static void SetMediaPlayerOverlay(bool yn) 
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\MediaPlayer\Preferences\VideoSettings\", true);
                if (yn)
                {
                    //完整::視訊加速::不可截切模式
                    rk.SetValue("DontUseFrameInterpolation", (int)0);
                    rk.SetValue("DVDUseSWDecoder", (int)1);
                    rk.SetValue("DVDUseVMR", (int)1);
                    rk.SetValue("DVDUseVMRFSMS", (int)1);
                    rk.SetValue("DVDUseVMROverlay", (int)1);
                    rk.SetValue("EnableDXVA_WMV", (int)0);
                    rk.SetValue("PerformanceSettings", (int)2);
                    rk.SetValue("UseFullScrMS", (int)0);
                    rk.SetValue("UseRGB", (int)1);
                    rk.SetValue("UseVMR", (int)1);
                    rk.SetValue("UseVMROverlay", (int)1);
                    rk.SetValue("UseYUV", (int)1);
                }
                else
                {
                    //無::視訊加速::可截切模式
                    rk.SetValue("DontUseFrameInterpolation", (int)1);
                    rk.SetValue("DVDUseSWDecoder", (int)0);
                    rk.SetValue("DVDUseVMR", (int)0);
                    rk.SetValue("DVDUseVMRFSMS", (int)0);
                    rk.SetValue("DVDUseVMROverlay", (int)0);
                    rk.SetValue("EnableDXVA_WMV", (int)0);
                    rk.SetValue("PerformanceSettings", (int)0);
                    rk.SetValue("UseFullScrMS", (int)1);
                    rk.SetValue("UseRGB", (int)0);
                    rk.SetValue("UseVMR", (int)0);
                    rk.SetValue("UseVMROverlay", (int)0);
                    rk.SetValue("UseYUV", (int)0);

                }
                rk.Flush();
                rk.Close();
            }
            catch { }
           
        }


   

        public static IntPtr hdcDesktop = API.GetWindowDC(API.GetDesktopWindow());
        public static void ScreenShot(Bitmap bmp, Rectangle bounds)
        {
            try
            {
                Graphics g = Graphics.FromImage(bmp);
                IntPtr hdcBmp = g.GetHdc();

                API.BitBlt(hdcBmp, 0, 0, bounds.Width, bounds.Height, hdcDesktop, bounds.Left, bounds.Top, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
                g.ReleaseHdc(hdcBmp);
                g.Dispose();
                //API.ReleaseDC(handle, hdcSrc);
            }
            catch (Exception ee)
            {
                MessageBox.Show("desktopBMP=" + ee.ToString());
            }

        }


        public static void SaveErrorLog(string uid, string msg)
        {
            /*
            string logFile = Application.StartupPath + "\\ErrorLog.log";
            if (File.Exists(logFile))
            {
                File.WriteAllText(logFile, " ");
            }

            string logString = File.ReadAllText(logFile);
            logString += Environment.NewLine;

            logString += "ERROR:" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + Environment.NewLine;
            logString += "ID = " + uid + Environment.NewLine;
            logString += "MSG = " + msg + Environment.NewLine;
            File.WriteAllText(logFile, logString);
            */
        }


        //public static void ProcessStop() 
        //{
          
        //    foreach (Process proc in ProcessList) 
        //    {
        //        MessageBox.Show("proc");
        //        try
        //        {
                   
        //            proc.Kill();
        //        }
        //        catch(Exception ee)
        //        {
        //            MessageBox.Show("proc="+ee.ToString());
        //        }
        //    }
        //}

        //private static List<System.Diagnostics.Process> ProcessList = new List<System.Diagnostics.Process>();
        public static void ProcessStart(string filename , string args)
        {
            Debug.WriteLine2(filename);
            /*
             //實例一個Process類，啟動一個獨立進程
              Process p = new Process();
  
              //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：
  
             p.StartInfo.FileName = "cmd.exe";           //設定程序名
             p.StartInfo.Arguments = "/c " + filename + " "+ args;    //設定程式執行參數
             p.StartInfo.UseShellExecute = false;        //關閉Shell的使用
             p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入
             p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出
             p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出
             p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口
 
             p.Start();   //啟動
            */

            /*
            string cmd = Application.StartupPath + "\\ConsoleApp.exe";
            string aa = "";
            if (System.IO.File.Exists(cmd))
            {
                


                aa = args.ToString();
                args = "\"" + filename + "\"";

                if (aa != null && aa.Length > 0)
                {
                    args += " " + "\"" + aa + "\"";
                }

              //  MessageBox.Show(args);
              
                filename = cmd;
            }
            */

            try
            {
                //if (System.IO.File.Exists(filename))
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();

                    //if (filename.IndexOf("http") == 0)
                    //{
                        
                    //    ProcessList.Add(p);
                    //}

                    //MessageBox.Show("ProcessStart="+filename);
                    p.StartInfo.FileName = filename.Trim();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.CreateNoWindow = false;
                    //UseShellExecute
                    if (args != null && args.Length > 0)
                    {
                        p.StartInfo.Arguments = args;
                    }
                    p.Start();
                }
            }
            catch (Exception ee)
            {
                Debug.Trace(ee.ToString());
            }
            
        }

        #region Text Encoding

        public static Encoding GetEncoding(string fileName, Encoding defaultEncoding)
        {
            string lang = Utilities.ReadRegisterKey("HKLM", @"Software\NXBoard\", "Lang");

            if (lang.Equals("zh-TW"))
            {
                defaultEncoding = Encoding.GetEncoding("big5");
            }
            else if (lang.Equals("zh-CN"))
            {
                defaultEncoding = Encoding.GetEncoding("gbk");
            }
            else
            {
                defaultEncoding = Encoding.Default;
            }

            //defaultEncoding = Encoding.GetEncoding("gbk");
            FileStream fs = new FileStream(fileName, FileMode.Open);

            Encoding targetEncoding = Utilities.GetEncoding(fs, defaultEncoding);

            fs.Close();


            //Console.WriteLine("code=" + targetEncoding.EncodingName);

            return targetEncoding;
        }

        /// <summary>

        /// 取得一個文字檔案流的編碼方式。

        /// </summary>

        /// <param name="stream">文字檔案流。</param>

        /// <param name="defaultEncoding">默認編碼方式。當該方法無法從檔的頭部取得有效的前置字元時，將返回該編碼方式。</param>

        /// <returns></returns>
        public static Encoding GetEncoding(FileStream stream, Encoding defaultEncoding)
        {

            Encoding targetEncoding = defaultEncoding;

            if (stream != null && stream.Length >= 2)
            {

                //保存檔流的前4個位元組

                byte byte1 = 0;

                byte byte2 = 0;

                byte byte3 = 0;

                byte byte4 = 0;

                //保存當前Seek位置

                long origPos = stream.Seek(0, SeekOrigin.Begin);

                stream.Seek(0, SeekOrigin.Begin);

                int nByte = stream.ReadByte();

                byte1 = Convert.ToByte(nByte);

                byte2 = Convert.ToByte(stream.ReadByte());

                if (stream.Length >= 3)
                {

                    byte3 = Convert.ToByte(stream.ReadByte());

                }

                if (stream.Length >= 4)
                {

                    byte4 = Convert.ToByte(stream.ReadByte());

                }

                //根據檔流的前4個位元組判斷Encoding

                //Unicode {0xFF, 0xFE};

                //BE-Unicode {0xFE, 0xFF};

                //UTF8 = {0xEF, 0xBB, 0xBF};

                if (byte1 == 0xFE && byte2 == 0xFF)//UnicodeBe
                {

                    targetEncoding = Encoding.BigEndianUnicode;

                }

                if (byte1 == 0xFF && byte2 == 0xFE && byte3 != 0xFF)//Unicode
                {

                    targetEncoding = Encoding.Unicode;

                }

                if (byte1 == 0xEF && byte2 == 0xBB && byte3 == 0xBF)//UTF8
                {

                    targetEncoding = Encoding.UTF8;
                }

                //恢復Seek位置　　　 

                stream.Seek(origPos, SeekOrigin.Begin);

            }

            return targetEncoding;

        }

        #endregion




        public static bool IsCtrlKeys(Keys key)
        {
            bool ctrl = false;


            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    ctrl = true;
                    break;
                //case Keys.LMenu:
                //case Keys.RMenu:
                //    rtnKey = rtnKey | Keys.Alt;
                //    break;
                //case Keys.LShiftKey:
                //case Keys.RShiftKey:
                //    rtnKey = rtnKey | Keys.Shift;
                //    break;
                default:
                    break;
            }
          
           

            return ctrl;
        }




    }
}
