using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using Java.Interop;

using MediBook.Client.Core;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Client.Core.Exceptions;
using MediBook.Shared.utils;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "Schedule Appointment")]
    public class ScheduleAppointmentScreen : Activity
    {
        public AppointmentComponent AppointmentComponent { get { return AppCore.Instance.GetComponent<AppointmentComponent>(); } }

        public Button SelectDateTimeButton { get { return FindViewById<Button>(Resource.Id.selectDateButton); } }
        public Button CancelButton { get { return FindViewById<Button>(Resource.Id.cancelButton); } }
        public Button ConfirmButton { get { return FindViewById<Button>(Resource.Id.confirmButton); } }

        public TextView ErrorText { get { return FindViewById<TextView>(Resource.Id.errorText); } }

        public TimePicker TimePicker { get { return PickerDialog.FindViewById<TimePicker>(Resource.Id.timePicker); } }
        public DatePicker DatePicker { get { return PickerDialog.FindViewById<DatePicker>(Resource.Id.datePicker); } }
        public Button SetDateTimeButton { get { return PickerDialog.FindViewById<Button>(Resource.Id.setDateTimeButton); } }

        private Dialog PickerDialog { get; set; }
        private ProgressDialog ProgressDialog { get; set; }

        private DateTime SelectedTime { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetTheme(Resource.Style.Theme_AppCompat);
            SetContentView(Resource.Layout.ScheduleAppointmentScreen);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            ProgressDialog = new ProgressDialog(this);
            ProgressDialog.Indeterminate = true;
            ProgressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            ProgressDialog.SetCancelable(false);

            PickerDialog = new Dialog(this);

            PickerDialog.SetContentView(Resource.Drawable.date_time_picker_dialog);
            PickerDialog.SetTitle("Pick Date and Time");
            SelectedTime = DateTime.Now;

            //Datepicker month indexed from 0
            DatePicker.UpdateDate(SelectedTime.Year, SelectedTime.Month - 1, SelectedTime.Day + 1);
            TimePicker.CurrentHour = (Java.Lang.Integer)SelectedTime.Hour;
            TimePicker.CurrentMinute = (Java.Lang.Integer)SelectedTime.Minute;

            DatePicker.MinDate = SelectedTime.ToUnixEpoch();
            SetDateTimeButton.Click += SetDate;

            this.HideError();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    // app icon in action bar clicked; goto parent activity.
                    this.Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetDate(object sender, EventArgs eventArgs)
        {
            this.SelectedTime = new DateTime(DatePicker.Year, DatePicker.Month + 1, DatePicker.DayOfMonth, (int)TimePicker.CurrentHour, (int)TimePicker.CurrentMinute, 0, DateTimeKind.Local).ToUniversalTime();
            SelectDateTimeButton.Text = SelectedTime.ToFormattedString();
            PickerDialog.Hide();
        }

        [Export]
        public void SelectDate(View view)
        {
            PickerDialog.Show();
        }

        [Export]
        public void Cancel(View view)
        {
            StartActivity(new Intent(this, typeof(AppointmentScreen)));
        }

        [Export]
        public async void Confirm(View view)
        {
            if (SelectDateTimeButton.Text == "Select Time and Date")
            {
                this.ShowError("You must select a date and time!");
                return;
            }

            this.ProgressDialog.SetMessage("Scheduling appointment...");
            this.ProgressDialog.Show();

            this.HideError();

            try
            {
                await AppointmentComponent.ScheduleAppointment(SelectedTime);
                this.ProgressDialog.Hide();
                StartActivity(new Intent(this, typeof(AppointmentScreen)));
            }
            catch (ScheduleException e)
            {
                this.ProgressDialog.Dismiss();
                this.ShowError(e.Message);
            }
        }

        private void HideError()
        {
            ErrorText.Visibility = ViewStates.Invisible;
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = ViewStates.Visible;
        }
    }
}