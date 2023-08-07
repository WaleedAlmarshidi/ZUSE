using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
namespace ZUSE.Server.Data
{

    public class EarlyReturnAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Perform any necessary logic before returning the result

            // Return a 2XX result (e.g., 200 OK) immediately
            var result = new StatusCodeResult(200);

            // Execute the remaining action logic
            var executedContext = await next();

            // Perform additional logic after returning the result
        }
    }

}

