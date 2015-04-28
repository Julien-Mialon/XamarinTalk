using System.Collections.Generic;
using System.Collections.ObjectModel;
using Storm.Mvvm;
using TwitterWall.Model;
using TwitterWall.Services;
using Xamarin.Forms;

namespace TwitterWall.ViewModels
{
	class TweetListViewModel : ViewModelBase
	{
		//public const string HASHTAG = "OrleansTechTalks";
		public const string HASHTAG = "devs";

		private string _hashtag;

		public string Hashtag
		{
			get { return _hashtag; }
			set { SetProperty(ref _hashtag, value); }
		}

		public ObservableCollection<Tweet> Tweets { get; private set; }

		private ITwitterSearchService _service;

		protected ITwitterSearchService Service
		{
			get { return _service ?? (_service = DependencyService.Get<ITwitterSearchService>()); }
		}

		public TweetListViewModel()
		{
			Tweets = new ObservableCollection<Tweet>();
			Hashtag = HASHTAG;

			LoadTweetsAsync();
		}

		private async void LoadTweetsAsync()
		{
			List<Tweet> tweets = await Service.SearchAsync(Hashtag);

			foreach (Tweet t in tweets)
			{
				Tweets.Add(t);
			}

			// stream tweets
			Service.StreamAsync(Hashtag, (t) =>
			{
				Tweets.Insert(0, t);
			});
		}
	}
}
