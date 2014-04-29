using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;

using MediBook.Client.Android.Fragments;
using MediBook.Client.Core;
using MediBook.Client.Core.Components.Account;

using ActionBar = Android.App.ActionBar;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "MediBook", Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {
        public AccountComponent AccountComponent { get { return AppCore.Instance.GetComponent<AccountComponent>(); } }

        public AppointmentList AppointmentList { get; set; }
        public NotificationList NotificationList { get; set; }

        private IMenuItem RefreshMenuItem { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            this.SetTheme(Resource.Style.Theme_AppCompat);
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.HomeScreen);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AppointmentList = new AppointmentList();
            NotificationList = new NotificationList();

            AddTab("Appointments", AppointmentList);
            AddTab("Notifications", NotificationList);

            if (bundle != null)
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("AppointmentList")));
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (RefreshMenuItem != null)
            {
                AppointmentList.RefreshItems(RefreshMenuItem);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.home_screen_menu, menu);

            RefreshMenuItem = menu.FindItem(Resource.Id.refresh_button);

            AppointmentList.RefreshItems(RefreshMenuItem);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.logout_button:
                    AccountComponent.Logout();
                    this.Finish();
                    return true;
                case Resource.Id.refresh_button:
                    AppointmentList.RefreshItems(RefreshMenuItem);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("AppointmentList", this.ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

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

