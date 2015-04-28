using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TwitterWall.Droid
{
	[Activity(Label = "TwitterWall.Xamarin.Forms", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());
		}
	}
}

