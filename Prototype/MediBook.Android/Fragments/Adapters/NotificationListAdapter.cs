using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using MediBook.Client.Core.Components.Notification.Models;

namespace MediBook.Client.Android.Fragments.Adapters
{
    public class NotificationListAdapter : BaseAdapter<AndroidNotificationModel>
    {
        public List<AndroidNotificationModel> Notifications;

        private static Activity _context;

        public NotificationListAdapter(Activity context, List<AndroidNotificationModel> notifications)
        {
            if(context != null) _context = context;
            this.Notifications = notifications;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return Notifications.Count; }
        }

        public override AndroidNotificationModel this[int position]
        {
            get
            {
                return Notifications[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) view = _context.LayoutInflater.Inflate(Resource.Layout.CustomList, null);

            var notification = Notifications[position];

            view.FindViewById<TextView>(Resource.Id.ListText1).Text = notification.Title;
            view.FindViewById<TextView>(Resource.Id.ListText2).Text = notification.Body;
            view.FindViewById<ImageView>(Resource.Id.ListImage).Visibility = ViewStates.Invisible;
            
            return view;
        }
    }
}