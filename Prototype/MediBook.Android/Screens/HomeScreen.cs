using Android.App;
using Android.OS;
using Android.Widget;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "MediBook", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {
        private ListView Appointments { get { return this.FindViewById<ListView>(Resource.Id.AppointmentList); } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);
        }
    }
}

