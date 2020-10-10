using System;
using Avalonia.Controls;
using Notification.Avalonia.Enums;
using Notification.Avalonia.Views;
using ReactiveUI;

namespace Notification.Avalonia.ViewModel
{
    public class ButtonOkViewModel: ReactiveObject, IRoutableViewModel
    {
        private NotificationWindow _window;
        public string UrlPathSegment { get; }= Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        public ButtonOkViewModel(IScreen screen,NotificationWindow window) => (HostScreen,_window) = (screen,window);

        public void ClickOk()
        {
            _window.ButtonsResult = ButtonsResult.Ok;
            _window.Close();
        }
    }
}