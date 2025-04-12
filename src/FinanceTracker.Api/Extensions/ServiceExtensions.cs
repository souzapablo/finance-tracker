using FinanceTracker.Api.Features.Accounts;
using FinanceTracker.Api.Features.Users;
using FinanceTracker.Api.Infra;
using FinanceTracker.Api.Infra.Clients.Keycloak;
using FinanceTracker.Api.Infra.Contracts;
using Microsoft.Extensions.Options;

namespace FinanceTracker.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.RegisterClients()
            .RegisterRepositories();

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

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
