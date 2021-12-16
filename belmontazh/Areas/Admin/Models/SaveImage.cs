using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Areas.Admin.Models
{
    public class SaveImage
    {
        public string Upload(HttpPostedFileBase fil, int NewWidth, int MaxHeight, string new_name, string path)
        {
            string url = "";
            System.Drawing.Image img = System.Drawing.Image.FromStream(fil.InputStream);
            int oldw = img.Width, oldh = img.Height;
            if (oldw <= NewWidth)
            {
                NewWidth = oldw;
            }

            int NewHeight = oldh * NewWidth / oldw;
            if (NewHeight > MaxHeight)
            {
                NewWidth = oldw * MaxHeight / oldh;
                NewHeight = MaxHeight;
            }
            System.Drawing.Image dest = new System.Drawing.Bitmap(NewWidth, NewHeight, img.PixelFormat);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dest);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, NewWidth, NewHeight);
            url = new_name + Path.GetExtension(fil.FileName);
            dest.Save(path + url, img.RawFormat);
            dest.Dispose();
            img.Dispose();
            g.Dispose();

            return url;
        }
        public string Upload(FileInfo fil, int NewWidth, int MaxHeight, string new_name, string path)
        {
            string url = "";
            System.Drawing.Image img = System.Drawing.Image.FromFile(fil.FullName);
            if(fil.Extension.ToLower()==".gif")
            {
                img.Save(path+"/t.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                img = System.Drawing.Image.FromFile(path + "/t.jpg");
            }
            int oldw = img.Width, oldh = img.Height;
            if (oldw <= NewWidth)
            {
                NewWidth = oldw;
            }

            int NewHeight = oldh * NewWidth / oldw;
            if (NewHeight > MaxHeight)
            {
                NewWidth = oldw * MaxHeight / oldh;
                NewHeight = MaxHeight;
            }
            System.Drawing.Image dest = new System.Drawing.Bitmap(NewWidth, NewHeight, img.PixelFormat);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dest);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, NewWidth, NewHeight);
            url = new_name + fil.Extension;
            dest.Save(path + url, img.RawFormat);
            dest.Dispose();
            img.Dispose();
            g.Dispose();
            
            return url;
        }
    }
}