using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;


namespace KursDb.VM
{
    public class UpBilletVM : ViewModelBase
    {
        string Material { get; set; }
        int Price { get; set; }
        int Density { get; set; }

        public bool IsHaveValues;

        public UpBilletVM()
        {
            IsHaveValues = false;
        }

        public UpBilletVM(DownBillet down_billet)
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
                        var down_billet = new UpBillet()
                        {
                            Material = Material,
                            Price = Price,
                            Density = Density
                        };

                        context.UpBillets.Add(down_billet);
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
                        var down_billet = new UpBillet()
                        {
                            Material = Material,
                            Price = Price,
                            Density = Density
                        };

                        down_billet = context.UpBillets.Find(down_billet.Id);

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
