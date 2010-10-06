using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;

/// <summary>
/// PicObject 的摘要描述
/// </summary>
public class PicObject
{
	public PicObject()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    /// <summary>
    /// 將圖片縮圖
    /// </summary>
    /// <param name="sourceImagePath">來源圖片位置及檔名</param>
    /// <param name="thumbnailPath">目的圖片位置及檔名</param>
    /// <param name="width">寬</param>
    /// <param name="height">高</param>
    /// <param name="mode">縮放模式(HW：指定高寬縮放，可能變形；W：指定寬度，高度按比例；H：指定高度，寬度按比例；CUT：指定高寬裁減，不變形）</param>
    public static void MakeThumbnail(string sourceImagePath, string thumbnailPath, int width, int height, string mode)
    {
        Image originalImage = Image.FromFile(sourceImagePath);
        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;
        switch (mode)
        {
            // 指定高寬縮放（可能變形）
            case "HW":
                break;
            // 指定寬度，高度按比例
            case "W":
                height = originalImage.Height * width / originalImage.Width;
                break;
            // 指定高度，寬度按比例
            case "H":
                width = originalImage.Width * height / originalImage.Height;
                break;
            //指定高寬裁減（不變形）
            case "CUT":
                if (((double)originalImage.Width) / originalImage.Height > ((double)width) / height)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * width / height;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / width;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }
        Image bitmap = new Bitmap(width, height);
        Graphics g = Graphics.FromImage(bitmap);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.Clear(Color.Transparent);
        g.DrawImage(originalImage, new Rectangle(0, 0, width, height), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
        bitmap.Save(thumbnailPath, ImageFormat.Png);
        originalImage.Dispose();
    }
}