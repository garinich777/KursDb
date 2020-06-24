using DevExpress.Mvvm;
using KursDb.Properties;
using KursDb.View;
using System;
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

        public Visibility ModelConstVisibility
        {
            get { return GetValue<Visibility>("ModelConstVisibility"); }
            set { SetValue(value, "ModelConstVisibility"); }
        }

        public Visibility OrderVisibility
        {
            get { return GetValue<Visibility>("OrderVisibility"); }
            set { SetValue(value, "OrderVisibility"); }
        }

        public MainVM()
        {
            ModelConstVisibility = Visibility.Visible;
            OrderVisibility = Visibility.Visible;
            if (!Settings.Default.ModelConstPermission)
                ModelConstVisibility = Visibility.Collapsed;
            if (!Settings.Default.OrderPermission)
                OrderVisibility = Visibility.Collapsed;
        }

        public ICommand ModelConstructorPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var VM = new ModelConstructorVM();
                    CorentPage = new ModelConstructorPage(VM);
                    VM.DateUpdate += (s, e) => CorentPage = new ModelConstructorPage(VM);
                });
            }
        }
    }
}
