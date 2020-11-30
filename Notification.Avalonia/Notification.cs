using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Notification.Avalonia.Enums;
using Notification.Avalonia.ViewModel;
using Notification.Avalonia.Views;
using ReactiveUI;
using Splat;

namespace Notification.Avalonia
{
    public class Notification
    {
        private static Notification _manager;
        object locker = new object();
        private List<NotificationWindow> _notificationWindows = new List<NotificationWindow>();

        private void Add(NotificationWindow window)
        {
            _notificationWindows.Add(window);
            Reposition();
        }

        private void Reposition()
        {
            int count = 0;
            foreach (var window in _notificationWindows)
            {
                if (!window.IsClosed)
                {
                    count++;
                    window.Position = new PixelPoint(
                        window.Screens.Primary.WorkingArea.BottomRight.X - (int) window.Width,
                        window.Screens.Primary.WorkingArea.BottomRight.Y - count * (int) window.Height - 50 -
                        10 * count);
                }
            }
        }

        public static Notification Manager
        {
            get { return _manager ??= new Notification(); }
        }

        private Notification()
        {
            Locator.CurrentMutable.Register(() => new InputView(), typeof(IViewFor<InputViewModel>));
            Locator.CurrentMutable.Register(() => new ButtonOkView(), typeof(IViewFor<ButtonOkViewModel>));
            Locator.CurrentMutable.Register(() => new ButtonYesNoView(), typeof(IViewFor<ButtonYesNoViewModel>));
        }

        public async Task Show(string title, string text, int? timeShow = 5000)
        {
            var w = new NotificationWindow();
            w.ShowInTaskbar = false;
            w.DataContext = new NotificationWindowViewModel(title, text, w);
            w.Show();
            w.Closed += delegate { Reposition(); };
            Add(w);

            if (timeShow != null)
            {
                await Task.Run(async () =>
                {
                    for (int i = 0; i < timeShow / 100; i++)
                    {
                        await Task.Delay(100);
                        if (w.IsClosed) break;
                    }
                });
                if (!w.IsClosed)
                    w.Close();
            }
        }

        public async Task<ButtonsResult> ShowWithButtons(string title, string text, ButtonsType buttonsType,
            int? timeShow = 5000)
        {
            var w = new NotificationWindow();
            switch (buttonsType)
            {
                case ButtonsType.Ok:
                    w.DataContext = new NotificationWindowViewModel(title, text, w, NotificationType.Ok);
                    break;
                case ButtonsType.YesNo:
                    w.DataContext = new NotificationWindowViewModel(title, text, w, NotificationType.YesNo);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonsType), buttonsType, null);
            }

            w.Closed += delegate { Reposition(); };
            w.Show();
            Add(w);
            if (timeShow != null)
            {
                await Task.Run(async () =>
                {
                    for (int i = 0; i < timeShow / 100; i++)
                    {
                        await Task.Delay(100);
                        if (w.IsClosed) break;
                    }
                });
                if (!w.IsClosed)
                    w.Close();
            }
            else
            {
                while (true)
                {
                    await Task.Delay(100);
                    if (w.IsClosed) break;
                }
            }
            return w.ButtonsResult;
        }

        public async Task<string> ShowWithInput(string title, string text,
            int? timeShow = null)
        {
            var w = new NotificationWindow();
            w.DataContext = new NotificationWindowViewModel(title, text, w, NotificationType.Input);
            w.Closed += delegate { Reposition(); };
            w.Show();
            Add(w);
            if (timeShow != null)
            {
                await Task.Run(async () =>
                {
                    for (int i = 0; i < timeShow / 100; i++)
                    {
                        await Task.Delay(100);
                        if (w.IsClosed) break;
                    }
                });
                if (!w.IsClosed)
                    w.Close();
            }
            else
                while (true)
                {
                    await Task.Delay(100);
                    if (w.IsClosed) break;
                }

            return w.TextResult;
        }
    }
}