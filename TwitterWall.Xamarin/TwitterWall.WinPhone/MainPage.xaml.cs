using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TwitterWall.Model;
using TwitterWall.WinPhone.Resources;

namespace TwitterWall.WinPhone
{
	public partial class MainPage : PhoneApplicationPage
	{
		private readonly ObservableCollection<Tweet> _source = new ObservableCollection<Tweet>(); 

		// Constructeur
		public MainPage()
		{
			InitializeComponent();

			HeaderLabel.Text = string.Format("#{0}", Bootstrap.HASHTAG);
			StatusLabel.Text = "Loading data from Twitter API...";
			TweetList.ItemsSource = _source;

			Bootstrap.Initialize();
			Bootstrap.TwitterSearchService.SearchAsync(Bootstrap.HASHTAG).ContinueWith(x =>
			{
				Dispatcher.BeginInvoke(() =>
				{
					if (x.Result.Count == 0)
					{
						StatusLabel.Text = "No result";
					}
					else
					{
						StatusLabel.Visibility = Visibility.Collapsed;
					}
					foreach (Tweet t in x.Result)
					{
						_source.Add(t);
					}
				});

				Bootstrap.TwitterSearchService.StreamAsync(Bootstrap.HASHTAG, (t) =>
				{
					Dispatcher.BeginInvoke(() =>
					{
						if (StatusLabel.Visibility == Visibility.Visible)
						{
							StatusLabel.Visibility = Visibility.Collapsed;
						}

						_source.Insert(0, t);
						Task.Run(() => Dispatcher.BeginInvoke(() => TweetList.ScrollTo(t)));
					});
				});
			});
		}
	}
}