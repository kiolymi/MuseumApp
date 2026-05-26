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
        try
        {
            SessionUser.Login = txtLogin.Text.Trim();
            SessionUser.Password = txtPassword.Password;

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
