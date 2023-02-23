using System.Collections.Generic;

namespace Models
{
	public class Book : Book0
	{
		public List<Review> Reviews { get; set; } = new List<Review>();

		public List<Rating> Ratings { get; set; } = new List<Rating>();
	}

	public class Book0
	{
		public int BookId { get; set; }

		public string Title { get; set; }

		public string Cover { get; set; }

		public string Content { get; set; }

		public string Author { get; set; }

		public Genre Genre { get; set; }
	}

	public enum Genre
	{
		fantasy,
		history,
		horror,
		poetry
	}

	public class BookDTO
	{
		public int BookId { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public decimal Rating { get; set; }

		public int ReviewsNumber { get; set; }
	}

	public class BookWithReviews
	{
		public int BookId { get; set; }

		public string Title { get; set; }

		public string Cover { get; set; }

		public string Content { get; set; }

		public string Author { get; set; }

		public decimal Rating { get; set; }

		public Review[] Reviews { get; set; }
	}

	public class BookOnlyId
	{
		public int BookId { get; set;}
	}
}
