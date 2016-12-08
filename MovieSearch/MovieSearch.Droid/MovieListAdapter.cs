using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.IO;
using Square.Picasso;
using Movie = MovieSearch.Model.Movie;

namespace MovieSearch.Droid
{
	public class MovieListAdapter : BaseAdapter<Movie>
	{
		private Activity _context;

		private List<Model.Movie> _movieList;

        private readonly string ImageUrl = "http://image.tmdb.org/t/p/w92";

        public MovieListAdapter(Activity context, List<Movie> movieList)
		{
			this._context = context;
			this._movieList = movieList;
		}

		public override Movie this[int position]
		{
			get { return this._movieList[position]; }
		}

		public override int Count
		{
			get
			{
				return this._movieList.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;

			if (view == null)
			{
				view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
			}
			var movie = this._movieList[position];
		    view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title;
		    var castLabel = "";
            for (var i = 0; i < movie.Cast.Count; i++)
            {
                castLabel += movie.Cast[i];
                if (i + 1 != movie.Cast.Count)
                {
                    castLabel += ", ";
                }
            }
            view.FindViewById<TextView>(Resource.Id.cast).Text = castLabel;

            /* Switched to Picasso
		    if (movie.Poster != null)
		    {
                var file = new File(movie.Poster);
		        var bmimg = BitmapFactory.DecodeFile(file.AbsolutePath);
                view.FindViewById<ImageView>(Resource.Id.poster).SetImageBitmap(bmimg);
            }
            */
		    if (!string.IsNullOrEmpty(movie.Poster))
		    {
		        var im = ImageUrl + movie.Poster;
		        Picasso.With(_context).Load(im).Into(view.FindViewById<ImageView>(Resource.Id.poster));
		    }
		    

            return view;
		}
	}
}
