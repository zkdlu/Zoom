using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;

namespace Zoom_Project.ViewModels
{
    public class ShareViewModel : BaseViewModel
    {
        private MyScreenViewModel myScreenViewModel;
        public MyScreenViewModel MyScreenViewModel
        {
            get
            {
                if (myScreenViewModel == null)
                {
                    myScreenViewModel = new MyScreenViewModel();
                }

                return myScreenViewModel;
            }
        }

        private ApplicationViewModel applicationViewModel;
        public ApplicationViewModel ApplicationViewModel
        {
            get
            {
                if (applicationViewModel == null)
                {
                    applicationViewModel = new ApplicationViewModel();
                }

                return applicationViewModel;
            }
        }

        private int selecedtTabIndex;
        public int SelectedTabIndex
        {
            get { return selecedtTabIndex; }
            set
            {
                selecedtTabIndex = value;
                OnRaiseProperty(nameof(SelectedTabIndex));
            }
        }

        private ICommand clickCommnad;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommnad == null)
                {
                    clickCommnad = new RelayCommand<Window>(SelectSharetMode);
                }

                return clickCommnad;
            }
        }

        private void SelectSharetMode(Window window)
        {
            window.DialogResult = true;
            window.Close();
        }
    }
}
