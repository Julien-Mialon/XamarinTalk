using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LinqToTwitter;
using Newtonsoft.Json;
using TwitterWall.Model;

namespace TwitterWall.Services
{
	class TwitterSearchService : ITwitterSearchService
	{
		private IAuthorizer _authorizer;
		private TwitterContext _context;

		//public async Task<List<Tweet>> SearchAsync(string hashtag)
		//{
		//	TwitterContext twitterContext = await GetContextAsync();

		//	Search res = await (from search in twitterContext.Search
		//						where search.Type == SearchType.Search &&
		//								search.Query == string.Format("#{0}", hashtag)
		//						select search).SingleOrDefaultAsync();

		//	if (res != null && res.Statuses != null)
		//	{
		//		return res.Statuses.Select(x => new Tweet
		//		{
		//			Id = x.ID,
		//			Text = x.Text,
		//			UserImage = x.User.ProfileImageUrl,
		//			UserName = x.User.ScreenNameResponse
		//		}).ToList();
		//	}
		//	return new List<Tweet>();
		//}

		public async Task<List<Tweet>> SearchAsync(string hashtag)
		{
			HttpClient client = new HttpClient();
			var request = await client.GetAsync("http://storm-project.fr/walltweet/get.php");
			if (request.IsSuccessStatusCode)
			{
				string content = await request.Content.ReadAsStringAsync();

				List<Tweet> tweets = JsonConvert.DeserializeObject<List<Tweet>>(content);
				return tweets;
			}
			return new List<Tweet>();
		}

		public async Task StreamAsync(string hashtag, Action<Tweet> callbackAction)
		{
			try
			{

				TwitterContext twitterContext = await GetContextAsync();

				var res = await (from stream in twitterContext.Streaming
								 where stream.Type == StreamingType.Filter
									  && stream.Track == string.Format("#{0}", hashtag)
								 select stream).StartAsync(async streaming =>
						   {
							   if (streaming.EntityType == StreamEntityType.Status)
							   {
								   Debug.WriteLine("Got a status");
								   Status x = streaming.Entity as Status;
								   callbackAction(new Tweet
								   {
									   Id = x.ID,
									   Text = x.Text,
									   UserImage = x.User.ProfileImageUrl,
									   UserName = x.User.ScreenNameResponse
								   });
							   }
						   });

			}
			catch (Exception ex)
			{

				throw;
			}

		}

		private async Task<IAuthorizer> AuthAsync()
		{
			if (_authorizer == null)
			{
				var auth = new SingleUserAuthorizer()
				{
					CredentialStore = new InMemoryCredentialStore
					{
						ConsumerKey = "i4CpLM6hX3TuR2EGNvAfVOYxT",
						ConsumerSecret = "HUm1Vfp4Y1HhKg1hKQrypOHQyOuHohNrYuT6qU2spKS9rJ1YpN",
						OAuthToken = "314742868-8K7Fcv7KgaY3WXzr60MLQN9776BLD7z62iuHWkf2",
						OAuthTokenSecret = "vmPbZGBQ3wphMHUBM4bXkAW09axy0M8vcGurK1LH4oYli"
					}
				};
				await auth.AuthorizeAsync();
				_authorizer = auth;
			}
			return _authorizer;
		}

		private async Task<TwitterContext> GetContextAsync()
		{
			if (_context == null)
			{
				TwitterContext ctx = new TwitterContext(await AuthAsync());
				_context = ctx;
			}
			return _context;
		}
	}
}
