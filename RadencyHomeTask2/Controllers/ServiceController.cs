using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using Models;
using Microsoft.Extensions.Configuration;

namespace RadencyHomeTask2.Controllers
{
	public class ServiceController : ActionController<ServiceController>
	{
		private readonly IService _service;
		private readonly IConfiguration _configuration;

		public ServiceController(ILogger<ServiceController> logger, IService service, IConfiguration configuration) : base(logger)
		{
			_service = service;
			_configuration = configuration;
		}

		[Route("/api/books/author={author}")]
		[HttpGet]
		public async Task<IActionResult> GetAllBooksByAuthor(string author)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetAllBooksByAuthor(author);
			});
		}

		[Route("/api/recommended/genre={genre}")]
		[HttpGet]
		public async Task<IActionResult> GetTop10BooksByGenre(Genre genre)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetTop10BooksByGenre(genre);
			});
		}

		[Route("/api/books/id={id}")]
		[HttpGet]
		public async Task<IActionResult> GetBookWithReviewsByIdAsync(int id)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetBookWithReviewsByIdAsync(id);
			});
		}

		[Route("/api/books/id={id}/key={key}")]
		[HttpDelete]
		public async Task<IActionResult> GetBookWithReviewsByIdAsync(int id, string key)
		{
			return await ExecuteActionWithoutResultAsync(() =>
			{
				var correctKey = _configuration.GetValue<string>("Key");
				if (key != correctKey)
				{
					throw new Exception("Key not correct");
				}
				return _service.DeleteBook(id, key);
			});
		}
	}
}
