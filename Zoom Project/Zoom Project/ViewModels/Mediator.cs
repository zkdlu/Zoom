using Zoom_Project.Models;

namespace Zoom_Project.ViewModels
{
    public enum ShareMode
    {
        Screen, Application, None
    }

    public static class Mediator
    {
        public static bool IsShare
        {
            get;
            set;
        }

        public static ShareMode ShareMode
        {
            get;
            set;
        }

        public static ProcessInfo Application
        {
            get;
            set;
        }
    }
}
