using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Zoom_Project.API
{
    public static class Capture
    {
        public static BitmapImage ScreenshotWindow(IntPtr processHandle)
        {
            return ConvertToBitmapImage(Screenshot2(processHandle));
        }

        public static BitmapImage ScreenshotScreen()
        {
            IntPtr windowHandle = USER32.GetDesktopWindow();

            return ConvertToBitmapImage(Screenshot(windowHandle));
        }

        private static Bitmap Screenshot(IntPtr hWnd)
        {
            try
            {
                IntPtr hDC = USER32.GetWindowDC(hWnd);

                RECT windowRect = new RECT();
                USER32.GetWindowRect(hWnd, ref windowRect);

                int width = windowRect.right - windowRect.left;
                int height = windowRect.bottom - windowRect.top;

                IntPtr hdcDest = GDI32.CreateCompatibleDC(hDC);
                IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hDC, width, height);

                IntPtr hObj = GDI32.SelectObject(hdcDest, hBitmap);
                GDI32.BitBlt(hdcDest, 0, 0, width, height, hDC, 0, 0, USER32.SRCCOPY | USER32.CAPTUREBLT);
                GDI32.SelectObject(hdcDest, hObj);
                GDI32.DeleteDC(hdcDest);
                USER32.ReleaseDC(hWnd, hDC);

                Bitmap image = Image.FromHbitmap(hBitmap);
                GDI32.DeleteObject(hBitmap);

                return image;
            }
            catch
            {
                return new Bitmap(100, 100);
            }
        }

        private static Bitmap Screenshot2(IntPtr hWnd)
        {
            try
            {
                Graphics g = Graphics.FromHwnd(hWnd);
                Rectangle rect = Rectangle.Round(g.VisibleClipBounds);

                if (rect.IsEmpty)
                {
                    return Screenshot(hWnd);
                }

                Bitmap bitmap = new Bitmap(rect.Width, rect.Height, g);

                Graphics imgGraphics = Graphics.FromImage(bitmap);
                IntPtr hdc = imgGraphics.GetHdc();

                bool canCapture = USER32.PrintWindow(hWnd, hdc, 3);

                imgGraphics.ReleaseHdc(hdc);

                return bitmap;
            }
            catch
            {
                return new Bitmap(100, 100);
            }
        }

        private static BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();

            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }
    }
}
