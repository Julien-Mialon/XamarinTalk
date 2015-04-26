using System.Collections.ObjectModel;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TwitterWall.Model;

namespace TwitterWall.Android
{
	public class MainViewModel : ViewModelBase
	{
		private ObservableCollection<Tweet> _tweets;

		public ObservableCollection<Tweet> Tweets
		{
			get { return _tweets; }
			private set { SetProperty(ref _tweets, value); }
		}

		public MainViewModel()
		{
			Tweets = new ObservableCollection<Tweet>();
			Bootstrap.Initialize();
		}

		public override void OnNavigatedTo(NavigationArgs e, string parametersKey)
		{
			base.OnNavigatedTo(e, parametersKey);

			Bootstrap.TwitterSearchService.SearchAsync(Bootstrap.HASHTAG).ContinueWith(x =>
			{
				DispatcherService.InvokeOnUIThread(() =>
				{
					foreach (Tweet tweet in x.Result)
					{
						Tweets.Add(tweet);
					}
				});

				Bootstrap.TwitterSearchService.StreamAsync(Bootstrap.HASHTAG, (tweet) =>
				{
					DispatcherService.InvokeOnUIThread(() =>
					{
						var o = new ObservableCollection<Tweet>(Tweets);
						o.Insert(0, tweet);
						Tweets = o;
					});
				});
			});
		}
	}
}