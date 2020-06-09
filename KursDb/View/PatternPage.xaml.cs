using KursDb.VM;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursDb.View
{
    /// <summary>
    /// Логика взаимодействия для PatternPage.xaml
    /// </summary>
    public partial class PatternPage : Page
    {
        ICommand ButtonCommand;
        public PatternPage(PatternVM VM)
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
