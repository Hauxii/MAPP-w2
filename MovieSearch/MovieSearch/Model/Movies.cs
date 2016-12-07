using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
 
namespace MovieSearch.Model
{
    public class Movies
    {
        private List<Movie> _movieList;

        public Movies()
        {
            this._movieList = new List<Movie>();
        }

        public void AddMovie(Movie mov)
        {
            this._movieList.Add(mov);
        }

		public void ClearList()
		{
			this._movieList.Clear();
		}

		public  void ExtractInfo(MovieInfo movieInfo ,ApiQueryResponse<MovieCredit> response)
		{

			List<string> cast = new List<string>();
			List<string> genre = new List<string>();

		    if (response.Item.CastMembers != null)
		    {
                for (int i = 0; i < response.Item.CastMembers.Count && i < 3; i++)
                {
                    cast.Add(response.Item.CastMembers[i].Name);
                }
            }
			

			foreach (var g in movieInfo.Genres)
			{
				genre.Add(g.Name);
			}


			Movie newMovie = new Movie()
			{
				ID = movieInfo.Id,
				Title = movieInfo.Title,
				Year = movieInfo.ReleaseDate.Year.ToString(),
				Poster = movieInfo.PosterPath,
				Overview = movieInfo.Overview,
				Cast = cast,
				Genre = genre
			};

			AddMovie(newMovie);

		}

		public void ExtractDetailedInfo(ApiQueryResponse<Movie> response)
		{
			 
		}


		//Getter
		public List<Movie> MovieList => this._movieList;
    }
}
