
using DMapp.View;

using Xamarin.Forms;


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
