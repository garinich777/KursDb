using DevExpress.Mvvm;
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
    public class FullModelViewProp
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public string Sex { get; set; }
        public string Season { get; set; }
        public string UpMat { get; set; }
        public string DownMat { get; set; }
        public int Price { get; set; }
        public int LaborCosts { get; set; }
        public int Count { get; set; }
    }

    public class ModelViewVM : ViewModelBase
    {
        public ModelViewVM()
        {
            Initialize();
            FullModelPropList = new List<FullModelViewProp>();
        }

        private void Initialize()
        {
            ModelPropList = new List<ModelViewProp>();
            using (var context = new UserDbContext())
            {
                context.ShoeModels.Load();           
                foreach (var item in context.ShoeModels)
                {
                    int price = item.Pattern.Square * (item.DownBillet.Price + item.UpBillet.Price) + item.Insole.Price + item.Sole.Price;
                    if (MaxPrice > 0 && price > MaxPrice)
                        continue;
                    int labor_costs = item.Pattern.Сomplexity * (item.UpBillet.Density + item.DownBillet.Density);
                    var model_prop = new ModelViewProp
                    {
                        Id = item.Id,
                        DownMat = item.DownBillet.Material,
                        UpMat = item.UpBillet.Material,
                        Size = item.Size,
                        Sex = item.Sex,
                        Season = item.Sole.Season,
                        Price = price,
                        LaborCosts = labor_costs
                    };
                    ModelPropList.Add(model_prop);
                }
            }
        }
        public event EventHandler DateUpdate;
        protected virtual void OnDateUpdate(EventArgs e)
        {
            EventHandler handler = DateUpdate;
            handler?.Invoke(this, e);
        }

        public List<ModelViewProp> ModelPropList { get; set; }
        public List<FullModelViewProp> FullModelPropList { get; set; }

        public ModelViewProp ModelViewProp
        {
            get { return GetValue<ModelViewProp>("ModelViewProp"); }
            set { SetValue(value, "ModelViewProp"); }
        }

        public FullModelViewProp FullModelViewProp
        {
            get { return GetValue<FullModelViewProp>("FullModelViewProp"); }
            set { SetValue(value, "FullModelViewProp"); }
        }

        public int PriceCoefficient
        {
            get { return GetValue<int>("PriceCoefficient"); }
            set { SetValue(value, "PriceCoefficient"); }
        }

        public int MaxPrice
        {
            get { return GetValue<int>("MaxPrice"); }
            set
            {
                SetValue(value, "MaxPrice");
                Initialize();
                OnDateUpdate(EventArgs.Empty);                
            }
        }

        public ICommand DeleatModelClick
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

                    if (ModelViewProp == null)
                    {
                        MessageBox.Show("Выберете запись");
                        return;
                    }

                    MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                        return;

                    using (var context = new UserDbContext())
                    {
                        var el = context.ShoeModels.Find(ModelViewProp.Id);
                        context.ShoeModels.Remove(el);
                        context.SaveChanges();
                    }
                    OnDateUpdate(EventArgs.Empty);
                });
            }
        }

        public ICommand AddModelInOrderClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (ModelViewProp == null)
                    {
                        MessageBox.Show("Выберете поле для добавления");
                        return;
                    }

                    var dyalog_window = new CountDialog();
                    if (!dyalog_window.ShowDialog().Value)
                        return;

                    var el = new FullModelViewProp
                    {
                        Id = ModelViewProp.Id,
                        Price = ModelViewProp.Price,
                        Count = dyalog_window.Count,
                        DownMat = ModelViewProp.DownMat,
                        UpMat = ModelViewProp.UpMat,
                        LaborCosts = ModelViewProp.LaborCosts,
                        Season = ModelViewProp.Season,
                        Sex = ModelViewProp.Sex,
                        Size = ModelViewProp.Size
                    };
                    FullModelPropList.Add(el);
                    OnDateUpdate(EventArgs.Empty);
                });
            }
        }

        public ICommand DeleteModelInOrderClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!FullModelPropList.Remove(FullModelViewProp))
                        MessageBox.Show("Выберете поле для удаления");
                    else
                        OnDateUpdate(EventArgs.Empty);
                });
            }
        }

        public ICommand СreateOrderClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var context = new UserDbContext())
                    {
                        int sum = 0;
                        int hours_complet = 0;
                        foreach (var item in FullModelPropList)
                        {
                            sum += item.Count * item.Price;
                            hours_complet += item.Count * item.LaborCosts;
                        }

                        Order order = new Order
                        {
                            Date = DateTime.Now,
                            Sum = (PriceCoefficient * sum / 100).ToString(),
                            HoursComplet = hours_complet
                        };

                        context.Orders.Add(order);
                        context.SaveChanges();

                        int order_id = order.Id;

                        foreach (var item in FullModelPropList)
                        {
                            context.ModelsInOrder.Add(new ModelInOrder()
                            {
                                OrderId = order_id,
                                Count = item.Count,
                                ModelId = item.Id
                            });
                        }
                        context.SaveChanges();
                        MessageBox.Show("Заказ успешно создан");
                    }
                });  
            }
        }
    }
}
