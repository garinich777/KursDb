using KursDb.VM;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для ShoeTreePage.xaml
    /// </summary>
    public partial class ShoeTreePage : Page
    {
        public ShoeTreePage(ShoeTreeVM VM)
        {
            InitializeComponent();
            DataContext = VM;
            if (VM.IsHaveValues)
            {
                bt_add_mod.Command = VM.ModCommand;
                bt_add_mod.Content = "Изменить";
            }
            else
            {
                bt_add_mod.Command = VM.AddCommand;
                bt_add_mod.Content = "Добавить";
            }
        }
    }
}
