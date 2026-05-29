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

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtLogin.Text, 63, "Логин");
        if (error != null)
        {
            return error;
        }

        if (string.IsNullOrWhiteSpace(txtPassword.Password))
        {
            return "Введите пароль.";
        }

        error = ValidationHelper.NoForbiddenChars(txtPassword.Password, "Пароль");
        if (error != null)
        {
            return error;
        }

        return null;
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        var error = ValidateForm();
        if (error != null)
        {
            MessageBox.Show(error);
            return;
        }

        var login = txtLogin.Text.Trim();

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
