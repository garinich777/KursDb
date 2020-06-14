using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class DownBilletVM : ViewModelBase
    {
        public string Material { get; set; }
        public int Price { get; set; }
        public int Density { get; set; }

        public bool IsHaveValues;

        public DownBilletVM()
        {
            IsHaveValues = false;
        }

        public DownBilletVM(DownBillet down_billet)
        {
            Material = down_billet.Material;
            Price = down_billet.Price;
            Density = down_billet.Density;
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
                        var down_billet = new DownBillet()
                        {
                            Material = Material,
                            Price = Price,
                            Density = Density
                        };

                        down_billet = context.DownBillets.Find(down_billet.Id);

                        down_billet.Material = Material;
                        down_billet.Price = Price;
                        down_billet.Density = Density;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
