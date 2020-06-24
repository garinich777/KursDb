using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;


namespace KursDb.VM
{
    public class ShoeTreeVM : ViewModelBase
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
        public string Type { get; set; }
        public string Sex { get; set; }
        public int Size { get; set; }

        public bool IsHaveValues;

        public ShoeTreeVM()
        {
            Sex = string.Empty;
            IsHaveValues = false;
        }

        public ShoeTreeVM(ShoeTree el)
        {
            if (el == null)
            {
                IsHaveValues = false;
                return;
            }
            Id = el.Id;
            Type = el.Type;
            Sex = el.Sex;
            Size = el.Size;
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
                        var el = new ShoeTree()
                        {
                            Type = Type,
                            Sex = Sex,
                            Size = Size,
                        };

                        context.ShoeTrees.Add(el);
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
                        var el = context.ShoeTrees.Find(Id);

                        el.Type = Type;
                        el.Sex = Sex;
                        el.Size = Size;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
