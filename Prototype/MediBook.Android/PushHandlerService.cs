using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;
using Android.Util;

using Gcm.Client;

using MediBook.Client.Android.Screens;
using MediBook.Client.Core;
using MediBook.Client.Core.Components.Account;
using MediBook.Client.Core.Components.Notification;

namespace MediBook.Client.Android
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "medibook" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "medibook" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "medibook" })]

    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { "223145784936" };

        public const string TAG = "medi-book";
    }

    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        public PushHandlerService() : base(GcmBroadcastReceiver.SENDER_IDS) { }

        private const string TAG = "medi-book";

        public AccountComponent AccountComponent { get { return AppCore.Instance.GetComponent<AccountComponent>(); } }

        public NotificationComponent NotificationComponent { get { return AppCore.Instance.GetComponent<NotificationComponent>(); } }

        protected override void OnMessage(Context context, Intent intent)
        {
            var dict = new Dictionary<string, string>();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                {
                    dict[key] = intent.Extras.Get(key).ToString();
                }
            }

            NotificationComponent.AddNotification(dict["title"], dict["body"], dict["appointmentId"]);

            CreateNotification(dict["title"], dict["body"]);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(TAG, "GCM Error: " + errorId);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, "GCM Registered: " + registrationId);
            AccountComponent.SendDeviceID(registrationId);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, "GCM Unregistered: " + registrationId);
        }

        private void CreateNotification(string title, string body)
        {
            //Create notification
            var notificationManager = GetSystemService(NotificationService) as NotificationManager;

            //Create an intent to show ui
            var uiIntent = new Intent(this, typeof(HomeScreen));

            //Create the notification
            var notification = new Notification(Resource.Drawable.Medibook_Icon_72x72, title) { Flags = NotificationFlags.AutoCancel };

            //Auto cancel will remove the notification once the user touches it

            //Set the notification info
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            notification.SetLatestEventInfo(this, title, body, PendingIntent.GetActivity(this, 0, uiIntent, 0));

            //Show the notification
            notificationManager.Notify(1, notification);
        }
    }
}