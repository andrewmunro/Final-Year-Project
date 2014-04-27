using System;

using Android.App;
using Android.Gms.Maps;
using Android.OS;

namespace MediBook.Client.Android.Screens
{
    [Activity(Label = "Map")]
    public class MapScreen : Activity
    {
        public MapFragment Map { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Console.WriteLine(ApplicationContext.PackageName);

            this.SetTheme(Resource.Style.Theme_AppCompat);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.MapScreen);

            this.InitMapFragment();
        }

        private void InitMapFragment()
        {
            Map = FragmentManager.FindFragmentByTag("map") as MapFragment;
            if (Map == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                Map = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map_container, Map, "map");
                fragTx.Commit();
            }
        }
    }
}