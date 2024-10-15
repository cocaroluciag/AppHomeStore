using System.Net.Http;
using System.Text;
using System.Text.Json;
using AppHomeStore.Models;
using Microsoft.Maui.Controls;

namespace AppHomeStore.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var loginRequest = new UsuarioLoginDto
            {
                NombreUsuario = nombreUsuarioEntry.Text?.Trim(),
                Clave = claveEntry.Text?.Trim()
            };

            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            using (var client = new HttpClient(handler))
            {
                try
                {
                    // Asegúrate de que la URL sea la correcta
                    var response = await client.PostAsync("https://localhost:7269/api/usuario/ValidarCredencial", content);

                    response.EnsureSuccessStatusCode(); // Lanza una excepción si no es exitoso

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseContent);

                    if (loginResponse.Autenticado)
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Nombre de usuario o contraseña incorrectos.", "OK");
                    }
                }
                catch (HttpRequestException ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
}