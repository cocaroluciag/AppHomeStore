using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using AppHomeStore.Models;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Text;
using System.Diagnostics;

namespace AppHomeStore
{
  public class ApiService
  {
    private static readonly string BASE_URL = "https://localhost:7269/api/";
    static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) };

    public static async Task<List<Producto>> GetProductosAsync()
    {
        string FINAL_URL = BASE_URL + "Producto";

        try
        {
            var response = await httpClient.GetAsync(FINAL_URL);
            response.EnsureSuccessStatusCode(); // Manejar errores
            var jsonData = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                return JsonSerializer.Deserialize<List<Producto>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                })!;
            }

            throw new Exception("Resource Not Found");
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task<LoginReponseDto> ValidarLogin(string _email, string _password)
    {
        string FINAL_URL = BASE_URL + "Usuario/ValidarCredencial";
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { email = _email, password = _password }),
                Encoding.UTF8, "application/json"
            );

            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);
            var jsonData = await result.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                return JsonSerializer.Deserialize<LoginReponseDto>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                })!;
            }

            throw new Exception("Resource Not Found");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> CrearProductoAsync(CrearProductoDto nuevoProducto)
    {
        string FINAL_URL = BASE_URL + "Producto";
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(nuevoProducto),
                Encoding.UTF8, "application/json"
            );

            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Producto> GetProductoAsync(int id)
    {
        // string FINAL_URL = BASE_URL + "Productos/ObtenerPorId/"+id;

        string URL = "https://localhost:7269/api/Producto/" + id;

        try
        {
            //var response = await httpClient.GetAsync(URL);
            var response = await httpClient.GetAsync($"{URL}{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    // Deserializar un solo producto
                    var responseObject = JsonSerializer.Deserialize<Producto>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    throw new Exception("Resource Not Found");
                }
            }
            else
            {
                throw new Exception("Request failed with status code " + response.StatusCode);
            }
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task<bool> ModificarProductoAsync(ModificarProductoDto productoDto)
    {
        string FINAL_URL = BASE_URL + "Producto/" + productoDto.IdProducto;

        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(productoDto),
                Encoding.UTF8, "application/json"
            );

            // Registra la solicitud para depuración
            Debug.WriteLine($"Enviando PUT a {FINAL_URL} con contenido: {content}");

            var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                // Registra el código de estado para depuración
                Debug.WriteLine($"Error al modificar producto: {result.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
  
    public static async Task<bool> EliminarProductoAsync(int IdProducto)
    {
        string FINAL_URL = BASE_URL + "Producto/" + IdProducto;
        try
        {
            var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.OK || result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //API Service - Carrito
    public async Task<List<Carrito>> GetCarritos()
    {
        string FINAL_URL = BASE_URL + "Carrito";

        try
        {
            var response = await httpClient.GetAsync(FINAL_URL);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    // Inside the ApiService class
                    var responseObject = JsonSerializer.Deserialize<List<Carrito>>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    Exception exception = new Exception("Resource Not Found");
                    throw new Exception(exception.Message);
                }
            }
            else
            {
                Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                throw new Exception(exception.Message);
            }
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public static async Task<bool> CrearCarritos(Carrito carrito)
    {
        string FINAL_URL = BASE_URL + "Carrito";
        try
        {
            var content = new StringContent(
                    JsonSerializer.Serialize(carrito),
                    Encoding.UTF8, "application/json"
                );

            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);



            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<List<Carrito>> ObtenerCarritosPorId(int idCarrito)
    {
        string FINAL_URL = BASE_URL + "Carrito/" + idCarrito;
        try
        {
            var response = await httpClient.GetAsync(FINAL_URL);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Carrito>>(jsonResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> ModificarCarrito(Carrito carrito)
    {
        string FINAL_URL = BASE_URL + "Carrito/" + carrito.IdCarrito;
        try
        {
            var content = new StringContent(
                    JsonSerializer.Serialize(carrito),
                    Encoding.UTF8, "application/json"
                );

            var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> BorrarCarrito(int IdCarrito)
    {
        string FINAL_URL = BASE_URL + "Carrito/" + IdCarrito;
        try
        {
            var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.OK || result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // API Service - Detalle Carrito
    public static async Task<List<CarritoProducto>> ObtenerDetalleCarritos()
    {
        string FINAL_URL = BASE_URL + "CarritoProducto";

        try
        {
            var response = await httpClient.GetAsync(FINAL_URL);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    // Inside the ApiService class
                    var responseObject = JsonSerializer.Deserialize<List<CarritoProducto>>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    Exception exception = new Exception("Resource Not Found");
                    throw new Exception(exception.Message);
                }
            }
            else
            {
                Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                throw new Exception(exception.Message);
            }
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task<bool> CrearDetalleCarrito(CrearCarritoProductoDto crearCarrito)
    {
        string FINAL_URL = BASE_URL + "CarritoProducto";
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(crearCarrito),
                Encoding.UTF8, "application/json"
            );

            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<CarritoProducto>> ObtenerDetalleCarritoPorId(int IdCarrito)
    {

        string URL = "https://localhost:7269/api/CarritoProducto/" + IdCarrito;

        try
        {
            var response = await httpClient.GetAsync(URL);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    // Inside the ApiService class
                    var responseObject = JsonSerializer.Deserialize<List<CarritoProducto>>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    Exception exception = new Exception("Resource Not Found");
                    throw new Exception(exception.Message);
                }
            }
            else
            {
                Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                throw new Exception(exception.Message);
            }
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public static async Task<bool> ModificarDetalleCarrito(CarritoProducto carritoProducto)
    {
        string FINAL_URL = BASE_URL + "CarritoProducto/" + carritoProducto.IdCarritoProducto;
        try
        {
            var content = new StringContent(
                    JsonSerializer.Serialize(carritoProducto),
                    Encoding.UTF8, "application/json"
                );

            var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> BorrarDetalleCarrito(int IdCarritoProducto)
    {
        string FINAL_URL = BASE_URL + "CarritoPrroducto/" + IdCarritoProducto;
        try
        {
            var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.OK || result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ExisteProducto(int idProducto, int Cantidad)
    {
        string FINAL_URL = BASE_URL + "Producto/" + idProducto;

        try
        {
            var response = await httpClient.GetAsync(FINAL_URL);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ExisteCarrito(int idCarrito)
    {
        string FINAL_URL = BASE_URL + "Carrito/" + idCarrito;

        try
        {
            var response = await httpClient.GetAsync(FINAL_URL);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Carrito> ObtenerCarritoPorUsuario(int IdUsuario)
    {
        string url = $"https://localhost:7269/api/Carrito/{IdUsuario}";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Carrito>(jsonResult, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
        return null; // O maneja el error de manera apropiada
    }
  }
}