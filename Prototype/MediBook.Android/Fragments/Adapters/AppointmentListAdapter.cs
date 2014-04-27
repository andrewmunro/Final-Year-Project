using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using MediBook.Shared.Enums;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

namespace MediBook.Client.Android.Fragments.Adapters
{
    public class AppointmentListAdapter : BaseAdapter<AppointmentModel>
    {
        public List<AppointmentModel> Appointments;

        private readonly Activity context;

        public AppointmentListAdapter(Activity context, List<AppointmentModel> appointments)
        {
            this.context = context;
            this.Appointments = appointments;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return Appointments.Count; }
        }

        public override AppointmentModel this[int position]
        {
            get
            {
                return Appointments[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.CustomList, null);

            var appointment = Appointments[position];

            view.FindViewById<TextView>(Resource.Id.ListText1).Text = appointment.Type.Type;
            view.FindViewById<TextView>(Resource.Id.ListText2).Text = appointment.Status == AppointmentStatus.Unscheduled ? appointment.Status.ToString() : appointment.ScheduledTime.Value.ToFormattedString();
            view.FindViewById<ImageView>(Resource.Id.ListImage).SetImageResource(Resource.Drawable.Icon);
            
            return view;
        }
    }
}