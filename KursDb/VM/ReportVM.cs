using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using KursDb.Model;
using KursDb.Model.Tables;
using KursDb.Properties;
using KursDb.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    enum Filter { Daily, Weekly, Monthly, Yearly, AllTime}
    class OrderInfo
    {
        public int Id { get; set; }
        public string Sum { get; set; }
        public DateTime Date { get; set; }
        public int LeadTime { get; set; }
        public string Models { get; set; }
    }

    public class ReportVM : ViewModelBase
    {
        public event EventHandler DateUpdate;
        protected virtual void OnDateUpdate(EventArgs e)
        {
            EventHandler handler = DateUpdate;
            handler?.Invoke(this, e);
        }

        private void Initialize()
        {
            OrderList = new List<OrderInfo>();
            using (var context = new UserDbContext())
            {
                context.ShoeModels.Load();
                context.ModelsInOrder.Load();
                context.Orders.Load();
                foreach (var item in context.Orders)
                {
                    
                    string models = string.Empty;
                    foreach (var item2 in context.ShoeModels)
                    {
                        var model = context.ModelsInOrder.Find(item.Id, item2.Id);
                        if (model  != null)
                            models += $"\"{item2.Id}\" x{model?.Count}| ";
                    }

                    OrderList.Add(new OrderInfo
                    {
                        Date = item.Date,
                        Id = item.Id,
                        LeadTime = item.HoursComplet,
                        Sum = item.Sum,
                        Models = models
                    });
                }
            }
        }

        public ReportVM()
        {
            Initialize();
        }

        public int SelectedFilter
        {
            get { return GetValue<int>("SelectedFilter"); }
            set 
            { 
                SetValue(value, "SelectedFilter");
                Initialize();
                OnDateUpdate(EventArgs.Empty);
            }
        }
        public List<OrderInfo> OrderList { get; set; }
        public OrderInfo Order
        {
            get { return GetValue<OrderInfo>("Order"); }
            set { SetValue(value, "Order"); }
        }

        public ICommand DeleatOrderClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!Settings.Default.AdminPermission)
                    {
                        MessageBox.Show("У вас недостаточно прав на совершения этого действия", "Отказано!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Order == null)
                    {
                        MessageBox.Show("Выберете запись");
                        return;
                    }

                    MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                        return;

                    using (var context = new UserDbContext())
                    {
                        var el = context.Orders.Find(Order.Id);
                        context.Orders.Remove(el);
                        context.SaveChanges();
                    }
                    Initialize();
                    OnDateUpdate(EventArgs.Empty);
                });
            }
        }
    }
}
