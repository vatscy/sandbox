using Android.App;
using Android.Content.PM;
using Android.OS;
using KenshinLoginSkipDemo.Common;
using KenshinLoginSkipDemo.DependencyServices;
using KenshinLoginSkipDemo.Droid.DependencyServices;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace KenshinLoginSkipDemo.Droid
{
    [Activity(Label = "KenshinLoginSkipDemo", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static ShareDataService _dataService = new ShareDataService();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IShareDataService>(_dataService);
            }
        }
    }
}

