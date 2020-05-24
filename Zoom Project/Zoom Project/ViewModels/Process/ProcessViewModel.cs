using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Zoom_Project.API;
using Zoom_Project.Models;

namespace Zoom_Project.ViewModels.Process
{
    public class ProcessViewModel : BaseViewModel
    {
        public event SelectDele Selected;

        public ProcessInfo ProcessInfo
        {
            get;
            private set;
        }

        public IntPtr ProcessHandle
        {
            get { return ProcessInfo.ProcessHandle; }
        }

        public string Title
        {
            get { return ProcessInfo.Title; }
        }

        public BitmapImage Image
        {
            get { return ProcessInfo.Image; }
            set
            {
                ProcessInfo.Image = value;
                OnRaiseProperty(nameof(Image));
            }
        }

        private ICommand selectCommnad;
        public ICommand SelectCommnad
        {
            get
            {
                if (selectCommnad == null)
                {
                    selectCommnad = new RelayCommand(SelectProcess);
                }

                return selectCommnad;
            }
        }

        private void SelectProcess()
        {
            Selected?.Invoke(this);
        }

        public ProcessViewModel(ProcessInfo processInfo)
        {
            ProcessInfo = processInfo;
        }

        public void TakeScreen()
        {
            Image = Capture.ScreenshotWindow(ProcessInfo.ProcessHandle);
        }
    }

    public delegate void SelectDele(ProcessViewModel processViewModel);
}
