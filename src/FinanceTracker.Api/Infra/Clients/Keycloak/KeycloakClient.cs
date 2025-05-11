using System.Text.Json;
using System.Text;
using FinanceTracker.Api.Common.Base;
using System.Net.Http.Headers;
using FinanceTracker.Api.Infra.Clients.Keycloak.Dtos;
using Microsoft.Extensions.Options;
using FinanceTracker.Api.Features.Users;

namespace FinanceTracker.Api.Infra.Clients.Keycloak;

public class KeycloakClient(
    HttpClient httpClient,
    IOptions<KeycloakOptions> options) : IKeycloakClient
{
    private readonly KeycloakOptions _options = options.Value;
    public async Task<Result<string>> CreateUserAsync(Request request, long userId, CancellationToken cancellationToken)
    {
        var user = new KeycloakUser
        {
            Email = request.Email,
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Credentials = [ new() { Value = request.Password }],
            Attributes = new Dictionary<string, string[]>
            {
                { "app_user_id", new[] { userId.ToString() } }
            }
        };

        await GetToken(cancellationToken);

        using var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            $"admin/realms/{_options.Realm}/users", 
            jsonContent, 
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<KeycloakError>(cancellationToken);
            return Result<string>.Failure(Error.ExternalError(error?.Message!));
        }
        string externalId = string.Empty;
        if (response.Headers.Location is not null)
        {
            externalId = response.Headers.Location.Segments.Last().Trim('/');
        }

        return Result<string>.Success(externalId);
    }

    private async Task GetToken(CancellationToken cancellationToken)
    {
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"realms/{_options.Realm}/protocol/openid-connect/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = _options.ClientId,
                ["grant_type"] = "client_credentials",
                ["client_secret"] = _options.ClientSecret
            })
        };

        var tokenResponse = await httpClient.SendAsync(tokenRequest, cancellationToken);

        tokenResponse.EnsureSuccessStatusCode();

        var tokenContent = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
        var tokenJson = JsonDocument.Parse(tokenContent);
        var accessToken = tokenJson.RootElement.GetProperty("access_token").GetString();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }
}
