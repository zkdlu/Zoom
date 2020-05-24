using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zoom_Project.API;
using Zoom_Project.Models;

namespace Zoom_Project.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DispatcherTimer _dispatcherTimer;

        private string buttonContent = "Share";
        public string ButtonConent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                OnRaiseProperty(nameof(ButtonConent));
            }
        }

        private BitmapImage shareImage;
        public BitmapImage ShareImage
        {
            get { return shareImage; }
            set
            {
                shareImage = value;
                OnRaiseProperty(nameof(ShareImage));
            }
        }

        private ICommand windowOpenCommand;
        public ICommand WindowOpenCommand
        {
            get
            {
                if (windowOpenCommand == null)
                {
                    windowOpenCommand = new RelayCommand(OpenWindowOrStop);
                }

                return windowOpenCommand;
            }
        }

        private void OpenWindowOrStop()
        {
            if (!Mediator.IsShare)
            {
                ShareWindow shareWindow = new ShareWindow();
                shareWindow.Closed += ShareWindow_Closed;
                shareWindow.ShowDialog();
            }
            else
            {
                StopShare();
            }
        }

        private void ShareWindow_Closed(object sender, EventArgs e)
        {
            if (Mediator.IsShare)
            {
                StartShare();
            }
        }

        private void StopShare()
        {
            Mediator.IsShare = false;

            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
                _dispatcherTimer = null;
            }

            ButtonConent = "Share";
            ShareImage = null;
        }

        private void StartShare()
        {
            if (_dispatcherTimer == null)
            {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
                _dispatcherTimer.Tick += _dispatcherTimer_Tick;
                _dispatcherTimer.Start();
            }

            ButtonConent = "Stop";
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!Mediator.IsShare)
            {
                return;
            }

            switch (Mediator.ShareMode)
            {
                case ShareMode.Screen:
                    ShareScreen();
                    break;
                case ShareMode.Application:
                    ShareApplication();
                    break;
                case ShareMode.None:
                    StopShare();
                    break;
            }
        }

        private void ShareApplication()
        {
            ProcessInfo processInfo = Mediator.Application;
            if (processInfo == null)
            {
                MessageBox.Show("공유 할 프로세스를 선택해주세요.");

                StopShare();

                return;
            }

            ShareImage = Capture.ScreenshotWindow(processInfo.ProcessHandle);
        }

        private void ShareScreen()
        {
            ShareImage = Capture.ScreenshotScreen();
        }
    }
}
