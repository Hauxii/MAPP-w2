using System;
using DM.MovieApi;

using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieSearch.Model;
using System.Collections.Generic;
using System.Threading;
using MovieSearch.MovieDownload;
using System.Threading.Tasks;

namespace MovieSearch.iOS.Controllers

{
	public class MovieController : UIViewController
	{

		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		private Movies _movies;

		public MovieController(Movies movies)
		{ 
			this._movies = movies;

			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Title = "Search";

			this.View.BackgroundColor = UIColor.White;

			this._yCoord = StartY;

			var prompt = CreatePrompt("Enter words in a movie title: ");

			this._yCoord += StepY;

			var movieField = CreateMovieField();

			this._yCoord += StepY;

			var searchButton = CreateButton("Get movie");
			this._yCoord += StepY;

			var prevS = CreatePrompt("Previous searches:");
			this.View.AddSubview(prevS);
			this._yCoord += StepY/2;

			var loading = CreateLoadingSpinner();
			loading.StopAnimating();

			searchButton.TouchUpInside += async (sender, args) =>
			{
				if (movieField.Text.Length == 0)
				{
					CreateAlertWithMessage("Invalid input", "Please enter a valid input");
				}
				else {
					searchButton.Enabled = false;
					searchButton.Hidden = true;

					loading.StartAnimating();

					MovieResourceProvider resourceProvider = new MovieResourceProvider();

					await resourceProvider.GetMoviesByTitle(this._movies, movieField.Text);

					if (this._movies.MovieList.Count == 0)
					{
						CreateAlertWithMessage("No movies", "There are no movies in the database containing this set of words");
					}
					else {
						NavigationController.PushViewController(new MovieListController(this._movies.MovieList), true);
					}

					var ps = CreatePrompt("\t" + movieField.Text);
					this.View.AddSubview(ps);
					this._yCoord += StepY / 2;

					loading.StopAnimating();
					searchButton.Enabled = true;
					searchButton.Hidden = false;
					loading.StopAnimating();
				}

			};

			this.View.AddSubview(prompt);
			this.View.AddSubview(movieField);
			this.View.AddSubview(searchButton);
			this.View.AddSubview(loading);
		}

		private UIButton CreateButton(string title)
		{
			var navButton = UIButton.FromType(UIButtonType.RoundedRect);
			navButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
			navButton.SetTitle(title, UIControlState.Normal);
			return navButton;
		}

		private UITextField CreateMovieField()
		{
			var movieField = new UITextField()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				BorderStyle = UITextBorderStyle.RoundedRect,
				Placeholder = "Title"
			};
			return movieField;
		}

		private UILabel CreatePrompt(string text)
		{
			var prompt = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = text
			};
			return prompt;
		}

		private UIActivityIndicatorView CreateLoadingSpinner()
		{
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			loading.Frame = new CGRect(HorizontalMargin, 180, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
			loading.HidesWhenStopped = true;
			return loading;
		}

		private void CreateAlertWithMessage(string header, string message)
		{
			var okAlertController = UIAlertController.Create(header, message, UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			this.PresentViewController(okAlertController, true, null);
		}
    }
}

