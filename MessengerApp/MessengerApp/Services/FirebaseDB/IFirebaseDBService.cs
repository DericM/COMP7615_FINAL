using MessengerApp.Models.Message;
using System;

namespace MessengerApp.Services.FirebaseDB
{
    public interface IFirebaseDBService
    {
        void Connect();
        void GetMessages();
        void SetMessage(Message message);
        String GetMessageKey();
    }
}
