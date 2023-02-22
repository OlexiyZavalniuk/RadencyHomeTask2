using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace RadencyHomeTask2.Controllers
{
	public class ActionController<TParentClass> : Controller
	{
		readonly ILogger<TParentClass> _logger;

		public ActionController(ILogger<TParentClass> logger)
		{
			_logger = logger;
		}

		protected async Task<IActionResult> ExecuteActionAsync<TResult>(Func<Task<TResult>> action)
		{
			try
			{
				var result = await action();
				return Ok(result);
			}
			catch (Exception exc)
			{
				_logger.LogError(exc, $"Request Error");
				return BadRequest();
			}
		}

		protected async Task<IActionResult> ExecuteActionWithoutResultAsync(Func<Task> action)
		{
			try
			{
				await action();
				return Ok();
			}
			catch (Exception exc)
			{
				_logger.LogError(exc, $"Request Error");
				return BadRequest();
			}
		}
	}
}
