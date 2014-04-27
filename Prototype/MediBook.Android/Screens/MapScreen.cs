#region

using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;

using MediBook.Client.Core;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Shared.Models;

#endregion

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "Map")]
    public class MapScreen : Activity
    {
        public MapFragment Map { get; set; }

        private LocationModel AppointmentLocation
        {
            get
            {
                return AppCore.Instance.GetComponent<AppointmentComponent>().ActiveAppointment.Location;
            }
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetTheme(Resource.Style.Theme_AppCompat);
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.SetContentView(Resource.Layout.MapScreen);

            this.InitMapFragment();
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.AddMarker();

            this.CenterOnMarker();
        }

        private void InitMapFragment()
        {
            this.Map = this.FragmentManager.FindFragmentByTag("map") as MapFragment;
            if (this.Map == null)
            {
                GoogleMapOptions mapOptions =
                    new GoogleMapOptions().InvokeMapType(GoogleMap.MapTypeNormal)
                        .InvokeZoomControlsEnabled(false)
                        .InvokeCompassEnabled(false);

                FragmentTransaction fragTx = this.FragmentManager.BeginTransaction();
                this.Map = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map_container, this.Map, "map");
                fragTx.Commit();
            }
        }

        private void AddMarker()
        {
            var marker = new MarkerOptions();
            var position = new LatLng(this.AppointmentLocation.Latititude, this.AppointmentLocation.Longititude);
            marker.SetPosition(position);
            marker.SetTitle(this.AppointmentLocation.Name);
            this.Map.Map.AddMarker(marker);
        }

        private void CenterOnMarker()
        {
            var position = new LatLng(this.AppointmentLocation.Latititude, this.AppointmentLocation.Longititude);

            this.Map.Map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(position, 11.0f));
        }
    }
}