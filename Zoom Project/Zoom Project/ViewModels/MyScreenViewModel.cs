using System;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zoom_Project.API;

namespace Zoom_Project.ViewModels
{
    public class MyScreenViewModel : BaseViewModel
    {
        private DispatcherTimer _dispatcherTimer;

        private BitmapImage myScreen;
        public BitmapImage MyScreen
        {
            get { return myScreen; }
            set
            {
                myScreen = value;
                OnRaiseProperty(nameof(MyScreen));
            }
        }

        public MyScreenViewModel()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        ~MyScreenViewModel()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
            }
            _dispatcherTimer = null;
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MyScreen = Capture.ScreenshotScreen();
        }
    }
}
