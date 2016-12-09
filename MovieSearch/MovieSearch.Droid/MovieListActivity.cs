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

namespace MovieSearch.Droid
{
    using MovieSearch.Model;

    using Newtonsoft.Json;

    [Activity(Theme = "@style/MyTheme", Label = "Name list")]
    public class MovieListActivity : Activity
    {
        private List<Movie> _movielList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.MovieList);

            var jsonStr = this.Intent.GetStringExtra("movieList");
            _movielList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);

            var listview = this.FindViewById<ListView>(Resource.Id.movielistview);
            listview.Adapter = new MovieListAdapter(this, _movielList);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "List of Movies";
        }
    }
}