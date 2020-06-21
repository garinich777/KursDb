using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;


namespace KursDb.VM
{
    public class UpBilletVM : ViewModelBase
    {
        public string Material { get; set; }
        public int Price { get; set; }
        public int Density { get; set; }

        public bool IsHaveValues;

        public UpBilletVM()
        {
            IsHaveValues = false;
        }

        public UpBilletVM(UpBillet el)
        {
            Material = el.Material;
            Price = el.Price;
            Density = el.Density;
            IsHaveValues = true;
        }

        public ICommand AddCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var context = new UserDbContext())
                    {
                        var el = new UpBillet()
                        {
                            Material = Material,
                            Price = Price,
                            Density = Density
                        };

                        context.UpBillets.Add(el);
                        context.SaveChanges();
                        MessageBox.Show("Запись добавлена");
                    }
                });
            }
        }

        public ICommand ModCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var context = new UserDbContext())
                    {
                        var el = new UpBillet()
                        {
                            Material = Material,
                            Price = Price,
                            Density = Density
                        };

                        el = context.UpBillets.Find(el.Id);

                        el.Material = Material;
                        el.Price = Price;
                        el.Density = Density;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
