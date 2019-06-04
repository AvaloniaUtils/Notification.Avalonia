using System;
using Avalonia.Controls;
using Notification.Avalonia.Enums;
using Notification.Avalonia.Views;
using ReactiveUI;

namespace Notification.Avalonia.ViewModel
{
    public class ButtonYesNoViewModel: ReactiveObject, IRoutableViewModel

    {
        private NotificationWindow _window;
        public string UrlPathSegment { get; }= Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        public ButtonYesNoViewModel(IScreen screen,NotificationWindow window) => (HostScreen,_window) = (screen,window);
        
        public void ClickYes()
        {
            _window.ButtonsResult = ButtonsResult.Yes;
            _window.Close();
        }
        public void ClickNo()
        {
            _window.ButtonsResult = ButtonsResult.No;
            _window.Close();
        }
    }
}