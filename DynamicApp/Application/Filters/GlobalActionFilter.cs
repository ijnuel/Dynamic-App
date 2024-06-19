using Microsoft.AspNetCore.Mvc.Filters;
using Core.SessionIdentitier;

namespace Application.Filters
{
    public class GlobalActionAsyncFilter : IAsyncActionFilter
    {
        private readonly ISessionIdentifier _sessionIdentifier;
        public GlobalActionAsyncFilter(ISessionIdentifier sessionIdentifier)
        {
            _sessionIdentifier = sessionIdentifier;
            
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _sessionIdentifier.GetUserDetails();
            await next();
        }
    }
}
