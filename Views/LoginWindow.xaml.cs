using System.Windows;
using MuseumApp.Data;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        var login = txtLogin.Text.Trim();
        if (string.IsNullOrEmpty(login))
        {
            MessageBox.Show("Введите логин.");
            return;
        }

        try
        {
            SessionUser.Login = login;
            SessionUser.Password = txtPassword.Password;
            SessionUser.Role = RoleHelper.ResolveRole(login);

            using var context = new MuseumDbContext();
            context.Database.CanConnect();

            var main = new MainWindow();
            main.Show();
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка входа: " + ex.Message);
        }
    }
}
