using DevExpress.Mvvm;
using KursDb.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursDb.VM
{
    public class MainVM : ViewModelBase
    {
        public Page CorentPage
        {
            get { return GetValue<Page>("CorentPage"); }
            set { SetValue(value, "CorentPage"); }
        }

        public Visibility Visibility
        {
            get { return GetValue<Visibility>("Visibility"); }
            set { SetValue(value, "Visibility"); }
        }

        public MainVM()
        {
            CorentPage = new DownBilletPage(new DownBilletVM());
            Visibility = Visibility.Collapsed;
        }
    }
}
