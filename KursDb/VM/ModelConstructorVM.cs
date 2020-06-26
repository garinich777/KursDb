using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using KursDb.Properties;
using KursDb.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursDb.VM
{
    public class FullInfoFittingsInModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }

    public class ModelConstructorVM : ViewModelBase
    {
        public ModelConstructorVM()
        {
            BackButtonVisibility = Visibility.Collapsed;
            FittingInModelList = new List<FullInfoFittingsInModel>();
        }

        public event EventHandler DateUpdate;
        protected virtual void OnDateUpdate(EventArgs e)
        {
            EventHandler handler = DateUpdate;
            handler?.Invoke(this, e);
        }

        public Page CorentPage
        {
            get { return GetValue<Page>("CorentPage"); }
            set { SetValue(value, "CorentPage"); }
        }

        public int TabIndex
        {
            get { return GetValue<int>("TabIndex"); }
            set { SetValue(value, "TabIndex"); }
        }

        public Visibility BackButtonVisibility
        {
            get { return GetValue<Visibility>("BackButtonVisibility"); }
            set { SetValue(value, "BackButtonVisibility"); }
        }

        public int Size
        {
            get { return GetValue<int>("Size"); }
            set
            {
                OnDateUpdate(EventArgs.Empty);
                SetValue(value, "Size");
            }
        }

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
                OnDateUpdate(EventArgs.Empty);
            }
        }

        public string Sex { get; set; }

        #region Main Parametrs
        public DownBillet DownBillet
        {
            get { return GetValue<DownBillet>("DownBillet"); }
            set { SetValue(value, "DownBillet"); }
        }
        public Insole Insole
        {
            get { return GetValue<Insole>("Insole"); }
            set { SetValue(value, "Insole"); }
        }
        public Pattern Pattern
        {
            get { return GetValue<Pattern>("Pattern"); }
            set { SetValue(value, "Pattern"); }
        }
        public ShoeTree ShoeTree
        {
            get { return GetValue<ShoeTree>("ShoeTree"); }
            set { SetValue(value, "ShoeTree"); }
        }
        public Sole Sole
        {
            get { return GetValue<Sole>("Sole"); }
            set { SetValue(value, "Sole"); }
        }
        public UpBillet UpBillet
        {
            get { return GetValue<UpBillet>("UpBillet"); }
            set { SetValue(value, "UpBillet"); }
        }
        public Fitting Fitting
        {
            get { return GetValue<Fitting>("Fitting"); }
            set { SetValue(value, "Fitting"); }
        }
        public FullInfoFittingsInModel FittingsInModel
        {
            get { return GetValue<FullInfoFittingsInModel>("FittingsInModel"); }
            set { SetValue(value, "FittingsInModel"); }
        }
        #endregion Main Parametrs

        #region MainLists
        public List<DownBillet> DownBilletList 
        { 
            get
            {
                using (var context = new UserDbContext())
                {
                    context.DownBillets.Load();
                    return context.DownBillets.Local.ToList();
                }
            }
        }
        public List<Insole> InsoleList
        {
            get
            {
                using (var context = new UserDbContext())
                {
                    context.Insole.Load();                    
                    return context.Insole.Local.ToList().FindAll(n => (n.Sex == Sex || SelectedSex == -1) && (n.Size == Size || Size == 0));
                }
            }
        }
        public List<Pattern> PatternList
        {
            get
            {
                using (var context = new UserDbContext())
                {
                    context.Patterns.Load();
                    return context.Patterns.Local.ToList().FindAll(n => (n.Sex == Sex || SelectedSex == -1) && (n.Size == Size || Size == 0));
                }
            }
        }
        public List<ShoeTree> ShoeTreeList
        {
            get
            {
                using (var context = new UserDbContext())
                {
                    context.ShoeTrees.Load();
                    return context.ShoeTrees.Local.ToList().FindAll(n => (n.Sex == Sex || SelectedSex == -1) && (n.Size == Size || Size == 0));
                }
            }
        }
        public List<Sole> SoleList
        {
            get
            {
                using (var context = new UserDbContext())
                {
                    context.Soles.Load();
                    return context.Soles.Local.ToList().FindAll(n => (n.Sex == Sex || SelectedSex == -1) && (n.Size == Size || Size == 0));
                }
            }
        }
        public List<UpBillet> UpBilletList
        {
            get
            {
                using (var context = new UserDbContext())
                {
                    context.UpBillets.Load();
                    return context.UpBillets.Local.ToList();
                }
            }
        }
        public List<Fitting> FittingList
        {
            get
            {
                using (var context = new UserDbContext())
                {
                    context.Fittings.Load();
                    return context.Fittings.Local.ToList();
                }
            }
        }

        public List<FullInfoFittingsInModel> FittingInModelList { get; set; }
        #endregion MainList

        public ICommand AddFittingInModelClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Fitting == null)
                    {
                        MessageBox.Show("Выберете поле для добавления");
                        return;
                    }                       

                    var dyalog_window = new CountDialog();
                    if (!dyalog_window.ShowDialog().Value)
                        return;

                    var el = new FullInfoFittingsInModel();
                    el.Id = Fitting.Id;
                    el.Price = Fitting.Price;
                    el.Type = Fitting.Type;
                    el.Count = dyalog_window.Count;
                    FittingInModelList.Add(el);
                    OnDateUpdate(EventArgs.Empty);
                });
            }
        }
        public ICommand DeleteFittingInModelClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(!FittingInModelList.Remove(FittingsInModel))
                        MessageBox.Show("Выберете поле для удаления");
                    else
                        OnDateUpdate(EventArgs.Empty);
                });
            }
        }
        public ICommand BackClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CorentPage.Visibility = Visibility.Collapsed;
                    BackButtonVisibility = Visibility.Collapsed;
                    OnDateUpdate(EventArgs.Empty);
                });
            }
        }

        private void StartCorentAddPage(Page page)
        {
            CorentPage = page;
            CorentPage.Visibility = Visibility.Visible;
        }

        private void StartCorentModPage(Page page, bool IsNotNull)
        {
            if (IsNotNull)
            {
                StartCorentAddPage(page);
            }
            else
            {                
                BackButtonVisibility = Visibility.Collapsed;
                MessageBox.Show("Выберете поле для изменения");
            }
        }

        public ICommand AddPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BackButtonVisibility = Visibility.Visible;
                    switch (TabIndex)
                    {
                        case 0:
                            StartCorentAddPage(new DownBilletPage(new DownBilletVM()));                           
                            break;
                        case 1:
                            StartCorentAddPage(new UpBilletPage(new UpBilletVM()));
                            break;
                        case 2:
                            StartCorentAddPage(new InsolePage(new InsoleVM()));
                            break;
                        case 3:
                            StartCorentAddPage(new PatternPage(new PatternVM()));
                            break;
                        case 4:
                            StartCorentAddPage(new ShoeTreePage(new ShoeTreeVM()));
                            break;
                        case 5:
                            StartCorentAddPage(new SolePage(new SoleVM()));
                            break;
                        case 6:
                            StartCorentAddPage(new FittingsPage(new FittingsVM()));
                            break;
                        default:
                            BackButtonVisibility = Visibility.Collapsed;
                            CorentPage.Visibility = Visibility.Collapsed;
                            break;
                    }
                });
            }
        }

        public ICommand ModPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!Settings.Default.AdminPermission)
                    {
                        MessageBox.Show("У вас недостаточно прав на совершения этого действия", "Отказано!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    BackButtonVisibility = Visibility.Visible;
                    switch (TabIndex)
                    {
                        case 0:
                            StartCorentModPage(new DownBilletPage(new DownBilletVM(DownBillet)), DownBillet != null);                                                       
                            break;
                        case 1:
                            StartCorentModPage(new UpBilletPage(new UpBilletVM(UpBillet)), UpBillet != null);
                            break;
                        case 2:
                            StartCorentModPage(new InsolePage(new InsoleVM(Insole)), Insole != null);
                            break;
                        case 3:
                            StartCorentModPage(new PatternPage(new PatternVM(Pattern)), Pattern != null);
                            break;
                        case 4:
                            StartCorentModPage(new ShoeTreePage(new ShoeTreeVM(ShoeTree)), ShoeTree != null);
                            break;
                        case 5:
                            StartCorentModPage(new SolePage(new SoleVM(Sole)), Sole != null);
                            break;
                        case 6:
                            StartCorentModPage(new FittingsPage(new FittingsVM(Fitting)), Fitting != null);
                            break;
                        default:
                            BackButtonVisibility = Visibility.Collapsed;
                            CorentPage.Visibility = Visibility.Collapsed;
                            break;
                    }
                });
            }
        }

        public ICommand DeleteClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!Settings.Default.AdminPermission)
                    {
                        MessageBox.Show("У вас недостаточно прав на совершения этого действия", "Отказано!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                        return;

                    switch (TabIndex)
                    {
                        case 0:
                            if(DownBillet == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.DownBillets.Find(DownBillet.Id);
                                context.DownBillets.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        case 1:
                            if (UpBillet == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.UpBillets.Find(UpBillet.Id);
                                context.UpBillets.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        case 2:
                            if (Insole == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.Insole.Find(Insole.Id);
                                context.Insole.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        case 3:
                            if (Pattern == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.Patterns.Find(Pattern.Id);
                                context.Patterns.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        case 4:
                            if (ShoeTree == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.ShoeTrees.Find(ShoeTree.Id);
                                context.ShoeTrees.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        case 5:
                            if (Sole == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.Soles.Find(Sole.Id);
                                context.Soles.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        case 6:
                            if (Fitting == null)
                                break;
                            using (var context = new UserDbContext())
                            {
                                var el = context.Fittings.Find(Fitting.Id);
                                context.Fittings.Remove(el);
                                context.SaveChanges();
                            }
                            break;
                        default:
                            break;
                    }
                    OnDateUpdate(EventArgs.Empty);
                });
            }
        }
    }
}
