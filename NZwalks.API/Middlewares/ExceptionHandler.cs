using System.Net;

namespace NZwalks.API.Middlewares
{
	public class ExceptionHandler
	{
		private readonly ILogger logger;
		private readonly RequestDelegate next;

		public ExceptionHandler(ILogger<ExceptionHandler> logger,
			RequestDelegate next)
        {
			this.logger = logger;
			this.next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception ex)
			{
				var errorId = Guid.NewGuid();
				//Log the exception
				logger.LogError(ex, $"{errorId}:{ex.Message}");
					//Return a custom exception
					httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
				    httpContext.Response.ContentType = "application/json";
				var error = new
				{
                   id=errorId,	
				   ErrorMessage="something went wrong"

				};
				await httpContext.Response.WriteAsJsonAsync(error);

				}
		}
    }
}
