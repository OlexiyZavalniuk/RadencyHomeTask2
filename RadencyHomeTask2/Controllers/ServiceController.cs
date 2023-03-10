using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using Core;
using Models;

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

		[Route("/api/books")]
		[HttpGet]
		public async Task<IActionResult> GetAllBooksByAuthor([FromQuery] string order)
		{
			if (string.IsNullOrWhiteSpace(order))
			{
				return await ExecuteActionAsync(() =>
				{
					return _service.GetAllBooks();
				});
			}
			return await ExecuteActionAsync(() =>
			{
				return _service.GetAllBooksByAuthorAsync(order);
			});
		}

		[Route("/api/recommended")]
		[HttpGet]
		public async Task<IActionResult> GetTop10BooksByGenre([FromQuery] string genre)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetTop10BooksByGenreAsync(genre);
			});
		}

		[Route("/api/books/{id:int}")]
		[HttpGet]
		public async Task<IActionResult> GetBookWithReviewsByIdAsync(int id)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetBookWithReviewsByIdAsync(id);
			});
		}

		[Route("/api/books/{id:int}")]
		[HttpDelete]
		public async Task<IActionResult> GetBookWithReviewsByIdAsync(int id, [FromQuery] string secret)
		{
			return await ExecuteActionWithoutResultAsync(() =>
			{
				var correctKey = _configuration.GetValue<string>("Key");
				if (secret != correctKey)
				{
					throw new Exception("Key not correct");
				}
				return _service.DeleteBookAsync(id);
			});
		}

		[Route("/api/books/save")]
		[HttpPost]
		public async Task<IActionResult> CreateBook([FromBody] Book0 book)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.CreateBookAsync(book);
			});
		}

		[Route("/api/books/{id:int}/review")]
		[HttpPut]
		public async Task<IActionResult> AddReview(int id, [FromBody] ReviewDTO review)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.AddReviewAsync(id, review);
			});
		}

		[Route("/api/books/{id:int}/rate")]
		[HttpPut]
		public async Task<IActionResult> A(int id, [FromBody] RatingDTO rating)
		{
			return await ExecuteActionWithoutResultAsync(() =>
			{
				if (rating.Score != Score.norm &&
					rating.Score != Score.bad &&
					rating.Score != Score.terrible &&
					rating.Score != Score.good &&
					rating.Score != Score.excellent)
				{
					throw new Exception("not correct value of rate");
				}
				return _service.AddRatingAsync(id, rating);
			});
		}
	}
}
