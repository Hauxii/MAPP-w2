using System;
using MovieSearch.MovieDownload;
using MovieSearch.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.IO;

namespace MovieSearch.Droid
{
	public class MovieResourceProvider
	{
		private ImageDownloader _imageDl;
		private IApiMovieRequest _movieApi;

		public MovieResourceProvider()
		{
			this._imageDl = new ImageDownloader(new StorageClient());
			MovieDbFactory.RegisterSettings(new DBSettings());
			_movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
		}

		public async Task GetMoviesByTitle(Movies movies, string title)
		{
			var movieInfoResponse = await _movieApi.SearchByTitleAsync(title);

			await populateInfoHelper(movies, movieInfoResponse);

			return;
		}

		public async Task GetTopRated(Movies movies)
		{
			var movieInfoResponse = await _movieApi.GetTopRatedAsync();

			await populateInfoHelper(movies, movieInfoResponse);

			return;
		}

		private async Task populateInfoHelper(Movies movies, ApiSearchResponse<MovieInfo> res)
		{
			movies.ClearList();

			foreach (var m in res.Results)
			{
				ApiQueryResponse<MovieCredit> movieCreditsResponse = await _movieApi.GetCreditsAsync(m.Id);

				var localFilePath = _imageDl.LocalPathForFilename(m.PosterPath);
				if (localFilePath != string.Empty)
				{
					if (!File.Exists(localFilePath))
					{
						await _imageDl.DownloadImage(m.PosterPath, localFilePath, CancellationToken.None);
					}
				}

				m.PosterPath = localFilePath;
				movies.ExtractInfo(m, movieCreditsResponse);
			}
			return;
		}

	}
}
