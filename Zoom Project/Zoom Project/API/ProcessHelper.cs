using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zoom_Project.API
{
    public class ProcessApi
    {
        public string Title { get; set; }
        public IntPtr Handle { get; set; }
    }

    public static class ProcessHelper
    {
        public static List<ProcessApi> GetRunnningProcesses()
        {
            var list = new List<ProcessApi>();
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                var result = new StringBuilder(255);
                USER32.GetWindowText(hWnd, result, result.Capacity + 1);
                string title = result.ToString();

                if (!string.IsNullOrEmpty(title) && USER32.IsWindowVisible(hWnd) && !title.Equals("Program Manager"))
                {
                    list.Add(new ProcessApi
                    {
                        Title = title,
                        Handle = hWnd
                    });
                }

                return true;
            };
            USER32.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);

            list = list.Where(r =>
            {
                int clocked;
                long result = DWMAPI.DwmGetWindowAttribute(r.Handle, DWMAPI.DWMWA_CLOAKED, out clocked, sizeof(int));
                if (result != 0L)
                {
                    clocked = 0;
                }

                return clocked == 0;
            }).ToList();

            return list;
        }
    }
}
