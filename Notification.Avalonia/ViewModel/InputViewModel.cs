using System;
using Avalonia.Controls;
using Notification.Avalonia.Views;
using ReactiveUI;

namespace Notification.Avalonia.ViewModel
{
    public class InputViewModel : ReactiveObject, IRoutableViewModel
    {
        private NotificationWindow _window;
        private string _text;

        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text,value);
        }

        public string UrlPathSegment { get; }= Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        public InputViewModel(IScreen screen,NotificationWindow window) => (HostScreen,_window) = (screen,window);

        public void ClickSend()
        {
            _window.TextResult = _text;
            _window.Close();
        }
    }
}