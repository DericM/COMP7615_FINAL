using MessengerApp.ViewModels.Main;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerApp.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
        MainViewModel viewModel;
        public MainView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            viewModel = BindingContext as MainViewModel;

            if (viewModel != null) viewModel.OnAppearing(null);
        }
    }
}