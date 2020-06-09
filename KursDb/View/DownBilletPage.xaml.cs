using KursDb.VM;
using System.Windows.Controls;
using System.Windows.Input;


namespace KursDb.View
{
    public partial class DownBilletPage : Page
    {
        ICommand ButtonCommand;

        public DownBilletPage(DownBilletVM VM)
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
