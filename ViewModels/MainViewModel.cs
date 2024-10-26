using AppHomeStore.Views;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppHomeStore.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;

        public MainViewModel()
        {
            Title = "APP HOME STORE";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7269/api/")
            };
        }

        /*[RelayCommand]
        public async Task GoToProductoLista()
        {
            var viewModel = new ProductoListaViewModel(); // Instancia el ViewModel
            var productoListaPage = new ProductoListaPage(viewModel); // Pasa el ViewModel a la página

            var response = await _httpClient.GetAsync("/api/Producto");
            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.Navigation.PushAsync(productoListaPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la lista de productos.", "OK");
            }
        }*/

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

        /*[RelayCommand]
        public async Task GoToCarrito()
        {
            var response = await _httpClient.GetAsync("/api/Carrito");
            if (response.IsSuccessStatusCode)
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new CarritoListaPage());
                await Application.Current.MainPage.Navigation.PushAsync(new CarritoListaPage(new CarritoListaViewModel()));

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la lista de carritos.", "OK");
            }
        }*/

        [RelayCommand]
        public async Task GoToAcerca()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AcercaDePage());
        }

        public ICommand CloseSessionCommand => new Command(OnCloseSession);

        private void OnCloseSession()
        {
            // Lógica para cerrar sesión
            // Por ejemplo, navegar de vuelta a la página de inicio de sesión
            Application.Current.MainPage = new LoginPage(); // Cambia a la página de inicio de sesión
        }
    }
}