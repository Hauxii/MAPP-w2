using System;

using Android.Content;
using Android.InputMethodServices;
using Android.OS;
using Android.Views;
using Android.Widget;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Droid;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearch.Droid
{
    using MovieSearch.Model;
    using Android.Views.InputMethods;

    using Newtonsoft.Json;

    public class NameInputFragment : Fragment
    {
        private Movies _movies;
        private MovieResourceProvider _movieResourceProvider;
        private Button _searchButton;
        private ProgressBar _loading;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this._movies = new Movies();

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var rootView = inflater.Inflate(Resource.Layout.MovieInput, container, false);

            this._movies = new Movies();
            this._movieResourceProvider = new MovieResourceProvider();

            MovieDbFactory.RegisterSettings(new DBSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;


            // Get our UI controls from the loaded layout
            var movieEditText = rootView.FindViewById<EditText>(Resource.Id.movieEditText);

            _searchButton = rootView.FindViewById<Button>(Resource.Id.searchButton);

            _loading = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _loading.Visibility = ViewStates.Invisible;

            _searchButton.Click += async (sender, e) =>
            {
                _searchButton.Visibility = ViewStates.Gone;
                _searchButton.Enabled = false;
                _loading.Visibility = ViewStates.Visible;

                var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

                await this._movieResourceProvider.GetMoviesByTitle(this._movies, movieEditText.Text);

                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movies.MovieList));
                this.StartActivity(intent);
                _searchButton.Visibility = ViewStates.Visible;
                _searchButton.Enabled = true;
                _loading.Visibility = ViewStates.Gone;

            };

            return rootView;
        }
    }
}
