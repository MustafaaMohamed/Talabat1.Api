using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System.Text;

namespace Presentation.Attributes
{
	public class CacheAttribute(int durationInSeconds) : Attribute, IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServicesManager>().CacheService;
			var cacheKey = GenerateCacheKey(context.HttpContext.Request);
			var result = await cacheService.GetCacheValueAsync(cacheKey);
			if (!string.IsNullOrEmpty(result))
			{
				context.Result = new ContentResult()
				{
					ContentType = "application/json",
					StatusCode = StatusCodes.Status200OK,
					Content = result
				};
			}
			var contextResult = await next.Invoke();
			if(contextResult.Result is OkObjectResult okObject)
			{
				await cacheService.SetCacheValueAsync(cacheKey, okObject, TimeSpan.FromSeconds(durationInSeconds));
			}


		}
		private string GenerateCacheKey(HttpRequest request)
		{
			var key = new StringBuilder();
			key.Append(request.Path);
			foreach (var item in request.Query.OrderBy(q => q.Key))
			{
				key.Append($"|{item.Key}-{item.Value}");
			}
			return key.ToString();
		}
	}
}
