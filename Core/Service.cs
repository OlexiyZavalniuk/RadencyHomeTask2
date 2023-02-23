using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

using Models;
using Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core
{
	public class Service : IService
	{
		private readonly ApplicationContext _db;

		public Service(ApplicationContext appContext)
		{
			_db = appContext;
		}

		public async Task<IEnumerable<BookDTO>> GetAllBooksByAuthor(string author)
		{
			var books = await _db.Books.Where(b => b.Author == author).ToArrayAsync();

			return await getbooksDTO(books);
		}

		public async Task<IEnumerable<BookDTO>> GetTop10BooksByGenre(Genre genre)
		{
			var books = await _db.Books.Where(b => b.Genre == genre).ToArrayAsync();
			var booksDTO = await getbooksDTO(books);
			return booksDTO.Where(b => b.ReviewsNumber > 10).OrderByDescending(b => b.Rating).Take(10).ToArray();
		}

		public async Task<BookWithReviews> GetBookWithReviewsByIdAsync(int id)
		{
			var book = await _db.Books.FirstOrDefaultAsync(b => b.BookId == id);

			if (book == null)
			{
				return null;
			}

			var ratings = await _db.Ratings.Where(r => r.BookId == book.BookId).ToArrayAsync();
			var reviews = await _db.Reviews.Where(r => r.BookId == book.BookId).ToArrayAsync();

			return new BookWithReviews
			{
				BookId = book.BookId,
				Title = book.Title,
				Author = book.Author,
				Content = book.Content,
				Cover = book.Cover,
				Rating = ratings.Length > 0 ? ratings.Sum(r => (decimal)r.Score) / ratings.Length : 0,
				Reviews = reviews
			};
		}

		public async Task DeleteBook(int id, string key)
		{
			var book = new Book { BookId = id };
			_db.Books.Attach(book);
			_db.Books.Remove(book);
			await _db.SaveChangesAsync();
		}

		private async Task<IEnumerable<BookDTO>> getbooksDTO(IEnumerable<Book> books)
		{
			var result = new List<BookDTO>();

			foreach (var book in books)
			{
				var reviews = await _db.Reviews.Where(r => r.BookId == book.BookId).ToArrayAsync();
				var ratings = await _db.Ratings.Where(r => r.BookId == book.BookId).ToArrayAsync();

				result.Add(new BookDTO
				{
					Author = book.Author,
					BookId = book.BookId,
					Title = book.Title,
					Rating = ratings.Length > 0 ? ratings.Sum(r => (decimal)r.Score) / ratings.Length : 0,
					ReviewsNumber = reviews.Length
				});
			}

			return result;
		}


	}
}
