﻿using System;
using System.Threading.Tasks;

namespace MessengerApp.Services.FirebaseAuth
{
    public interface IFirebaseAuthService
    {
        String getAuthKey();
        bool IsUserSigned();
        Task<bool> SignUp(String email, String password);
        Task<bool> SignIn(String email, String password);
        void SignInWithGoogle();
        Task<bool> SignInWithGoogle(String token);
        Task<bool> Logout();
        String GetUserId();
        String CurrentUser();
    }
}
