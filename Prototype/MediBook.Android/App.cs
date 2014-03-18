using System;

using Android.App;
using Android.Runtime;

using MediBook.Client.Core;

namespace MediBook.Client.Android
{
    [Application]
    public class App : Application
    {
        public static App Current { get; private set; }

        public static AppCore AppCore { get; private set; }

        public App(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) 
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AppCore = new AppCore();
        }
    }
}