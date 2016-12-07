using MovieSearch.Model;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using DM.MovieApi;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
	public class MovieDetailController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 30;

		private int _yCoord;

		private MovieSearch.Model.Movie movie;

		public MovieDetailController(MovieSearch.Model.Movie movie)
		{
			this.movie = movie;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Movie details";
			this.View.BackgroundColor = UIColor.White;

			var loading = CreateLoadingSpinner();
			loading.StartAnimating();
			this.View.AddSubview(loading);

			MovieDbFactory.RegisterSettings(new DBSettings());
			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
			var movieDetails = await movieApi.FindByIdAsync(movie.ID);
			loading.StopAnimating();

			this.movie.Runtime = movieDetails.Item.Runtime.ToString();



			this._yCoord = StartY;

			var title = CreateTitle();

			var runtime = CreateRunningTimeAndGenre();

			this._yCoord += StepY;
			this._yCoord += StepY;

			var poster = CreatePoster();

			var overview = CreateOverview();

			this.View.AddSubview(title);
			this.View.AddSubview(overview);
			this.View.AddSubview(runtime);
			this.View.AddSubview(poster);
		}

		private UILabel CreateTitle()
		{
			string titleyear = movie.Title + " (" + movie.Year + ")";
			var title = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - HorizontalMargin * 2, 20),
				Font = UIFont.FromName("Marion", 22f),
				Text = titleyear,
			};
			return title;
		}

		private UILabel CreateOverview()
		{
			var overview = new UILabel()
			{
				Frame = new CGRect(120, this._yCoord, this.View.Bounds.Width - HorizontalMargin - 120, 120),
				Font = UIFont.FromName("Arial", 13f),
				Text = movie.Overview,
				Lines = 0,
				LineBreakMode = UILineBreakMode.WordWrap
			};


			return overview;
		}

		private UILabel CreateRunningTimeAndGenre()
		{
			var subtitle = new UILabel()
			{
				
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - HorizontalMargin * 2, 70),
				Font = UIFont.FromName("Arial", 15f),
				Text = this.movie.Runtime + " min | ",
			};
			for (int i = 0; i < movie.Genre.Count; i++)
			{
				subtitle.Text += movie.Genre[i];
				if (i+1 != movie.Genre.Count)
				{
					subtitle.Text += ", ";
				}
			}
			return subtitle;
		}

		private UIImageView CreatePoster()
		{
			var img = new UIImageView()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, 90, 135),
				Image = UIImage.FromFile(this.movie.Poster)
			};
			return img; //movie.Poster;
		}

		private UIActivityIndicatorView CreateLoadingSpinner()
		{
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			loading.Frame = new CGRect((this.View.Bounds.Width/2) - 25, this.View.Bounds.Height/2, 50, 50);
			loading.HidesWhenStopped = true;
			return loading;
		}
	}
}