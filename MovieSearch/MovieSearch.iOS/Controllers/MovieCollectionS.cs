using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieSearch.Model;

using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieListController : UITableViewController
    {
        private List<Movie> _movieList;

        public MovieListController(List<Movie> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            this.Title = "Movie list";
            this.View.BackgroundColor = UIColor.White;

            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
        }

        public void OnSelectedMovie(int row)
        {
			this.NavigationController.PushViewController(new MovieDetailController(_movieList[row]), true);
        }

    }
}