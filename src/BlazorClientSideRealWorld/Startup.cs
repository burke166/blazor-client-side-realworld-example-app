using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorClientSideRealWorld
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Services.IJwtService, Services.JwtService>();
            services.AddTransient<Services.IConsoleLogService, Services.ConsoleLogService>();
            services.AddTransient<Services.ArticlesService>();
            services.AddTransient<Services.CommentsService>();
            services.AddTransient<Services.TagsService>();
            services.AddTransient<Services.UserService>();
            services.AddTransient<Services.ProfilesService>();

            services.AddScoped<Ganss.XSS.IHtmlSanitizer, Ganss.XSS.HtmlSanitizer>(x =>
            {
                var sanitizer = new Ganss.XSS.HtmlSanitizer();
                sanitizer.AllowedAttributes.Add("class");
                return sanitizer;
            });
            services.AddScoped<Services.IApiService, Services.ApiService>();
            services.AddScoped<System.Net.Http.HttpClient>();
            services.AddScoped<Services.StateService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
