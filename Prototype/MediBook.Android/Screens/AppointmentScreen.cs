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

using MediBook.Client.Core.Components.Appointment;
using MediBook.Shared.Models;
using MediBook.Shared.Models.Enums;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "Appointment Information")]
    public class AppointmentScreen : Activity
    {
        private AppointmentModel Appointment { get { return App.AppCore.GetComponent<AppointmentComponent>().ActiveAppointment; } }

        private int appointmentDuration { get { return Appointment.RequiredAppointmentSlots * Appointment.Type.TimeSlot; } }
        
        public bool AddedToCalander
        {
            get
            {
                var cursor = ManagedQuery(CalendarContract.Events.ContentUri, new[] { CalendarContract.Events.InterfaceConsts.Id },
                String.Format("calendar_id={0}", this.Appointment.ID), null, "dtstart ASC");

                return cursor.Count > 0;
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
        public Button CalanderButton { get { return FindViewById<Button>(Resource.Id.addToCalanderButton); } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AppointmentScreen);

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
                AppointmentTime.Text = String.Format("{ddd}, {MMM} {d}, {yyyy} at {HH:mm tt}", Appointment.ScheduledTime);
            }

            SetButtonState(CalanderButton, false);

            switch (Appointment.Status)
            {
                case AppointmentStatus.Unscheduled:
                    SetButtonState(ScheduleButton, true, "Schedule Appointment");
                    break;
                case AppointmentStatus.Scheduled:
                    SetButtonState(ScheduleButton, true, "Reschedule Appointment");
                    if (!AddedToCalander) SetButtonState(CalanderButton, true);
                    break;
                case AppointmentStatus.Completed:
                    SetButtonState(ScheduleButton, false, "Appointment Completed");
                    break;
                case AppointmentStatus.InProgress:
                    SetButtonState(ScheduleButton, false, "Appointment in Progress");
                    break;
            }
        }

        [Export]
        public void ScheduleOrReshedule(View view)
        {
            StartActivity(new Intent(this, typeof(ScheduleAppointmentScreen)));
        }

        [Export]
        public void AddToCalander(View view)
        {
            if (Appointment.ScheduledTime == null) return;

            ContentValues eventValues = new ContentValues();

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId,
                Appointment.ID.ToString());
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title,
                "Medibook Appointment: " + Appointment.Type.Type);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description,
                Appointment.Type.Description);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation,
                Appointment.Location.Name);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, 
                Convert.ToInt64((Appointment.ScheduledTime.Value - epoch).Milliseconds));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,
                Convert.ToInt64((Appointment.ScheduledTime.Value.AddMinutes(appointmentDuration) - epoch).Milliseconds));

            ContentResolver.Insert(CalendarContract.Events.ContentUri,
                eventValues);

            SetButtonState(CalanderButton, false);
        }

        private void SetButtonState(Button button, bool state, string text = null)
        {
            button.Alpha = state ? 1 : 0.5f;
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
    }
}