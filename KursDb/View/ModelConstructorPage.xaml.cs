using KursDb.VM;
using System.Windows.Controls;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для ModelConstructorPage.xaml
    /// </summary>
    public partial class ModelConstructorPage : Page
    {
        public ModelConstructorPage(ModelConstructorVM VM)
        {
            InitializeComponent();
            DataContext = VM;
        }
    }
}
