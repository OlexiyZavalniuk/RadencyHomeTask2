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
					Cover = "Cover",
					Content = "Content",
					Author = "Author1",
					Genre = Genre.poetry
				},
				new Book
				{
					BookId = 2,
					Title = "Book2",
					Cover = "Cover",
					Content = "Content",
					Author = "Author1",
					Genre = Genre.horror,
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
				});
		}
	}
}
