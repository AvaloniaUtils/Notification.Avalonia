using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Notification.Avalonia.ViewModel;
using ReactiveUI;

namespace Notification.Avalonia.Views
{
    public class ButtonYesNoView : ReactiveUserControl<ButtonYesNoViewModel>
    {
        public ButtonYesNoView()
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