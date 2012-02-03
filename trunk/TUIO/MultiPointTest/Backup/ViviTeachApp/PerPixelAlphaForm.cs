using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace CloudPaperApp
{
    public class PerPixelAlphaForm : System.Windows.Forms.Form
    {
        // TRANSMISSINGCOMMENT: Class PerPixelAlphaForm

        // Dim CurrBitmap As Bitmap

        public PerPixelAlphaForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.ShowInTaskbar = true;
            //this.AllowDrop = true;
        }
        public PerPixelAlphaForm(Bitmap bitmap)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.AllowDrop = true;
            try
            {
                SetBitmap(bitmap);
            }
            catch { }
        }

        // TRANSMISSINGCOMMENT: Method SetBitmap
        public void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }
      

        // TRANSMISSINGCOMMENT: Method SetBitmap
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap == null) return;
            if (!((bitmap.PixelFormat == PixelFormat.Format32bppArgb)))
            {
                //throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
                return;
            }
            // CurrBitmap = bitmap
            IntPtr screenDc = API.GetDC(IntPtr.Zero);
            IntPtr memDc = API.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;
            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = API.SelectObject(memDc, hBitmap);
                Size bSize = bitmap.Size;
                API.Size size = new API.Size(bSize.Width, bSize.Height);
                API.Point pointSource = new API.Point(0, 0);
                API.Point topPos = new API.Point(Left, Top);
                API.BLENDFUNCTION blend = new API.BLENDFUNCTION();
                blend.BlendOp = API.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;

                blend.AlphaFormat = API.AC_SRC_ALPHA;
                API.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, API.ULW_ALPHA);
            }
            catch { }
            finally
            {
                API.ReleaseDC(IntPtr.Zero, screenDc);
                if (!((hBitmap.Equals(IntPtr.Zero))))
                {
                    API.SelectObject(memDc, oldBitmap);
                    API.DeleteObject(hBitmap);
                }
                API.DeleteDC(memDc);
            }
        }


        // TRANSMISSINGCOMMENT: Property CreateParams
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | (524288);
                return cp;
            }
        }
    } 
}
