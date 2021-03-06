using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using MediBook.Client.Android.Fragments.Adapters;
using MediBook.Client.Android.Screens;
using MediBook.Client.Core;
using MediBook.Client.Core.Components.Appointment;

namespace MediBook.Client.Android.Fragments
{
    public class AppointmentList : ListFragment
    {
        private AppointmentComponent AppointmentComponent { get { return AppCore.Instance.GetComponent<AppointmentComponent>(); } }

        public async void RefreshItems(IMenuItem refreshMenuItem)
        {
            ToggleSpinner(true, refreshMenuItem);
            var appointments = await AppointmentComponent.UpdateAppointments();
            this.ListAdapter = new AppointmentListAdapter(Activity, appointments);
            ToggleSpinner(false, refreshMenuItem);
        }

        public void ToggleSpinner(bool refreshing, IMenuItem refreshMenuItem)
        {
            if (refreshing)
            {
                refreshMenuItem.SetActionView(Resource.Drawable.action_progressbar);

                refreshMenuItem.ExpandActionView();
            }
            else
            {
                refreshMenuItem.CollapseActionView();
                // remove the progress bar view
                refreshMenuItem.SetActionView(null);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.AppointmentList, container, false);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var appointment = AppointmentComponent.Appointments[position];
            AppointmentComponent.ActiveAppointment = appointment;

            StartActivity(new Intent (Activity, typeof(AppointmentScreen)));
        }
    }
}