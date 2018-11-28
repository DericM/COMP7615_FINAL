using MessengerApp.Models.Message;
using MessengerApp.Services.FirebaseAuth;
using MessengerApp.Services.FirebaseDB;
using MessengerApp.ViewModels.Base;
using MessengerApp.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MessengerApp.ViewModels.Main
{
    class MainViewModel : ViewModelBase
    {
        private ICommand _logoutCommand;
        private ICommand _saveTextCommand;
        private IFirebaseAuthService _firebaseAuthService;
        private IFirebaseDBService _firebaseDatabaseService;
        private String _message;

        public ObservableCollection<Message> Messages { get;  } = new ObservableCollection<Message>();


        public MainViewModel()
        {
            _firebaseAuthService = DependencyService.Get<IFirebaseAuthService>();
            _firebaseDatabaseService = DependencyService.Get<IFirebaseDBService>();
            _firebaseDatabaseService.Connect();
            _firebaseDatabaseService.GetMessages();

            MessagingCenter.Subscribe<String, List<Message>>(this, _firebaseDatabaseService.GetMessageKey(), (sender, args) =>
            {
                List<Message> messages_list = (args);
                Messages.Clear();
                
                foreach (Message m in messages_list)
                {
                    Messages.Add(m);
                }
                RaisePropertyChanged();

            });


        }

        public ICommand LogoutCommand
        {
            get { return _logoutCommand = _logoutCommand ?? new DelegateCommandAsync(LogoutCommandExecute); }
        }

        private async Task LogoutCommandExecute()
        {
            if (await _firebaseAuthService.Logout())
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }


        }

        public ICommand SaveTextCommand
        {
            get { return _saveTextCommand = _saveTextCommand ?? new DelegateCommandAsync(SaveTextCommandExecute); }
        }
        private async Task SaveTextCommandExecute()
        {
            _firebaseDatabaseService.SetMessage(new Message(Message));
            Message = "";
        }

        public String Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }
    }
}
