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

            Settings.Default.AddMaterialsPermission = true;
            Settings.Default.AddModelPermission = true;
            Settings.Default.AddOrderPermission = true;
            Settings.Default.ModDelMaterialsPermission = true;
            Settings.Default.ModDelModelPermission = true;
            Settings.Default.ModDelOrderPermission = true;
            Settings.Default.ViewMaterialsPermission = true;
            Settings.Default.ViewModelPermission = true;
            Settings.Default.ViewOrderPermission = true;

            switch (login)
            {
                case "1":
                    MessageBox.Show("Включен режим администратора!", "Опасно!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "2":
                    Settings.Default.AddOrderPermission = false;
                    Settings.Default.ModDelMaterialsPermission = false;
                    Settings.Default.ModDelModelPermission = false;
                    Settings.Default.ModDelOrderPermission = false;
                    Settings.Default.ViewOrderPermission = false;
                    break;
                case "3":
                    Settings.Default.AddMaterialsPermission = false;
                    Settings.Default.AddModelPermission = false;
                    Settings.Default.ModDelMaterialsPermission = false;
                    Settings.Default.ModDelModelPermission = false;
                    Settings.Default.ModDelOrderPermission = false;
                    Settings.Default.ViewMaterialsPermission = false;
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
