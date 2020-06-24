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
    public class ModelConstructorVM : ViewModelBase
    {
        public ModelConstructorVM()
        {
            BackButtonVisibility = Visibility.Collapsed;
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
        #endregion Main Parametrs

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
        #endregion MainList

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
                        default:
                            BackButtonVisibility = Visibility.Collapsed;
                            CorentPage.Visibility = Visibility.Collapsed;
                            break;
                    }
                });
            }
        }

        public ICommand DeletePageClick
        {
            get
            {
                return new DelegateCommand<int>((selected_index) =>
                {
                    BackButtonVisibility = Visibility.Visible;
                    switch (selected_index)
                    {
                        case 0:
                            DownBilletVM VM = new DownBilletVM();
                            CorentPage = new DownBilletPage(VM);
                            CorentPage.Visibility = Visibility.Visible;
                            break;
                        case 1:
                            CorentPage = new UpBilletPage(new UpBilletVM());
                            CorentPage.Visibility = Visibility.Visible;
                            break;
                        case 2:
                            CorentPage = new InsolePage(new InsoleVM());
                            CorentPage.Visibility = Visibility.Visible;
                            break;
                        case 3:
                            CorentPage = new PatternPage(new PatternVM());
                            CorentPage.Visibility = Visibility.Visible;
                            break;
                        case 4:
                            CorentPage = new ShoeTreePage(new ShoeTreeVM());
                            CorentPage.Visibility = Visibility.Visible;
                            break;
                        case 5:
                            CorentPage = new SolePage(new SoleVM());
                            CorentPage.Visibility = Visibility.Visible;
                            break;
                        default:
                            BackButtonVisibility = Visibility.Collapsed;
                            CorentPage.Visibility = Visibility.Collapsed;
                            break;
                    }
                });
            }
        }

    }
}
