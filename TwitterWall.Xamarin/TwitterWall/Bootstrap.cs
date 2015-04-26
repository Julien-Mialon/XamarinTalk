using TwitterWall.Services;

namespace TwitterWall
{
	public static class Bootstrap
	{
		//public const string HASHTAG = "OrleansTechTalks";
		public const string HASHTAG = "devs";

		public static ITwitterSearchService TwitterSearchService { get; private set; }

		public static void Initialize()
		{
			TwitterSearchService = new TwitterSearchService();
		}

	}
}
