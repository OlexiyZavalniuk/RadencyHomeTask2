﻿using System;
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
			modelBuilder.Entity<Book>().HasData(new Book
			{
				BookId = 1,
				Title = "Kobzar",
				Cover = "Cover",
				Content = "Content",
				Author = "Taras Shevchenko",
				Genre = Genre.poetry
			}, new Book
			{
				BookId = 2,
				Title = "Kateryna",
				Cover = "Cover",
				Content = "Content",
				Author = "Taras Shevchenko",
				Genre = Genre.poetry
			});
			modelBuilder.Entity<Review>().HasData(new Review
			{
				ReviewId = 1,
				Message = "Great!",
				BookId = 1,
				Reviewer = "Larysa Kosach"
			});
			modelBuilder.Entity<Rating>().HasData(new Rating
			{
				RatingId = 1,
				BookId = 1,
				Score = Score.excellent
			});
			modelBuilder.Entity<Rating>().HasData(new Rating
			{
				RatingId = 2,
				BookId = 1,
				Score = Score.good
			});
		}
	}
}
