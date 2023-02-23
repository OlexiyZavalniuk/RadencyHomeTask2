namespace Models
{
	public class Review
	{
		public int ReviewId { get; set; }

		public string Message { get; set;}

		public int BookId { get; set; }

		public string Reviewer { get; set; }
	}

	public class ReviewDTO
	{
		public string Message { get; set; }

		public string Reviewer { get; set; }
	}
}
