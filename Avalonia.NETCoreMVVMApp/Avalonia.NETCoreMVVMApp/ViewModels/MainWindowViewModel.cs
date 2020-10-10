using System;
using System.Collections.Generic;
using System.Text;
using Notification = Notification.Avalonia.Notification;

namespace Avalonia.NETCoreMVVMApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public void Show()
        {
            global::Notification.Avalonia.Notification.Manager.Show("kek", "peck");
        }
    }
}