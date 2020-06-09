using KursDb.VM;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для InsolePage.xaml
    /// </summary>
    public partial class InsolePage : Page
    {
        ICommand ButtonCommand;

        public InsolePage(InsoleVM VM)
        {
            InitializeComponent();
            DataContext = VM;
            if (VM.IsHaveValues)
            {
                ButtonCommand = VM.ModCommand;
                bt_add_mod.Content = "Изменить";
            }
            else
            {
                ButtonCommand = VM.AddCommand;
                bt_add_mod.Content = "Добавить";
            }
        }
    }
}
