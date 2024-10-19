using AppHomeStore.Views;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public CarritoListaViewModel CarritoListaViewModel { get; }

        private readonly HttpClient _httpClient;
        private readonly ApiService _apiService;

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService; // Inicializa ApiService
            Title = "APP HOME STORE";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7269/api/")
            };

            // Lógica para obtener o definir el idUsuario
            int idUsuario = ObtenerIdUsuario();

            // Inicializa CarritoListaViewModel con ApiService e idUsuario
            CarritoListaViewModel = new CarritoListaViewModel(_apiService, idUsuario);
        }

        private int ObtenerIdUsuario()
        {
            // Lógica para obtener el idUsuario, por ejemplo, desde una sesión o configuración
            return 123; // Valor de ejemplo
        }

        [RelayCommand]
        public async Task GoToProductoLista()
        {
            var viewModel = new ProductoListaViewModel();
            var productoListaPage = new ProductoListaPage(viewModel);

            var response = await _httpClient.GetAsync("/api/Producto");
            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.Navigation.PushAsync(productoListaPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la lista de productos.", "OK");
            }
        }

        [RelayCommand]
        public async Task GoToUsuarioLista()
        {
            var response = await _httpClient.GetAsync("/api/Usuario");
            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UsuarioListaPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la lista de usuarios.", "OK");
            }
        }

        [RelayCommand]
        public async Task GoToCarrito()
        {
            var response = await _httpClient.GetAsync("/api/Carrito");
            if (response.IsSuccessStatusCode)
            {
                var carritoListaPage = new CarritoListaPage(CarritoListaViewModel); // Pasa el ViewModel a la página
                await Application.Current.MainPage.Navigation.PushAsync(carritoListaPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la lista de carritos.", "OK");
            }
        }

        [RelayCommand]
        public async Task GoToAcerca()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AcercaPage());
        }
    }
}