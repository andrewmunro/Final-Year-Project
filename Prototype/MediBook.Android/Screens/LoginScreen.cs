using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using Java.Interop;

using MediBook.Client.Core.Components.Account;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "MediBook", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginScreen : Activity
    {

        public AccountComponent AccountComponent {
            get
            {
                return App.AppCore.GetComponent<AccountComponent>();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
        }

        [Export]
        public async void Login(View view)
        {
            var username = FindViewById<EditText>(Resource.Id.usernameInput);
            var password = FindViewById<EditText>(Resource.Id.passwordInput);

            var status = await AccountComponent.Login(username.Text, password.Text);
        }
    }
}