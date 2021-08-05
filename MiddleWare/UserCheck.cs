using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Net;

namespace MiddleWare
{

    public class UserCheck
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public IConfiguration _configuration;

        public UserCheck(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;

            _logger = logFactory.CreateLogger("UserCheck");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Validando Usuario..");

            /*Obtener ruta del AppSettings*/
            var fileData = AppConfiguration.GetFileName();


            /*Validar contra el JSON*/
            using (StreamReader dataList = File.OpenText(fileData))
            {
                JsonSerializer serializer = new JsonSerializer();
                UserData[] users = (UserData[])serializer.Deserialize(dataList, typeof(UserData[]));

                httpContext.Request.Headers.TryGetValue("usuario", out var currentUser);
                httpContext.Request.Headers.TryGetValue("clave", out var currentPassword);

                var result = Array.Find(users, element => element.Nombre == currentUser && element.Clave == currentPassword);
                if (result != null)
                {
                    await _next(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserCheckExtensions
    {
        public static IApplicationBuilder UseUserCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserCheck>();
        }
    }
}
