using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Application.Models;
using System.Text.Json;

namespace Application.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            var exception = !_hostEnvironment.IsDevelopment() ? new Exception("An error occured! Please contact the administrator.") : context.Exception;

            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = JsonSerializer.Serialize(ResponseModel<string>.Exception(exception))
            };
        }
    }
}
