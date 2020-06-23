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

            Image LogoImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, overLayImageUrl));

            using (WebClient client = new WebClient())
            {
                byte[] ImageData = client.DownloadData(orginalImageUrl);
                using (MemoryStream ImageStream = new MemoryStream(ImageData))
                {
                    Image OriginalImage = Image.FromStream(ImageStream);

                    using (OriginalImage)
                    {
                        using (Bitmap NewBitmap = new Bitmap(400, 400))
                        {
                            using (Graphics Canvas = Graphics.FromImage((NewBitmap)))
                            {
                                Canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                Canvas.DrawImage(
                                    OriginalImage,
                                    new Rectangle(0, 0, 400, 400),
                                    new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                                    GraphicsUnit.Pixel
                                );

                                Canvas.DrawImage(
                                    LogoImage,
                                    new Point(80, NewBitmap.Size.Height - NewBitmap.Size.Height - 30)
                                );
                                Canvas.Save();

                                string OutputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetTempFileName().Replace(".tmp", ".png"));
                                NewBitmap.Save(OutputFile);
                                return OutputFile;
                            }
                        }
                    }
                }
            }
        }
    }
}
