using Microsoft.EntityFrameworkCore;

using Models;

namespace Database
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Rating> Ratings { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasMany(b => b.Ratings)
				.WithOne();

			modelBuilder.Entity<Book>()
				.HasMany(b => b.Reviews)
				.WithOne();


			modelBuilder.Entity<Book>().HasData(
				new Book
				{
					BookId = 1,
					Title = "Book1",
					Cover = "",
					Content = "Content",
					Author = "Author1",
					Genre = "poetry"
				},
				new Book
				{
					BookId = 2,
					Title = "Book2",
					Cover = "Cover",
					Content = "Content",
					Author = "Author1",
					Genre = "horror",
				},
				new Book
				{
					BookId = 3,
					Title = "Book3",
					Cover = "Cover",
					Content = "Content",
					Author = "Author1",
					Genre = "history"
				},
				new Book
				{
					BookId = 4,
					Title = "Book4",
					Cover = "Cover",
					Content = "Content",
					Author = "Author2",
					Genre = "poetry"
				},
				new Book
				{
					BookId = 5,
					Title = "Book5",
					Cover = "Cover",
					Content = "Content",
					Author = "Author3",
					Genre = "poetry"
				},
				new Book
				{
					BookId = 6,
					Title = "Book6",
					Cover = "Cover",
					Content = "Content",
					Author = "Author4",
					Genre = "poetry"
				},
				new Book
				{
					BookId = 7,
					Title = "Book7",
					Cover = "Cover",
					Content = "Content",
					Author = "Author4",
					Genre = "poetry"
				},
				new Book
				{
					BookId = 8,
					Title = "Book8",
					Cover = "Cover",
					Content = "Content",
					Author = "Author4",
					Genre = "fantasy"
				},
				new Book
				{
					BookId = 9,
					Title = "Book9",
					Cover = "Cover",
					Content = "Content",
					Author = "Author4",
					Genre = "fantasy"
				},
				new Book
				{
					BookId = 10,
					Title = "Book10",
					Cover = "Cover",
					Content = "Content",
					Author = "Author4",
					Genre = "fantasy"
				});

			modelBuilder.Entity<Rating>().HasData(
				new Rating
				{
					RatingId = 1,
					BookId = 1,
					Score = Score.good
				},
				new Rating
				{
					RatingId = 2,
					BookId = 1,
					Score = Score.excellent,
				});

			modelBuilder.Entity<Review>().HasData(
				new Review
				{
					ReviewId = 1,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 2,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 3,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 4,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 5,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 6,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 7,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 8,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 9,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 10,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 11,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				},
				new Review
				{
					ReviewId = 12,
					BookId = 1,
					Message = "Good book",
					Reviewer = "Alex"
				});
		}
	}
}
