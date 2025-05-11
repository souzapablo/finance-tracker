using System.Text.Json.Serialization;

namespace FinanceTracker.Api.Infra.Clients.Keycloak.Dtos;

public class KeycloakError
{
    [JsonPropertyName("errorMessage")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("error_description")]
    public string Description { get; set; } = string.Empty;
}
