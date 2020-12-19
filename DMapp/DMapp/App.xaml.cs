using DMapp.Models;
using DMapp.View;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DMapp
{
    public partial class App : Application
    {

        public static string DataBase = string.Empty;

       
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }


        public App(string DataBaseLocation)
        {
            InitializeComponent();
            MainPage = new MainPage();
           
            DataBase = DataBaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
    }
}
