using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace TwitterWall.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.Initialize();
			System.Console.WriteLine("Searching...");
			Bootstrap.TwitterSearchService.StreamAsync(Bootstrap.HASHTAG, t =>
			{
				System.Console.WriteLine("Got a tweet from {0} => {1}", t.UserName, t.Text);

				HttpClient client = new HttpClient();
				client.PostAsync("http://storm-project.fr/walltweet/post.php", new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"username", t.UserName},
					{"userimage", t.UserImage},
					{"text", t.Text}
				}));
			});
			System.Console.WriteLine("Done...");
			System.Console.ReadKey();
		}
	}
}
