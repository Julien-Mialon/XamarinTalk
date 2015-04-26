using System;
using System.IO;
using System.Net.Http;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace TwitterWall.Android
{
	public class RemoteImageView : ImageView
	{
		private string _sourceUri;

		public string SourceUri
		{
			get { return _sourceUri; }
			set
			{
				if (Equals(_sourceUri, value)) return;
				_sourceUri = value;
				if (value != null)
				{
					DownloadImage();
				}
			}
		}

		protected RemoteImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public RemoteImageView(Context context) : base(context)
		{
		}

		public RemoteImageView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public RemoteImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		protected async void DownloadImage()
		{
			using (HttpClient client = new HttpClient())
			{
				var request = await client.GetAsync(SourceUri);
				if (request.IsSuccessStatusCode)
				{
					using (Stream requestStream = await request.Content.ReadAsStreamAsync())
					{
						var bitmap = await BitmapFactory.DecodeStreamAsync(requestStream);

						Post(() =>
						{
							SetImageBitmap(bitmap);
							Invalidate();
						});
					}
				}
			}
		}
	}
}