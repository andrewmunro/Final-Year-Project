using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using MediBook.Shared.Models;

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
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomList, null);
            view.FindViewById<TextView>(Resource.Id.ListText1).Text = Appointments[position].Type.Type;
            view.FindViewById<TextView>(Resource.Id.ListText2).Text = Appointments[position].Status.ToString();
            view.FindViewById<ImageView>(Resource.Id.ListImage).SetImageResource(Resource.Drawable.Icon);
            return view;
        }
    }
}