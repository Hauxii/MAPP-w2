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
using Newtonsoft.Json;
using MovieSearch.Model;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "Movie list")]
	//ListActivity is similar to TableViewController in ios
    public class MovieListActivity : ListActivity
    {
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//ask the intent object to extract the list that the previous activity sentt
			var jsonString = this.Intent.GetStringExtra("movieList");

			//Deserialize the json string into the movieList object
			var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);

			//Create a new adapter that will accept the movieList and try to display it)
			this.ListAdapter = new MovieListAdapter(this, movieList);
        }
    }
}