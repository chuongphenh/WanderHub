﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WanderHub.Infrastructure.DependencyInjection.Options;

namespace WanderHub.API.DependencyInjection.Extensions;

public static class JwtExtensions
{
    public static void AddJwtAuthenticationAPI (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            JwtOption jwtOption = new JwtOption();
            configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

            /**
             * Storing the JWT in the AuthenticationProperties allows you to retrieve it from elsewhere within your application.
             * public async Task<IActionResult> SomeAction()
                {
                    // using Microsoft.AspNetCore.Authentication;
                    var accessToken = await HttpContext.GetTokenAsync("access_token");
                    // ...
                }
             */
            o.SaveToken = true; // Save token into AuthenticationProperties

            var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // on production make it true
                ValidateAudience = true, // on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();
    }
}
