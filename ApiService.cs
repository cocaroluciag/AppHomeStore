using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppHomeStore.Models;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using AppHomeStore.Models.Dto;


namespace AppHomeStore
{
    public class ApiService
    {
        private static readonly HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        private static readonly HttpClient client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:7269/") // Cambia esto a tu URL base
        };

        /*public static async Task<string> ValidarCredenciales(UsuarioLoginDto loginRequest)
        {
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Usuario/ValidarCredencial", content);

            if (response != null)
            {
                return await response.Content.ReadAsStringAsync(); ;
            }
            else
            {
                return await response.Content.ReadAsStringAsync(); ;
            }
        }*/


        public async Task<LoginResponseDto> ValidarCredenciales(string NombreUsuario, string Clave)
        {
            string FINAL_URL = "api/Usuario/ValidarCredencial";

            var loginParams = new StringContent(
                            JsonSerializer.Serialize(
                                new
                                {
                                    NombreUsuario = NombreUsuario,
                                    Clave = Clave
                                    // password = Encriptar.GetSHA256(_password),

                                }),
                                Encoding.UTF8, "application/json"
                            );

            try
            {
                var result = await client.PostAsync(FINAL_URL, loginParams).ConfigureAwait(false);

                var json = await result.Content.ReadAsStringAsync();



                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var responseLogin = JsonSerializer.Deserialize<LoginResponseDto>(json,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });

                    return responseLogin;
                }
                else
                {
                    return null;

                    /* responseLogin = new LoginResponseModel
                    {
                        IdEstablecimiento = 0,
                        IdUsuario = 0,
                        Nombre = "",
                        Autenticado = false,
                        IdRol = 0,
                        Email = ""
                    };*/
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Obtener todos los usuarios
        public static async Task<IEnumerable<Usuario>> GetUsersAsync()
        {
            var response = await client.GetAsync("api/usuario");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<IEnumerable<Usuario>>();

            return default;
        }

        // Obtener un usuario por ID
        public static async Task<Usuario> GetUserByIdAsync(int id)
        {
            var response = await client.GetAsync($"api/usuario/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Usuario>();

            return default;
        }

        // Crear un nuevo usuario
        public static async Task<Usuario> CreateUserAsync(string NombreUsuario, string Correo, string NumeroTelefonico, string NroDocumento, string Clave)
        {
            var Nuevousuario = new
            {
                nombreusuario = NombreUsuario,
                correo = Correo,
                numeroTelefonico = NumeroTelefonico,
                nroDocumento = NroDocumento,
                clave = Clave
            };

            var response = await client.PostAsJsonAsync("api/usuario", Nuevousuario);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            else
            {
                return null;
            }

            // Manejo de errores
            //var errorContent = await response.Content.ReadAsStringAsync();
            //throw new Exception($"Error en la creación del usuario: {errorContent}");
        }

        // Actualizar un usuario existente
        public static async Task<bool> UpdateUserAsync(int id, Usuario usuario)
        {
            var response = await client.PutAsJsonAsync($"api/usuario/{id}", usuario);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Eliminar un usuario
        public static async Task<bool> DeleteUserAsync(int id)
        {
            var response = await client.DeleteAsync($"api/usuario/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
