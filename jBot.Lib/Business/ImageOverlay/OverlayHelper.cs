using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace jBot.Lib.Business.ImageOverlay
{
    public static class OverlayHelper
    {
        public static string CreateLogoImage(string orginalImageUrl, string overLayImageUrl)
        {
            try
            {
                Image logoImage = Image.FromFile(overLayImageUrl);

                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(orginalImageUrl);
                    using (MemoryStream imageStream = new MemoryStream(imageData))
                    {
                        Image originalImage = Image.FromStream(imageStream);

                        using (originalImage)
                        {
                            using (Bitmap newBitmap = new Bitmap(400, 400))
                            {
                                using (Graphics canvas = Graphics.FromImage((newBitmap)))
                                {
                                    canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    canvas.DrawImage(
                                        originalImage,
                                        new Rectangle(0, 0, 400, 400),
                                        new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                        GraphicsUnit.Pixel
                                    );

                                    canvas.DrawImage(
                                        logoImage,
                                        new Point(80, newBitmap.Size.Height - logoImage.Size.Height - 30)
                                    );
                                    canvas.Save();

                                    string OutputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetTempFileName().Replace(".tmp", ".png"));
                                    newBitmap.Save(OutputFile);
                                    return OutputFile;
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }        
    }
}
