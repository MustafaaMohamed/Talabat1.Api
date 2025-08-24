
using Domain.Exceptions;
using Shared.ErrorModels;

namespace Talabat1.Api.Middlewares
{
	public class GlobalErrorHandlingMiddleware 
	{
		private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
		private readonly RequestDelegate _next;

		public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
				if(context.Response.StatusCode == StatusCodes.Status404NotFound)
				{
					context.Response.ContentType = "application/json";
					var response = new ErrorDetails()
					{
						StatusCode = StatusCodes.Status404NotFound,
						ErrorMessage = $"The Endpoint {context.Request.Path} was not found "
					};
					await context.Response.WriteAsJsonAsync(response);
				}

			}
			catch(Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				var response = new ErrorDetails()
				{
					ErrorMessage= ex.Message
				};
				response.StatusCode = ex switch
				{
					NotFoundException => StatusCodes.Status404NotFound,
					_ => StatusCodes.Status500InternalServerError
				};
				context.Response.StatusCode =response.StatusCode;
				await context.Response.WriteAsJsonAsync(response);
				

			}
		}
	}
}
