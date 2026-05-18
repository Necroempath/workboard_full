using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkBoard.Infrastructure.Contracts;

namespace WorkBoard.API.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        services.Configure<RefreshTokenSettings>(
            configuration.GetSection(RefreshTokenSettings.SectionName));

        services.Configure<PasswordResetTokenSettings>(
            configuration.GetSection(PasswordResetTokenSettings.SectionName));

        services.Configure<EmailSettings>(
            configuration.GetSection(EmailSettings.SectionName));

        var jwtSettings = configuration
            .GetSection(JwtSettings.SectionName)
            .Get<JwtSettings>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings!.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
            });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy
                        .AllowAnyOrigin();
                });
        });

        services.AddAuthorization();

        return services;
    }
}
