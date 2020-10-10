using System;
using System.Reactive;
using Avalonia.Controls;
using Notification.Avalonia.Enums;
using Notification.Avalonia.Views;
using ReactiveUI;
using Splat;

namespace Notification.Avalonia.ViewModel
{
    public class NotificationWindowViewModel : ReactiveObject, IScreen
    {
        private string _title = "this is title";
        private string _text = "this is some text";
       
        private NotificationWindow _window;

        public string Title
        {
            get => _title;
            private set => this.RaiseAndSetIfChanged(ref _title,value);
        }

        public string Text
        {
            get => _text;
            private set => this.RaiseAndSetIfChanged(ref _text,value);
        }

        public void Close()
        {
            _window.TextResult = null;
            _window.ButtonsResult = ButtonsResult.Close;
            _window.Close();
        }
        public RoutingState Router { get; } = new RoutingState();

        public NotificationWindowViewModel(string title,string text,NotificationWindow window,NotificationType notificationType=NotificationType.None)
        {
            
            (Title,Text,_window) = (title,text,window);
            switch (notificationType)
            {
                case NotificationType.None:
                    break;
                case NotificationType.Ok:
                    Router.Navigate.Execute(new ButtonOkViewModel(this,_window));
                    break;
                case NotificationType.YesNo:
                    Router.Navigate.Execute(new ButtonYesNoViewModel(this,_window));
                    break;
                case NotificationType.Input:
                    Router.Navigate.Execute(new InputViewModel(this,_window));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null);
            }
        }
    }
}