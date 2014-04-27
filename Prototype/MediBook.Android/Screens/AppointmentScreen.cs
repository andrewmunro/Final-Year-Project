using System;
using System.Net;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;

using Java.Interop;
using MediBook.Client.Core;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Shared.Enums;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "Appointment Information")]
    public class AppointmentScreen : Activity
    {
        private AppointmentModel Appointment { get { return AppCore.Instance.GetComponent<AppointmentComponent>().ActiveAppointment; } }

        private int appointmentDuration { get { return Appointment.RequiredAppointmentSlots * Appointment.Type.TimeSlot; } }

        private long EventId { get { return BitConverter.ToInt64(Appointment.ID.ToByteArray(), 0); } }

        public global::Android.Net.Uri EventUri { get { return ContentUris.WithAppendedId(CalendarContract.Events.ContentUri, EventId); } }

        public bool AddedToCalendar
        {
            get
            {
                var cursor = ContentResolver.Query(EventUri, new[] { "_id", "deleted" }, "calendar_id=" + CalendarId, null, null);

                var addedToCalendar = false;

                while (cursor.MoveToNext())
                {
                    long eventId = cursor.GetLong(cursor.GetColumnIndex("_id"));
                    int deleted = cursor.GetInt(cursor.GetColumnIndex("deleted"));
                    if (eventId == EventId)
                    {
                        if (deleted == 0) addedToCalendar = true;
                    }
                }
                cursor.Close();
                return addedToCalendar;
            }
        }

        private int CalendarId
        {
            get
            {
                var calendarsUri = CalendarContract.Calendars.ContentUri;

                string[] calendarsProjection =
                    {
                        CalendarContract.Calendars.InterfaceConsts.Id,
                        CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                        CalendarContract.Calendars.InterfaceConsts.AccountName
                    };

                var cursor = ManagedQuery(calendarsUri, calendarsProjection, null, null, null);

                cursor.MoveToFirst();
                return cursor.GetInt(0);
            }
        }

        public ImageView DoctorImage { get { return FindViewById<ImageView>(Resource.Id.doctorImage); } }
        public TextView DoctorName { get { return FindViewById<TextView>(Resource.Id.doctorName); } }
        public TextView DoctorType { get { return FindViewById<TextView>(Resource.Id.doctorType); } }

        public TextView AppointmentType { get { return FindViewById<TextView>(Resource.Id.appointmentType); } }
        public TextView AppointmentDescription { get { return FindViewById<TextView>(Resource.Id.appointmentDescription); } }
        public TextView AppointmentTime { get { return FindViewById<TextView>(Resource.Id.appointmentTime); } }
        public TextView AppointmentDuration { get { return FindViewById<TextView>(Resource.Id.appointmentDuration); } }
        public TextView AppointmentLocation { get { return FindViewById<TextView>(Resource.Id.appointmentLocation); } }

        public Button ScheduleButton { get { return FindViewById<Button>(Resource.Id.scheduleButton); } }
        public Button CancelButton { get { return FindViewById<Button>(Resource.Id.cancelButton); } }
        public Button CalendarButton { get { return FindViewById<Button>(Resource.Id.addToCalendarButton); } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetTheme(Resource.Style.Theme_AppCompat);
            SetContentView(Resource.Layout.AppointmentScreen);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            this.Title = Appointment.Type.Type;

            this.LoadDoctorImage();
            DoctorName.Text = Appointment.Doctor.FirstName + " " + Appointment.Doctor.LastName + ", MD";
            DoctorType.Text = Appointment.Doctor.DoctorType;

            AppointmentType.Text = Appointment.Type.Type;
            AppointmentDescription.Text = Appointment.Type.Type;
            AppointmentLocation.Text = Appointment.Location.Name;
            AppointmentDuration.Text = appointmentDuration.ToString();

            if (Appointment.ScheduledTime == null)
            {
                AppointmentTime.Text = "Unscheduled";
            }
            else
            {
                AppointmentTime.Text = Appointment.ScheduledTime.Value.ToFormattedString();
            }

            SetButtonState(this.CalendarButton, false);

            switch (Appointment.Status)
            {
                case AppointmentStatus.Unscheduled:
                    SetButtonState(ScheduleButton, true, "Schedule Appointment");
                    break;
                case AppointmentStatus.Scheduled:
                    SetButtonState(ScheduleButton, true, "Reschedule Appointment");
                    if (!AddedToCalendar) SetButtonState(this.CalendarButton, true, "Add to Calendar");
                    else SetButtonState(this.CalendarButton, true, "Remove from Calendar");
                    break;
                case AppointmentStatus.Completed:
                    SetButtonState(ScheduleButton, false, "Appointment Completed");
                    break;
                case AppointmentStatus.InProgress:
                    SetButtonState(ScheduleButton, false, "Appointment in Progress");
                    break;
            }

            SetButtonState(CancelButton, Appointment.Status == AppointmentStatus.Scheduled);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.appointment_screen_menu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    // app icon in action bar clicked; goto parent activity.
                    this.Finish();
                    return true;
                case Resource.Id.map_button:
                    OpenMap();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void OpenMap()
        {
            StartActivity(new Intent(this, typeof(MapScreen)));
        }

        [Export]
        public void ScheduleOrReshedule(View view)
        {
            StartActivity(new Intent(this, typeof(ScheduleAppointmentScreen)));
        }

        [Export]
        public void AddOrRemoveFromCalendarButtonClicked(View view)
        {
            if (Appointment.ScheduledTime == null) return;

            if (AddedToCalendar)
            {
                RemoveFromCalendar();
                SetButtonState(this.CalendarButton, true, "Add to Calendar");
            }
            else
            {
                AddToCalendar();
                SetButtonState(this.CalendarButton, true, "Remove from Calendar");
            }
        }

        private void SetButtonState(Button button, bool state, string text = null)
        {
            button.Alpha = state ? 1 : 0.8f;
            button.Enabled = state;
            if (text != null) button.Text = text;
        }

        private async void LoadDoctorImage()
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = await webClient.DownloadDataTaskAsync(Appointment.Doctor.ImageURL);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            DoctorImage.SetImageBitmap(imageBitmap);
        }

        //TODO Bug: event not added if event has already been added and then deleted
        //Solution: Either use unique id or set deleted column somehow.
        public void AddToCalendar()
        {
            var eventValues = new ContentValues();

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId,
                this.CalendarId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Id,
                EventId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title,
                "Medibook Appointment: " + Appointment.Type.Type);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description,
                Appointment.Type.Description);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation,
                Appointment.Location.Name);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart,
                Appointment.ScheduledTime.Value.ToUnixEpoch());
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,
                Appointment.ScheduledTime.Value.AddMinutes(appointmentDuration).ToUnixEpoch());
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

            var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
        }

        public void RemoveFromCalendar()
        {
            ContentResolver.Delete(EventUri, null, null);
        }
    }
}