using System.Collections.Generic;
using System.Threading.Tasks;

using Models;

namespace Core
{
	public interface IService
	{
		public Task<IEnumerable<BookDTO>> GetAllBooksByAuthorAsync(string author);

		public Task<IEnumerable<BookDTO>> GetTop10BooksByGenreAsync(string genre);

		public Task<BookWithReviewsAndGanre> GetBookWithReviewsByIdAsync(int id);

		public Task DeleteBookAsync(int id);

		public Task<BookOnlyId> CreateBookAsync(Book0 book);

		public Task<BookOnlyId> AddReviewAsync(int id, ReviewDTO review);

		public Task AddRatingAsync(int id, RatingDTO rating);

		public Task<IEnumerable<BookWithReviews>> GetAllBooks();
	}
}
