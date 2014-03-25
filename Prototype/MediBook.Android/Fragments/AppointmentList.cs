using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using MediBook.Client.Core.Components.Appointment;

namespace MediBook.Client.Android.Fragments
{
    public class AppointmentList : ListFragment
    {
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            App.AppCore.GetComponent<AppointmentComponent>();

            var testItems = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
            this.ListAdapter = new ArrayAdapter<string>(Activity, global::Android.Resource.Layout.SimpleListItem1, testItems);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.AppointmentList, container, false);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowAppointment(position);
        }

        private void ShowAppointment(int appointmentID)
        {
            
        }
    }
}