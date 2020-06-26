using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class FittingsVM : ViewModelBase
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }

        public bool IsHaveValues;

        public FittingsVM()
        {
            IsHaveValues = false;
        }

        public FittingsVM(Fitting el)
        {
            if (el == null)
            {
                IsHaveValues = false;
                return;
            }
            Id = el.Id;
            Type = el.Type;
            Price = el.Price;
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
                        var fitting = context.Fittings.Find(Id);

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
