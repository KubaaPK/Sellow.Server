using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Sellow.Modules.Shared.Infrastructure.Swagger;

internal static class Extensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services) => services
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Sellow"
            });

            // https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/93#issuecomment-458690098
            var xmlFiles = Directory
                .GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .ToList();
            xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
        });

    public static IApplicationBuilder UseSwagger_(this IApplicationBuilder app) => app
        .UseSwagger()
        .UseSwaggerUI();
}