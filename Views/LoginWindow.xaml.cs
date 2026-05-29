using System.Windows;
using System.Windows.Media.Imaging;
using MuseumApp.Data;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/logo.png", UriKind.Absolute));
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        var login = txtLogin.Text.Trim();
        if (string.IsNullOrEmpty(login))
        {
            MessageBox.Show("Введите логин.");
            return;
        }

        if (!RoleHelper.IsKnownLogin(login))
        {
            MessageBox.Show("Такой учетной записи не существует", "Ошибка входа",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            SessionUser.Login = login;
            SessionUser.Password = txtPassword.Password;
            SessionUser.Role = RoleHelper.ResolveRole(login);

            if (string.IsNullOrWhiteSpace(SessionUser.Login))
            {
                MessageBox.Show("Введите логин.");
                return;
            }

            using (var context = new MuseumDbContext())
            {
                if (!context.Database.CanConnect())
                {
                    MessageBox.Show("Не удалось подключиться к базе данных.");
                    return;
                }
            }

            SessionUser.Role = SessionUser.ResolveRoleFromDatabase();
            if (string.IsNullOrEmpty(SessionUser.Role))
            {
                MessageBox.Show("У пользователя нет роли admin_museum, curator_museum или cashier_museum.");
                return;
            }

            var main = new MainWindow();
            main.Show();
            Close();
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }
}
