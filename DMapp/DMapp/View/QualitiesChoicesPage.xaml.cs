﻿using DMapp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DMapp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QualitiesChoicesPage : ContentPage
    {
        
        QualitiesChoicesVM viewModel;
        public QualitiesChoicesPage(INavigation navi)
        {
            InitializeComponent();
            viewModel = new QualitiesChoicesVM(navi);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.SiteDisplayed();
        }
    }
}