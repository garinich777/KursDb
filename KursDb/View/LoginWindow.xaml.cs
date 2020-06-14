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

            switch (login)
            {
                case "1":
                    MessageBox.Show("Включен режим администратора!", "Опасно!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Settings.Default.AdminPermission = true;
                    break;
                case "2":
                    Settings.Default.ModelConstPermission = true;
                    Settings.Default.OrderPermission = false;
                    break;
                case "3":
                    Settings.Default.ModelConstPermission = false;
                    Settings.Default.OrderPermission = true;
                    break;
                default:
                    MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
            }
            new MainWindow().Show();
            Close();
        }
    }
}
