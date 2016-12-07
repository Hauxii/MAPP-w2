using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.MovieDownload;
using MovieSearch.Model;
using Android.Content;
using System.Linq;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
	[Activity (Theme = "@style/MyTheme", Label = "Movie search", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private Movies _movies;
	    private MovieResourceProvider _movieResourceProvider;
	    private Button _searchButton;
	    private ProgressBar _loading;
        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			this._movies = new Movies();
            this._movieResourceProvider = new MovieResourceProvider();

			// Set our view from the "main" layout resource
			this.SetContentView (Resource.Layout.Main);

            MovieDbFactory.RegisterSettings(new DBSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;


            // Get our UI controls from the loaded layout
            var movieEditText = this.FindViewById<EditText>(Resource.Id.movieEditText);

			_searchButton = this.FindViewById<Button>(Resource.Id.searchButton);

		    _loading = this.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _loading.Visibility = ViewStates.Invisible;

			_searchButton.Click += async  (sender, e) =>
			{
			    _searchButton.Visibility = ViewStates.Gone;
                _loading.Visibility = ViewStates.Visible;

				var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
				manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

			    await this._movieResourceProvider.GetMoviesByTitle(this._movies, movieEditText.Text);

                var intent = new Intent(this, typeof(MovieListActivity));
				intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movies.MovieList));
                this.StartActivity(intent);

			};
		}

	    protected override void OnPause()
	    {
	        base.OnPause();

            this._searchButton.Visibility = ViewStates.Visible;
            this._loading.Visibility = ViewStates.Gone;
        }
	}
}


