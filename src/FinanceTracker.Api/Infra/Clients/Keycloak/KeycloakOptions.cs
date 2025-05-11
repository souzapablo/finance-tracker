using Microsoft.Extensions.Options;

namespace FinanceTracker.Api.Infra.Clients.Keycloak;

public class KeycloakOptionsSetup(IConfiguration configuration) : IConfigureOptions<KeycloakOptions>
{
    private const string _sectionName = "Keycloak";
    private readonly IConfiguration _configuration = configuration;

    public void Configure(KeycloakOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}

public class KeycloakOptions
{
    public string Uri { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Realm { get; set; } = string.Empty;
}
