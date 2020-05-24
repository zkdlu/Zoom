using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Zoom_Project.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DispatcherTimer _dispatcherTimer;

        private string buttonContent;
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
            
        }
    }
}
