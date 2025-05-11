using FinanceTracker.Api.Features.Accounts;
using FinanceTracker.Api.Features.Users;
using FinanceTracker.Api.Infra.Clients.Keycloak;
using FinanceTracker.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FinanceTracker.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<FinanceTrackerDbContext>(cfg =>
        {
            cfg.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        services.RegisterClients()
            .RegisterRepositories(configuration)
            .RegisterDocumentation(configuration);

        return services;
    }
    private static IServiceCollection RegisterClients(this IServiceCollection services)
    {
        services.ConfigureOptions<KeycloakOptionsSetup>();

        services.AddHttpClient<IKeycloakClient, KeycloakClient>((serviceProvider, client) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(keycloakOptions.Uri);
        });

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }

    private static IServiceCollection RegisterDocumentation(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName!.Replace("+", "."));

            options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                        Scopes = new Dictionary<string, string>
                {
                    {"openid", "openid" }
                }
                    }
                }
            });

            var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Keycloak",
                                Type = ReferenceType.SecurityScheme
                            },
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Scheme = "Bearer"
                        },
                        []
                    }
                };

            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}
