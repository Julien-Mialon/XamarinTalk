using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWall.Model
{
	public class Tweet
	{
		public ulong Id { get; set; }

		public string UserName { get; set; }

		public string UserImage { get; set; }

		public string Text { get; set; }
	}
}
