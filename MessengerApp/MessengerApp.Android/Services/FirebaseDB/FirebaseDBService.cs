using System;
using Firebase.Database;
using MessengerApp.Droid.Services.FirebaseAuth;
using MessengerApp.Droid.Services.FirebaseDB;
using MessengerApp.Services.FirebaseDB;
using MessengerApp.Models.Message;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

[assembly: Dependency(typeof(FirebaseDBService))]
namespace MessengerApp.Droid.Services.FirebaseDB
{
    public class ValueEventListener : Java.Lang.Object, IValueEventListener
    {
        public void OnCancelled(DatabaseError error) { }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists())
            {
                List<Message> messages = new List<Message>();
                var obj = snapshot.Children;

                foreach (DataSnapshot snapshotChild in obj.ToEnumerable())
                {
                    if (snapshotChild.GetValue(true) == null) continue;

                    Message msg = new Message();
                    msg.User = snapshotChild.Child("user")?.GetValue(true)?.ToString();
                    msg.Text = snapshotChild.Child("text")?.GetValue(true)?.ToString();
                    msg.Time = snapshotChild.Child("time")?.GetValue(true)?.ToString();
                    messages.Add(msg);
                }
                MessagingCenter.Send(FirebaseDBService.KEY_MESSAGE, FirebaseDBService.KEY_MESSAGE, messages);
            }

        }
    }

    public class FirebaseDBService : IFirebaseDBService
    {
        DatabaseReference databaseReference;
        FirebaseDatabase database;
        FirebaseAuthService authService = new FirebaseAuthService();
        public static String KEY_MESSAGE = "message";

       

        public void Connect()
        {
            database = FirebaseDatabase.GetInstance(MainActivity.app);
        }

        public void GetMessages()
        {
            var userId = authService.GetUserId();

            databaseReference = database.GetReference("messages");
            databaseReference.AddValueEventListener(new ValueEventListener());

        }

        public string GetMessageKey()
        {
            return KEY_MESSAGE;
        }

        public void SetMessage(Message message)
        {
            var userId = authService.GetUserId();
            var user = authService.CurrentUser();
            message.User = user;

            databaseReference = database.GetReference("messages/" + message.Time + "/text");
            databaseReference.SetValue(message.Text);
            databaseReference = database.GetReference("messages/" + message.Time + "/user");
            databaseReference.SetValue(message.User);
            databaseReference = database.GetReference("messages/" + message.Time + "/time");
            databaseReference.SetValue(message.Time);

        }

    }
}