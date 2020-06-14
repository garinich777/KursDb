﻿using DevExpress.Mvvm;
using KursDb.Model;
using KursDb.Model.Tables;
using KursDb.Properties;
using KursDb.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Page CorentPage
        {
            get { return GetValue<Page>("CorentPage"); }
            set { SetValue(value, "CorentPage"); }
        }

        public Visibility BackButtonVisibility
        {
            get { return GetValue<Visibility>("BackButtonVisibility"); }
            set { SetValue(value, "BackButtonVisibility"); }
        }

        public int Size
        {
            get { return GetValue<int>("Size"); }
            set { SetValue(value, "Size"); }
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
            }
        }
        public string Sex { get; set; }

        public List<DownBillet> DownBillet 
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
        public List<Insole> Insole { get; set; }
        public List<Pattern> Pattern { get; set; }
        public List<ShoeTree> ShoeTree { get; set; }
        public List<Sole> Sole { get; set; }
        public List<UpBillet> UpBillet { get; set; }

        public ICommand BackClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CorentPage.Visibility = Visibility.Collapsed;
                    BackButtonVisibility = Visibility.Collapsed;
                });
            }
        }

        public ICommand AddPageClick
        {
            get
            {
                return new DelegateCommand<int>((selected_index) =>
                {
                    BackButtonVisibility = Visibility.Visible;
                    switch (selected_index)
                    {
                        case 0:
                            CorentPage = new DownBilletPage(new DownBilletVM());
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