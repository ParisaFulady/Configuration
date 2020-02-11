using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Configuration.Infrastructures
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private  RequestDelegate _next;

        private readonly UptimeService _uptimeService;

       public Middleware(RequestDelegate next, UptimeService uptimeService)
        {
            _next = next;
            _uptimeService = uptimeService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            if (_uptimeService.GetUptime.ToString() == "819011")
            {

                await httpContext.Response.WriteAsync(
                $"This is from the content middleware. uptime is: {_uptimeService.GetUptime}", Encoding.UTF8);
            }
             else
            {

                await _next.Invoke(httpContext);
            }
        }
    }

  
}
