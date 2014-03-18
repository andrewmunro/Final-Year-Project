using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using Java.Interop;

using MediBook.Client.Core.Components.Account;
using MediBook.Client.Core.Exceptions;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "MediBook", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginScreen : Activity
    {

        public AccountComponent AccountComponent { get { return App.AppCore.GetComponent<AccountComponent>(); } }

        public EditText Username { get { return FindViewById<EditText>(Resource.Id.usernameInput); } }
        public EditText Password { get { return FindViewById<EditText>(Resource.Id.passwordInput); } }
        public TextView ErrorText { get { return FindViewById<TextView>(Resource.Id.errorText); } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
        }

        [Export]
        public async void Login(View view)
        {
            this.HideError();
            try
            {
                await AccountComponent.Login(Username.Text, Password.Text);
                Console.WriteLine("Logged In!");
            }
            catch (AuthException e)
            {
                this.ShowError(e.Message);
            }
        }

        [Export]
        public async void Register(View view)
        {
            this.HideError();

            try
            {
                await AccountComponent.Register(Username.Text, Password.Text);
                this.Login(view);
            }
            catch (RegistrationException e)
            {
                this.ShowError(e.ErrorMessage);
            }
        }

        private void HideError()
        {
            ErrorText.Visibility = ViewStates.Invisible;
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = ViewStates.Visible;
        }
    }
}