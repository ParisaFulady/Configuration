using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Infrastructures
{
    public class ContentMiddleware
    {
        private RequestDelegate nextDelegate;


        public ContentMiddleware(RequestDelegate next)
        {
            nextDelegate = next;

        }
        public async Task Invoke(HttpContext httpContext)
        {

           
            var u = httpContext.Request.Headers["User-Agent"].ToString();

            if (u.Contains("Mozilla"))
            {
                
                httpContext.Response.StatusCode = 404;

                await httpContext.Response.WriteAsync(
              $"noooooo", Encoding.UTF8);
            }
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }

    }
    public class ErrorMiddleware
    {
        private RequestDelegate nextDelegate;
        public ErrorMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            await nextDelegate.Invoke(httpContext);
            if (httpContext.Response.StatusCode == 403)
            {
                await httpContext.Response
                .WriteAsync("Edge not supported", Encoding.UTF8);
            }
            else if (httpContext.Response.StatusCode == 404)
            {
                await httpContext.Response
                .WriteAsync("No content middleware response", Encoding.UTF8);
            }
        }
    }
}
