﻿using System;

namespace Models
{
	public class Book
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
}