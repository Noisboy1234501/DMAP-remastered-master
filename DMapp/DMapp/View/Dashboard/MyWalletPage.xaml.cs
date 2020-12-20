using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DMapp.View.Dashboard
{
    /// <summary>
    /// My wallet page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyWalletPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyWalletPage"/> class.
        /// </summary>
        /// 
        MyWalletPage viewModel;
        public MyWalletPage()
        {
            InitializeComponent();
            viewModel = new MyWalletPage();
            BindingContext = viewModel;
        }


    }
}