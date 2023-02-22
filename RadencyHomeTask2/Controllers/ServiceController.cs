using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using Models;

namespace RadencyHomeTask2.Controllers
{
	public class ServiceController : ActionController<ServiceController>
	{
		private readonly IService _service;

		public ServiceController(ILogger<ServiceController> logger, IService service) : base(logger)
		{
			_service = service;
		}

		[Route("/api/books/{author}")]
		[HttpGet]
		public async Task<IActionResult> GetAllBooksByAuthor(string author)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetAllBooksByAuthor(author);
			});
		}

		[Route("/api/recommended/{genre}")]
		[HttpGet]
		public async Task<IActionResult> w(Genre genre)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetTop10BooksByGenre(genre);
			});
		}
	}
}
