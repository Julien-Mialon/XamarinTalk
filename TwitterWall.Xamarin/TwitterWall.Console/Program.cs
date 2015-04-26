using System;
using System.Collections.Generic;
using System.Linq;
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
			Bootstrap.TwitterSearchService.Search();
			System.Console.WriteLine("Done...");
			System.Console.ReadKey();
		}
	}
}
