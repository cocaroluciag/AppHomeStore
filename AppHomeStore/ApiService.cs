using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppHomeStore.Models;

public static class ApiService
{
    private static readonly HttpClientHandler handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };

    private static readonly HttpClient client = new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7269/") // Cambia esto a tu URL base
    };

    public static async Task<bool> ValidarCredenciales(UsuarioLoginDto loginRequest)
    {
        var json = JsonSerializer.Serialize(loginRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("api/usuario/ValidarCredencial", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseContent);

            return loginResponse?.Autenticado ?? false;
        }
        catch (HttpRequestException)
        {
            // Manejo de errores (puedes personalizar este bloque)
            return false;
        }
    }
}
