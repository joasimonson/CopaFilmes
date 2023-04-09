using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Middlewares.Exceptions;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger _logger;

	public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
	{
		_next = next;
		_logger = logger.CreateLogger("ExceptionHandler");
	}

	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
		}
	}

	private async Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		_logger.LogError(exception, message: "");

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionResponse
		{
			Message = exception.Message
		}));
	}
}
