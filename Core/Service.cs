using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

using Database;
using Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Core
{
	public class Service : IService
	{
		private readonly ApplicationContext _db;
		private readonly IMapper _mapper;

		public Service(ApplicationContext appContext, IMapper mapper)
		{
			_db = appContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BookDTO>> GetAllBooksByAuthorAsync(string author)
		{
			var books = await _db.Books.Where(b => b.Author == author).ToArrayAsync();

			return await getbooksDTO(books);
		}

		public async Task<IEnumerable<BookDTO>> GetTop10BooksByGenreAsync(string genre)
		{
			var books = await _db.Books.Where(b => b.Genre == genre).ToArrayAsync();
			var booksDTO = await getbooksDTO(books);
			return booksDTO.Where(b => b.ReviewsNumber > 10).OrderByDescending(b => b.Rating).Take(10).ToArray();
		}

		public async Task<BookWithReviewsAndGanre> GetBookWithReviewsByIdAsync(int id)
		{
			var book = await _db.Books.FirstOrDefaultAsync(b => b.BookId == id);

			if (book == null)
			{
				return null;
			}

			var ratings = await _db.Ratings.Where(r => r.BookId == book.BookId).ToArrayAsync();
			var reviews = await _db.Reviews.Where(r => r.BookId == book.BookId).ToArrayAsync();

			return new BookWithReviewsAndGanre
			{
				BookId = book.BookId,
				Title = book.Title,
				Author = book.Author,
				Content = book.Content,
				Cover = book.Cover,
				Genre = book.Genre,
				Rating = ratings.Length > 0 ? ratings.Sum(r => (decimal)r.Score) / ratings.Length : 0,
				Reviews = reviews
			};
		}

		public async Task DeleteBookAsync(int id)
		{
			var book = new Book { BookId = id };
			_db.Books.Attach(book);
			_db.Books.Remove(book);
			await _db.SaveChangesAsync();
		}

		public async Task<BookOnlyId> CreateBookAsync(Book0 book)
		{
			var existBook = await _db.Books.FirstOrDefaultAsync(b => b.BookId == book.BookId);
			if (existBook == null)
			{
				await _db.Books.AddAsync(_mapper.Map<Book>(book));
			}
			else
			{
				_db.Entry(existBook).CurrentValues.SetValues(book);
			}
			await _db.SaveChangesAsync();
			return new BookOnlyId { BookId = book.BookId };
		}

		public async Task<BookOnlyId> AddReviewAsync(int id, ReviewDTO review)
		{
			var newReview = new Review
			{
				BookId = id,
				Message = review.Message,
				Reviewer = review.Reviewer,
			};
			await _db.Reviews.AddAsync(newReview);
			await _db.SaveChangesAsync();

			return new BookOnlyId { BookId = newReview.ReviewId };
		}

		public async Task AddRatingAsync(int id, RatingDTO rating)
		{
			var newRating = new Rating
			{
				BookId = id,
				Score = rating.Score,
			};
			await _db.Ratings.AddAsync(newRating);
			await _db.SaveChangesAsync();
		}

		public async Task<IEnumerable<BookWithReviews>> GetAllBooks()
		{
			var result = new List<BookWithReviews>();
			var books = await _db.Books.ToArrayAsync();

			if (books == null)
			{
				return null;
			}

			foreach (var book in books)
			{
				var ratings = await _db.Ratings.Where(r => r.BookId == book.BookId).ToArrayAsync();
				var reviews = await _db.Reviews.Where(r => r.BookId == book.BookId).ToArrayAsync();

				result.Add(new BookWithReviews
				{
					BookId = book.BookId,
					Title = book.Title,
					Author = book.Author,
					Content = book.Content,
					Cover = book.Cover,
					Rating = ratings.Length > 0 ? ratings.Sum(r => (decimal)r.Score) / ratings.Length : 0,
					Reviews = reviews
				});
			}
			return result;
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
