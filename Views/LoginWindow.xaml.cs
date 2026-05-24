using System.Windows;
using System.Windows.Controls;
using MuseumApp.Data;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        cmbRole.SelectedIndex = 0;
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            SessionUser.Login = txtLogin.Text.Trim();
            SessionUser.Password = txtPassword.Password;
            SessionUser.Role = (cmbRole.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "";

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
