using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorClientSideRealWorld
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<Services.IJwtService, Services.JwtService>();
            builder.Services.AddTransient<Services.IConsoleLogService, Services.ConsoleLogService>();
            builder.Services.AddTransient<Services.ArticlesService>();
            builder.Services.AddTransient<Services.CommentsService>();
            builder.Services.AddTransient<Services.TagsService>();
            builder.Services.AddTransient<Services.UserService>();
            builder.Services.AddTransient<Services.ProfilesService>();

            builder.Services.AddScoped<Ganss.XSS.IHtmlSanitizer, Ganss.XSS.HtmlSanitizer>(x =>
            {
                var sanitizer = new Ganss.XSS.HtmlSanitizer();
                sanitizer.AllowedAttributes.Add("class");
                return sanitizer;
            });
            builder.Services.AddScoped<Services.IApiService, Services.ApiService>();
            builder.Services.AddScoped<System.Net.Http.HttpClient>();
            builder.Services.AddScoped<Services.StateService>();

            await builder.Build().RunAsync();
        }
    }
}
