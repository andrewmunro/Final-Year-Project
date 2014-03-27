using System.Text;

using Android.App;
using Android.Content;
using Android.Util;

using Gcm.Client;
using MediBook.Client.Android.Screens;

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

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            //Store the message
            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            CreateNotification("GCM Sample", "Message Received for GCM Sample... Tap to View!");
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(TAG, "GCM Error: " + errorId);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, "GCM Registered: " + registrationId);
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
            var notification = new Notification(Resource.Drawable.Icon, title) { Flags = NotificationFlags.AutoCancel };

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