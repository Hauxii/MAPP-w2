using Android.Content;
using Android.Support.V4.App;
using Android.Views.InputMethods;
using Android.Widget;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    using Android.OS;
    using Android.Views;

    using MovieSearch.Model;
    using System.Threading.Tasks;

    public class OtherFragment : Fragment
    {
        private Movies _movies;
        private MovieResourceProvider _movieResourceProvider;
        private ProgressBar _loading;
        private View _rootView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this._movies = new Movies();
            this._movieResourceProvider = new MovieResourceProvider();

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            // Use this to return your custom view for this Fragment
            _rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);
            

            // Get our UI controls from the loaded layout
            _loading = this._rootView.FindViewById<ProgressBar>(Resource.Id.progressBar2);
            

            return _rootView;
        }

        public async Task FetchTopRatedMovies()
        {
            this._movies.MovieList.Clear();
            _loading.Visibility = ViewStates.Visible;
            await this._movieResourceProvider.GetTopRated(this._movies);
            _loading.Visibility = ViewStates.Gone;

            var listview = _rootView.FindViewById<ListView>(Resource.Id.movielistview);
            listview.Adapter = new MovieListAdapter(this.Activity, this._movies.MovieList);
        }
    }
}