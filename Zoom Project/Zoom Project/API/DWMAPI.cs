using System;
using System.Runtime.InteropServices;

namespace Zoom_Project.API
{
    public static class DWMAPI
    {
        public const int DWMWA_CLOAKED = 14;

        [DllImport("Dwmapi.dll")]
        public static extern long DwmGetWindowAttribute(IntPtr hWnd, int dwAttr, out int pvAttr, int cbAttr);
    }
}
