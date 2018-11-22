
using MessengerApp.ViewModels.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        LoginViewModel viewModel;
        public LoginView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            viewModel = BindingContext as LoginViewModel;

            if (viewModel != null) viewModel.OnAppearing(null);
        }
    }
}