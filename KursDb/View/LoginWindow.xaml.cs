using KursDb.Properties;
using System.Windows;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ProgremStart(object sender, RoutedEventArgs e)
        {
            string password = pb_password.Password;
            string login = tb_login.Text;

            Settings.Default.ModelConstPermission = true;
            Settings.Default.OrderPermission = true;
            Settings.Default.AdminPermission = false;

            string error_message = "Неправильный логин или пароль";
            switch (login)
            {
                case "1":
                    if(password != "1")
                    {
                        MessageBox.Show(error_message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Включен режим администратора!", "Опасно!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Settings.Default.AdminPermission = true;
                    break;
                case "2":
                    if (password != "2")
                    {
                        MessageBox.Show(error_message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    Settings.Default.ModelConstPermission = true;
                    Settings.Default.OrderPermission = false;
                    break;
                case "3":
                    if (password != "3")
                    {
                        MessageBox.Show(error_message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    Settings.Default.ModelConstPermission = false;
                    Settings.Default.OrderPermission = true;
                    break;
                default:
                    MessageBox.Show(error_message, "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
            }
            new MainWindow().Show();
            Close();
        }
    }
}
