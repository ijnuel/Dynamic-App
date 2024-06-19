using Application.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;
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
