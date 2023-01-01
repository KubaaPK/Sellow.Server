using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sellow.Modules.Shared.Infrastructure.Options;

namespace Sellow.Modules.Auth.Core.Auth.Firebase;

internal static class Extensions
{
    public static IServiceCollection AddFirebaseAuth(this IServiceCollection services)
    {
        var firebaseAuthOptions = services.GetOptions<FirebaseAuthOptions>("Firebase");

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(firebaseAuthOptions.ApiKeyFilePath),
            ProjectId = firebaseAuthOptions.ProjectId
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = firebaseAuthOptions.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = firebaseAuthOptions.ValidIssuer,
                    ValidateAudience = true,
                    ValidAudience = firebaseAuthOptions.ValidAudience,
                    ValidateLifetime = true
                };
            });

        services.AddScoped<IAuthService, FirebaseAuthService>();

        return services;
    }
}