using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace MiddleWare
{
    public class Startup
    {
  
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.TryGetValue("clave", out var traceValue))
                {                    
                    await next();
                }
                else
                {                    
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }                
            });

            app.UseUserCheck();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {                    
                    await context.Response.WriteAsync("Request Valido, Bienvenido");
                });
            });
        }
    }
}
