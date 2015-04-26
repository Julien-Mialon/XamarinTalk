using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TwitterWall.Model;
using Timer = System.Timers.Timer;

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

			Timer timer = new Timer(15000);
			timer.Elapsed += (sender, args) => UpdateList();
			timer.Start();

			UpdateList();
		}

		private void UpdateList()
		{
			Bootstrap.TwitterSearchService.SearchAsync(Bootstrap.HASHTAG).ContinueWith(x =>
			{
				DispatcherService.InvokeOnUIThread(() =>
				{
					if (Tweets.Count == 0)
					{
						foreach (Tweet tweet in x.Result)
						{
							Tweets.Add(tweet);
						}
					}
					else
					{
						ulong firstId = Tweets.First().Id;
						int index = 0;
						foreach (Tweet tweet in x.Result.Where(t => t.Id > firstId))
						{
							Tweets.Insert(index++, tweet);
						}
					}
				});
			});
		}
	}
}