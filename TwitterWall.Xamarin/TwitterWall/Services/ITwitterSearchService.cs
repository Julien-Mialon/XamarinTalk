using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterWall.Model;

namespace TwitterWall.Services
{
	public interface ITwitterSearchService
	{
		Task<List<Tweet>> SearchAsync(string hashtag);

		Task StreamAsync(string hashtag, Action<Tweet> callbackAction);
	}
}
