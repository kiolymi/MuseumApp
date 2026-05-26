using System.Windows;
using MuseumApp.Helpers;

namespace MuseumApp;

public partial class App : Application
{
    public App()
    {
        DispatcherUnhandledException += (_, e) =>
        {
            MessageBox.Show(DbErrorHelper.GetMessage(e.Exception));
            e.Handled = true;
        };
    }
}
