using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public interface IService
	{
		public Task<IEnumerable<BookDTO>> GetAllBooksByAuthor(string author);

		public Task<IEnumerable<BookDTO>> GetTop10BooksByGenre(Genre genre);
	}
}
