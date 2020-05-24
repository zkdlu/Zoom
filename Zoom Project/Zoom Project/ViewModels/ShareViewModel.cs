using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using Zoom_Project.ViewModels.Process;

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
            if (SelectedTabIndex == 0)
            {
                Mediator.ShareMode = ShareMode.Screen;
            }
            else
            {
                ProcessViewModel selectedProcess = ApplicationViewModel.SelectedProcess;
                Mediator.ShareMode = ShareMode.Application;

                if (selectedProcess != null)
                {
                    Mediator.Application = selectedProcess.ProcessInfo;
                }
                else
                {
                    Mediator.Application = null;
                }
            }

            Mediator.IsShare = true;

            window.DialogResult = true;
            window.Close();
        }
    }
}
