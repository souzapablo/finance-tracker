using System.Text.Json.Serialization;

namespace FinanceTracker.Api.Infra.Clients.Keycloak.Dtos;

public class KeycloakUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("credentials")]
    public Credential[] Credentials { get; set; } = [];

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    [JsonPropertyName("attributes")]
    public Dictionary<string, string[]> Attributes { get; set; } = [];
}

public class Credential 
{
    [JsonPropertyName("userLabel")]
    public string UserLabel { get; set; } = "Password";
    [JsonPropertyName("temporary")]
    public bool Temporary { get; set; } = false;
    [JsonPropertyName("type")]
    public string Type { get; set; } = "password";
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
