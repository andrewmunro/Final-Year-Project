using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using MediBook.Client.Core;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "Choose an appointment time")]
    public class ScheduleAppointmentChoiceScreen : Activity
    {
        public AppointmentComponent AppointmentComponent { get { return AppCore.Instance.GetComponent<AppointmentComponent>(); } }
        public List<PossibleTime> PossibleTimes { get { return AppointmentComponent.ActivePossibleTimes; } }

        public Button Option1 { get { return FindViewById<Button>(Resource.Id.option1); } }
        public Button Option2 { get { return FindViewById<Button>(Resource.Id.option2); } }
        public Button Option3 { get { return FindViewById<Button>(Resource.Id.option3); } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetTheme(Resource.Style.Theme_AppCompat);
            SetContentView(Resource.Layout.ScheduleAppointmentChoiceScreen);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            SetupOptionButton(Option1, PossibleTimes[0]);
            SetupOptionButton(Option2, PossibleTimes[1]);
            SetupOptionButton(Option3, PossibleTimes[2]);
        }

        private void SetupOptionButton(Button option, PossibleTime possibleTime)
        {
            option.Text = possibleTime.Time.ToFormattedString();
            option.Click += async (sender, ea) =>
                {
                    await AppointmentComponent.ConfirmSchedulingChoice(possibleTime);
                    this.Finish();
                };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    this.Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}