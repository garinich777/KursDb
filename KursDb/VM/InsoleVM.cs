using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class InsoleVM : ViewModelBase
    {
        public int Id { get; set; }
        public int SelectedSex
        {
            get 
            {
                switch (Sex)
                {
                    case "M":
                        return (int)SexEnum.Male;
                    case "Ж":
                        return (int)SexEnum.Female;
                    case "Д":
                        return (int)SexEnum.Children;
                    default:
                        return (int)SexEnum.Default;
                }
            }
            set 
            {
                switch (value)
                {
                    case (int)SexEnum.Male:
                        Sex = "M";
                        break;
                    case (int)SexEnum.Female:
                        Sex = "Ж";
                        break;
                    case (int)SexEnum.Children:
                        Sex = "Д";
                        break;
                    default:
                        Sex = string.Empty;
                        break;
                }
            } 
        }
        public string Sex { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }

        public bool IsHaveValues;

        public InsoleVM()
        {
            Sex = string.Empty;
            IsHaveValues = false;
        }

        public InsoleVM(Insole el)
        {
            if (el == null)
            {
                IsHaveValues = false;
                return;
            }
            Id = el.Id;
            Sex = el.Sex;
            Size = el.Size;
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
                        var insole = new Insole()
                        {
                            Sex = Sex,
                            Size = Size,
                            Price = Price,
                        };

                        context.Insole.Add(insole);
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
                        var el = context.Insole.Find(Id);

                        el.Sex = Sex;
                        el.Size = Size;
                        el.Price = Price;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
