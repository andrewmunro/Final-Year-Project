using Android.App;
using Android.OS;

using MediBook.Client.Android.Fragments;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "MediBook", Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Home);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("AppointmentList", Resource.Drawable.Icon, new AppointmentList());
            AddTab("NotificationList", Resource.Drawable.Icon, new NotificationList());

            if (bundle != null)
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("AppointmentList")));
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("AppointmentList", this.ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            // must set event handler before adding tab
            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += (sender, e) => e.FragmentTransaction.Remove(view);

            this.ActionBar.AddTab(tab);
        }
    }
}

