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
