using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Notification.Avalonia.ViewModel;
using ReactiveUI;

namespace Notification.Avalonia.Views
{
    public class ButtonOkView : ReactiveUserControl<ButtonOkViewModel>
    {
        public ButtonOkView()
        {
            this.WhenActivated(disposables => { });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}