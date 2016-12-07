using DM.MovieApi;
using System;

namespace MovieSearch
{
    public class DBSettings : IMovieDbSettings
    
    {
        public DBSettings()
        {
        }

        public string ApiKey
        {
            get
            {
                return "ab2135030aef90d5c5cca752213e6c56";
            }
        }

        public string ApiUrl
        {
            get
            {
                return "http://api.themoviedb.org/3/";
            }
        }
 
    }
}

