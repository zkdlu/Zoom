using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Zoom_Project.API;
using Zoom_Project.Models;
using Zoom_Project.ViewModels.Process;

namespace Zoom_Project.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        private DispatcherTimer _dispatcherTimer;
        public ObservableCollection<ProcessViewModel> Processes { get; } = new ObservableCollection<ProcessViewModel>();

        private ProcessViewModel selectedProcess;
        public ProcessViewModel SelectedProcess
        {
            get { return selectedProcess; }
            set
            {
                selectedProcess = value;
                OnRaiseProperty(nameof(SelectedProcess));
            }
        }

        public ApplicationViewModel()
        {
            var processes = ProcessHelper.GetRunnningProcesses();

            foreach (var p in processes)
            {
                ProcessInfo processInfo = new ProcessInfo
                {
                    ProcessHandle = p.Handle,
                    Title = p.Title
                };

                ProcessViewModel processViewModel = new ProcessViewModel(processInfo);
                processViewModel.Selected += ProcessViewModel_Selected;
                Processes.Add(processViewModel);
            }

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        ~ApplicationViewModel()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
            }
            _dispatcherTimer = null;
        }

        private void ProcessViewModel_Selected(ProcessViewModel processViewModel)
        {
            SelectedProcess = processViewModel;
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach (var p in Processes)
            {
                p.TakeScreen();
            }
        }

        private bool IsNotWindowHandle(IntPtr hWnd)
        {
            return hWnd == IntPtr.Zero;
        }
    }
}
