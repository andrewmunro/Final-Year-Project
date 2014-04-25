using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using Gcm.Client;

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

        public string RegistrationID { get { return GcmClient.GetRegistrationId(this); } }

        private ProgressDialog Dialog { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            Dialog = new ProgressDialog(this);
            Dialog.Indeterminate = true;
            Dialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            Dialog.SetCancelable(false);

            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            if (AccountComponent.Token != null) StartActivity(typeof(HomeScreen));
        }

        [Export]
        public async void Login(View view)
        {
            this.Dialog.SetMessage("Logging in...");
            this.Dialog.Show();

            this.HideError();

            try
            {
                await AccountComponent.Login(Username.Text, Password.Text);
                this.Dialog.Dismiss();
                Console.WriteLine("Logged In!");
            }
            catch (AuthException e)
            {
                this.Dialog.Dismiss();
                this.ShowError(e.Message);
                return;
            }

            if (String.IsNullOrEmpty(RegistrationID))
            {
                GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);
            }

            StartActivity(typeof(HomeScreen));
        }

        [Export]
        public async void Register(View view)
        {
            this.Dialog.SetMessage("Registering new account...");
            this.Dialog.Show();

            this.HideError();

            try
            {
                await AccountComponent.Register(Username.Text, Password.Text);
            }
            catch (RegistrationException e)
            {
                this.Dialog.Dismiss();
                this.ShowError(e.ErrorMessage);
                return;
            }
            this.Login(view);
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