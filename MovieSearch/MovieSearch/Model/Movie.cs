using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Model
{
	public class Movie
	{
		public Movie() { }
		public int ID { get; set; }
		public string Title { get; set; }
		public string Runtime { get; set; }
		public string Year { get; set; }
		public List<string> Genre { get; set; }
		public string Overview { get; set; }
		public string Poster { get; set; }
		public List<string> Cast { get; set; }
    }
}
