using System;
using System.Collections.Generic;
using System.Text;
using MovieSearch.Model;
using MovieSearch.iOS.Views;

using UIKit;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.iOS.Controllers
{
	public class MovieTopRatedController : UITableViewController
    {
		private Movies _movies;

		public MovieTopRatedController(Movies movies)
		{
			this._movies = movies;

			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
		}	

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Top rated movies";
			this.View.BackgroundColor = UIColor.White;

			var indicator = CreateLoadingSpinner();
			indicator.StartAnimating();
			this.View.AddSubview(indicator);

			MovieResourceProvider resourceProvider = new MovieResourceProvider();
			await resourceProvider.GetTopRated(this._movies);

			this.TableView.Source = new MovieListSource(this._movies.MovieList, OnSelectedMovie);

			this.TableView.ReloadData();

			indicator.StopAnimating();

		}

		public void OnSelectedMovie(int row)
		{
			this.NavigationController.PushViewController(new MovieDetailController(_movies.MovieList[row]), true);
		}

		private UIActivityIndicatorView CreateLoadingSpinner()
		{
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			loading.Frame = new CGRect((this.View.Bounds.Width /2) - 25, (this.View.Bounds.Height/2) - 50, 50, 50);
			loading.HidesWhenStopped = true;
			loading.Color = UIColor.Gray;
			return loading;
		}
    }
}