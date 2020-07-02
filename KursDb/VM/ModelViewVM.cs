using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Properties;
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
    public class ModelViewVM : ViewModelBase
    {
        public List<ModelViewProp> ModelPropList { get; set; }

        public ModelViewProp ModelViewProp
        {
            get { return GetValue<ModelViewProp>("ModelViewProp"); }
            set { SetValue(value, "ModelViewProp"); }
        }

        public ModelViewVM()
        {
            ModelPropList = new List<ModelViewProp>();
            using (var context = new UserDbContext())
            {
                context.ShoeModels.Load();
                foreach (var item in context.ShoeModels)
                {
                    int price = item.Pattern.Square * (item.DownBillet.Price + item.UpBillet.Price) + item.Insole.Price + item.Sole.Price;
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
                });
            }
        }

    }
}
