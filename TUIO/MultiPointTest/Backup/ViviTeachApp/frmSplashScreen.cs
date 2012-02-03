using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CloudPaperApp
{
    public class frmSplashScreen : PerPixelAlphaForm
    {
        public frmSplashScreen()
        {
            InitializeComponent();

            csInit();
        }

        private void csInit()
        {
           
        }




        private void csFree()
        {

            //System.Windows.Forms.MessageBox.Show("free");
            //try
            //{
            //    ScreenImage.Dispose();
            //}
            //catch { }

            //ScreenImage = null;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PreLoaderScreen
            // 
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Name = "PreLoaderScreen";
            this.TopMost = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            Rectangle rect = Utilities.ScreenArea;
            this.Location = new Point((rect.Width - utils.SplashForm.Width) / 2, (rect.Height - utils.SplashForm.Height) / 2);

            this.ResumeLayout(false);

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            csFree();
        }

        private bool IsLoading = false;
        private void Start()
        {
            System.Threading.Thread thread1 = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
            {
                this.IsLoading = true;

                try
                {
                    int alpha = 0;
                    while (alpha<255) 
                    {
                        Console.WriteLine("alpha=" + alpha);
                        alpha+=10;
                        InvalidateEx(alpha);
                        System.Threading.Thread.Sleep(33);//30
                    }
                   
                    //System.Windows.Forms.MessageBox.Show("shit");
                    InvalidateEx(alpha);
                    System.Threading.Thread.Sleep(2000);//30
                   
                }
                catch (Exception ee)
                {
                    IsLoading = false;
                  //  System.Windows.Forms.MessageBox.Show("ex=" + ee.ToString());
                   // Console.WriteLine(ee.ToString());

                }
                finally
                {
                   // System.Windows.Forms.MessageBox.Show("finished");
                   AnimationClose();
                }
            }));

         
            thread1.Priority = System.Threading.ThreadPriority.Lowest;
            thread1.Start();

          
        }

        private void InvalidateEx(int alpha)
        {
            if (alpha > 255) alpha = 255;            
            if (this.InvokeRequired) {
                this.Invoke(new MyAction1<int>(InvalidateEx), alpha);
                return;
            }
            this.SetBitmap(utils.SplashForm, (byte)alpha);
        }

        private void Stop()
        {
            this.IsLoading = false;
        }

        private void AnimationClose()
        {
            byte AnimationAlpha = 255;
            int AnimationCount = 10;//29

            Bitmap bmp = new Bitmap(1,1);
            try
            {
                while (true)
                {
                    AnimationCount--;

                    if (AnimationCount <= 0)
                    {
                        this.Invoke(new MyAction0(delegate()
                        {
                            this.SetBitmap(bmp);
                            this.Close();
                        }));

                        break;
                    }
                    else
                    {
                        this.Invoke(new MyAction0(delegate()
                        {
                            AnimationAlpha = (byte)((AnimationCount / 10f) * 255);
                            this.SetBitmap(utils.SplashForm, AnimationAlpha);
                        }));
                    }

                    System.Threading.Thread.Sleep(33);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("AnimationClose=" + ee.ToString());
            }

        }

    }
}
