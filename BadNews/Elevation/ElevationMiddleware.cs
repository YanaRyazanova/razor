using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BadNews.Elevation
{
    public class ElevationMiddleware
    {
        private readonly RequestDelegate next;
    
        public ElevationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
    
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var query = context.Request.Query;
            if (path.ToString().Contains("/elevation"))
            {
                if (query.ContainsKey("up"))
                {
                    context.Response.Cookies.Append(ElevationConstants.CookieName, ElevationConstants.CookieValue);
                }
                else if (query.Count == 0)
                {
                    context.Response.Cookies.Delete(ElevationConstants.CookieName);
                }
            }
            await next(context);
            context.Response.Redirect("/");
        }
    }
}
