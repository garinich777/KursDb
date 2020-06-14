using KursDb.VM;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для FittingsPage.xaml
    /// </summary>
    public partial class FittingsPage : Page
    {
        public FittingsPage(FittingsVM VM)
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
