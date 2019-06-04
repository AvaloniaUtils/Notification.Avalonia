using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Notification.Avalonia.Enums;
using Notification.Avalonia.ViewModel;
using ReactiveUI;

namespace Notification.Avalonia.Views
{
    public class NotificationWindow : ReactiveWindow<NotificationWindowViewModel>
    {
      

        public string TextResult { get; set; }

        public ButtonsResult ButtonsResult { get; set; }
        public bool IsClosed { get; private set; }

        public NotificationWindow()
        {
            this.WhenActivated(disposables => { });
            InitializeComponent();
            //TODO: do smh with code below
            /*Closing +=  (sender, args) =>
            {
                double op = 1;
                for (int i = 0; i < 10; i++)
                {
                    op-= 0.1;
                    var t =Task.Delay(100);
                    t.Wait();
                    Opacity = op;
                }
                
            };*/
            Closed += delegate { IsClosed = true; };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}