using System;
using Firebase.Database;
using MessengerApp.Droid.Services.FirebaseAuth;
using MessengerApp.Droid.Services.FirebaseDB;
using MessengerApp.Services.FirebaseDB;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseDBService))]
namespace MessengerApp.Droid.Services.FirebaseDB
{
    public class ValueEventListener : Java.Lang.Object, IValueEventListener
    {
        public void OnCancelled(DatabaseError error) { }

        public void OnDataChange(DataSnapshot snapshot)
        {

            try {
                String message = snapshot.Value.ToString();
                MessagingCenter.Send(FirebaseDBService.KEY_MESSAGE, FirebaseDBService.KEY_MESSAGE, message);
            }
            catch (Exception)
            {

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

        public void GetMessage()
        {
            var userId = authService.GetUserId();
            databaseReference = database.GetReference("messages/" + userId);
            databaseReference.AddValueEventListener(new ValueEventListener());

        }

        public string GetMessageKey()
        {
            return KEY_MESSAGE;
        }

        public void SetMessage(string message)
        {
            var userId = authService.GetUserId();
            databaseReference = database.GetReference("messages/" + userId);
            databaseReference.SetValue(message);
        }
    }
}