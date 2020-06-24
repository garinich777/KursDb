using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using System.Windows;
using System.Windows.Input;

namespace KursDb.VM
{
    public class SoleVM : ViewModelBase
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
        public int SelectedSeason
        {
            get
            {
                switch (Season)
                {
                    case "Л":
                        return (int)SeasonEnum.Summer;
                    case "О":
                        return (int)SeasonEnum.Autumn;
                    case "З":
                        return (int)SeasonEnum.Winter;
                    case "В":
                        return (int)SeasonEnum.Spring;                        
                    default:
                        return (int)SeasonEnum.Default;
                }
            }
            set
            {
                switch (value)
                {
                    case (int)SeasonEnum.Summer:
                        Season = "Л";
                        break;
                    case (int)SeasonEnum.Autumn:
                        Season = "О";
                        break;
                    case (int)SeasonEnum.Winter:
                        Season = "З";
                        break;
                    case (int)SeasonEnum.Spring:
                        Season = "В";
                        break;
                    default:
                        Season = string.Empty;
                        break;
                }
            }
        }
        public string Type { get; set; }
        public string Season { get; set; }
        public string Sex { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }

        public bool IsHaveValues;

        public SoleVM()
        {
            Sex = string.Empty;
            Season = string.Empty;
            IsHaveValues = false;
        }

        public SoleVM(Sole el)
        {
            if (el == null)
            {
                IsHaveValues = false;
                return;
            }
            Id = el.Id;
            Sex = el.Sex;
            Price = el.Price;
            Type = el.Type;
            Season = el.Season;
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
                        var el = new Sole()
                        {
                            Sex = Sex,
                            Price = Price,
                            Type = Type,
                            Season = Season,
                            Size = Size,
                        };

                        context.Soles.Add(el);
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
                        var el = context.Soles.Find(Id);

                        el.Sex = Sex;
                        el.Price = Price;
                        el.Type = Type;
                        el.Season = Season;
                        el.Size = Size;

                        context.SaveChanges();
                        MessageBox.Show("Запись изменена");
                    }
                });
            }
        }
    }
}
