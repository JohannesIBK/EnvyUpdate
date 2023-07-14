﻿using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation.Collections;
using Wpf.Ui.Markup;

namespace EnvyUpdate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Wpf.Ui.Appearance.Accent.ApplySystemAccent();
            ThemesDictionary themedict = new ThemesDictionary();
            if (Util.IsDarkTheme())
                themedict.Theme = Wpf.Ui.Appearance.ThemeType.Dark;
            else
                themedict.Theme = Wpf.Ui.Appearance.ThemeType.Light;
            Application.Current.Resources.MergedDictionaries.Add(themedict);

            // Listen to notification activation
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                // Need to dispatch to UI thread if performing UI operations
                Application.Current.Dispatcher.Invoke(delegate
                {
                    switch (args.Get("action"))
                    {
                        case "download":
                            Process.Start(Util.GetGpuUrl());
                            break;
                        default:
                            Util.ShowMain();
                            break;
                    }
                });
            };
        }
    }
}
