using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class DownBilletVM : ViewModelBase
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public int Price { get; set; }
        public int Density { get; set; }

        public bool IsHaveValues;

        public DownBilletVM()
        {
            IsHaveValues = false;
        }

        public DownBilletVM(DownBillet el)
        {
            if (el == null)
            {
                IsHaveValues = false;
                return;
            }

            Id = el.Id;
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
                        var down_billet = new DownBillet()
                        {
                            Material = Material,
                            Price = Price,
                            Density = Density
                        };

                        context.DownBillets.Add(down_billet);
                        context.SaveChanges();
                        MessageBox.Show("Запись успешно добавлена", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        var el = context.DownBillets.Find(Id);

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
