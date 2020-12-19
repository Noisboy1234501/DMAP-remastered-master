using DMapp.ViewModel;
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
    public partial class ItemsPage : ContentPage
    {
        ItemsVM viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            viewModel = new ItemsVM(Navigation);
            BindingContext = viewModel;
        }

       
        protected override void OnAppearing()
        {
            viewModel.loadPickerOptions();
            DecisionsListView.SelectedItem = null;
            
            viewModel.PageAppeared();
            
        }
    }
}