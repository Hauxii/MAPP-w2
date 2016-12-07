using System;
using System.Collections.Generic;
using Foundation;
using MovieSearch.iOS.Views;
using UIKit;
using MovieSearch.Model;


namespace MovieSearch.iOS.Controllers
{
    public class MovieListSource : UITableViewSource
    {
        private List<Movie> _movieList;

        public readonly NSString MovieListCellId = new NSString("MovieListCell");

        private Action<int> _onSelectedMovie;

        public MovieListSource(List<Movie> _movieList, Action<int> onSelectedMovie)
        {
            this._movieList = _movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (CustomCell)tableView.DequeueReusableCell(MovieListCellId);
            if (cell == null)
            {
                cell = new CustomCell(this.MovieListCellId);
            }

            int row = indexPath.Row;

			string titleyear = this._movieList[row].Title + " (" + this._movieList[row].Year + ")";

			cell.UpdateCell(this._movieList[row].Poster, titleyear, this._movieList[row].Cast);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(indexPath.Row);
        }


    }
}