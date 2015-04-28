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
	class TweetHolder : Java.Lang.Object
	{
		private RemoteImageView _image;
		private TextView _userName;
		private TextView _text;

		private readonly View _context;

		public TweetHolder(View context)
		{
			_context = context;
		}

		public RemoteImageView Image
		{
			get { return _image ?? (_image = _context.FindViewById<RemoteImageView>(Resource.Id.ImageView)); }
		}

		public TextView Username
		{
			get { return _userName ?? (_userName = _context.FindViewById<TextView>(Resource.Id.UserName)); }
		}

		public TextView Text
		{
			get { return _text ?? (_text = _context.FindViewById<TextView>(Resource.Id.Text)); }
		}

		public void Update(Tweet t)
		{
			Image.SourceUri = t.UserImage;
			Username.Text = string.Format("@{0}", t.UserName);
			Text.Text = t.Text;
		}
	}

	public class TweetAdapter : ArrayAdapter<Tweet>
	{
		private Activity _context;
		private int _resourceId;
		private Tweet[] _tweet;

		public TweetAdapter(Activity context, int textViewResourceId, Tweet[] objects)
			: base(context, textViewResourceId, objects)
		{
			_context = context;
			_resourceId = textViewResourceId;
			_tweet = objects;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			TweetHolder holder = null;

			if (row == null)
			{
				row = _context.LayoutInflater.Inflate(_resourceId, parent, false);

				holder = new TweetHolder(row);
				row.Tag = holder;
			}
			else
			{
				holder = row.Tag as TweetHolder ?? new TweetHolder(row);
			}

			Tweet tweet = _tweet[position];
			holder.Update(tweet);
			return row;
		}
	}
}