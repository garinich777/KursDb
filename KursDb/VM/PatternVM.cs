using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class PatternVM
    {
        int SelectedSex
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
        public int Square { get; set; }
        public int Сomplexity { get; set; }

        public bool IsHaveValues;

        public PatternVM()
        {
            Sex = string.Empty;
            IsHaveValues = false;
        }

        public PatternVM(Pattern el)
        {
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
                        var el = new Pattern()
                        {
                            Sex = Sex,
                            Size = Size,
                            Square = Square,
                            Сomplexity = Сomplexity
                        };

                        context.Patterns.Add(el);
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
                        var el = new Pattern()
                        {
                            Sex = Sex,
                            Size = Size,
                            Square = Square,
                            Сomplexity = Сomplexity
                        };

                        el = context.Patterns.Find(el.Id);

                        Sex = Sex;
                        Size = Size;
                        Square = Square;
                        Сomplexity = Сomplexity;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
