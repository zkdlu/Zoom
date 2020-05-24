using System;
using System.Windows.Media.Imaging;

namespace Zoom_Project.Models
{
    public class ProcessInfo
    {
        public IntPtr ProcessHandle
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        private BitmapImage image;
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
            }
        }
    }
}
