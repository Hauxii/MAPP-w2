using System;
using System.Collections.Generic;
using Foundation;
using MovieSearch.iOS.Views;
using UIKit;
using MovieSearch.Model;


namespace MovieSearch.iOS.Controllers
{
    public class MovieCollectionSource : UICollectionViewSource
    {
        private List<Movie> _movieList;

        public static readonly NSString MovieCollectionCellId = new NSString("MovieCollectionCell");

        private Action<int> _onSelectedMovie;

        public MovieCollectionSource(List<Movie> _movieList, Action<int> onSelectedMovie)
        {
            this._movieList = _movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
			var cell = (CustomCollectionCell)collectionView.DequeueReusableCell(MovieCollectionCellId, indexPath);

            int row = indexPath.Row;

			string titleyear = this._movieList[row].Title + " (" + this._movieList[row].Year + ")";

			cell.UpdateCell(this._movieList[row].Poster, titleyear, this._movieList[row].Cast);
            return cell;
        }

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			return this._movieList.Count;
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row {0} selected", indexPath.Row);
		}


    }
}