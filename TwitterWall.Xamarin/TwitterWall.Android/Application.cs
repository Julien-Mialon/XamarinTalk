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
using Funq;
using Storm.Mvvm;
using Storm.Mvvm.Inject;

namespace TwitterWall.Android
{
	[Application]
	public class Application : ApplicationBase
	{
		public Application(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{

		}

		public override void OnCreate()
		{
			base.OnCreate();

			AndroidContainer.CreateInstance<Container>(this, null);
		}
	}
}