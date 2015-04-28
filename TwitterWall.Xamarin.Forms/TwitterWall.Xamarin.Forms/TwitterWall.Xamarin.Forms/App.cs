using Storm.Mvvm;
using TwitterWall.Services;
using TwitterWall.Views;
using Xamarin.Forms;

namespace TwitterWall
{
	public class App : MvvmApplication<TweetListPage>
	{
		public App()
			: base(() =>
			{
				DependencyService.Register<ITwitterSearchService, TwitterSearchService>();
			})
		{

			
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
