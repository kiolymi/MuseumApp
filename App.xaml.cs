using System.Windows;

namespace MuseumApp;

public partial class App : Application
{
    public App()
    {
        DispatcherUnhandledException += (_, e) =>
        {
            MessageBox.Show("Ошибка: " + e.Exception.Message);
            e.Handled = true;
        };
    }
}
