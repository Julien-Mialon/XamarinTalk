using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TwitterWall.Model;

namespace TwitterWall.Android
{
	[Activity(Label = "TwitterWall.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class HomeActivity : Activity
	{
		private TextView _hashtagTextView;
		private ListView _tweetListView;

		public TextView HashtagTextView
		{
			get { return _hashtagTextView ?? (_hashtagTextView = FindViewById<TextView>(Resource.Id.Hashtag)); }
		}

		public ListView TweetListView
		{
			get { return _tweetListView ?? (_tweetListView = FindViewById<ListView>(Resource.Id.TweetList)); }
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			HashtagTextView.Text = string.Format("#{0}", Bootstrap.HASHTAG);

			LoadTweetsAsync();
		}

		private async void LoadTweetsAsync()
		{
			Bootstrap.Initialize();
			List<Tweet> tweets = await Bootstrap.TwitterSearchService.SearchAsync(Bootstrap.HASHTAG);
			
			TweetListView.Adapter = new TweetAdapter(this, Resource.Layout.TweetTemplate, tweets.ToArray());
		}
	}
}