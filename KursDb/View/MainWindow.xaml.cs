using KursDb.VM;
using System.Windows;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM VM = new MainVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}
