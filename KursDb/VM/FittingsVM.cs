using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class FittingsVM
    {
        string Type { get; set; }
        int Price { get; set; }

        public bool IsHaveValues;

        public FittingsVM()
        {
            IsHaveValues = false;
        }

        public FittingsVM(Fitting down_billet)
        {
            Type = down_billet.Type;
            Price = down_billet.Price;
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
                        var fitting = new Fitting()
                        {
                            Type = Type,
                            Price = Price,
                        };

                        context.Fittings.Add(fitting);
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
                        var fitting = new Fitting()
                        {
                            Type = Type,
                            Price = Price,
                        };

                        fitting = context.Fittings.Find(fitting.Id);

                        fitting.Type = Type;
                        fitting.Price = Price;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
