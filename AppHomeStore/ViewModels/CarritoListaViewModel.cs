using AppHomeStore.Models;  // Importa el modelo necesario
using AppHomeStore.Utils;   // Importa ApiService y otras utilidades
using AppHomeStore.Views;   // Importa las páginas de navegación
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class CarritoListaViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;  // Variable privada para ApiService
        private readonly int _idUsuario;  // Variable privada para almacenar el idUsuario

        [ObservableProperty]
        private ObservableCollection<Carrito> _carritos;  // Lista observable de carritos

        [ObservableProperty]
        private Carrito _carritoSeleccionado;  // Carrito seleccionado

        [ObservableProperty]
        private bool isRefreshing;  // Bandera para indicar si se está actualizando

        public Command OnVolverCommand { get; }  // Comando para volver

        // Constructor que recibe ApiService e idUsuario como parámetros
        public CarritoListaViewModel(ApiService apiService, int idUsuario)
        {
            _apiService = apiService;  // Inicializa ApiService
            _idUsuario = idUsuario;  // Inicializa idUsuario
            Title = Constants.AppName;  // Asigna el nombre de la aplicación

            Task.Run(async () => { await GetCarritos(); }).Wait();  // Llama a la función para obtener los carritos
        }

        // Comando para obtener la lista de carritos
        [RelayCommand]
        private async Task GetCarritos()
        {
            IsBusy = IsRefreshing = true;  // Marca como ocupado y actualizando

            // Llama a ApiService para obtener la lista de carritos
            var carritos = await _apiService.GetCarritos();

            if (carritos != null)
            {
                Carritos = new ObservableCollection<Carrito>(carritos);  // Asigna los carritos obtenidos a la colección observable
            }

            IsBusy = IsRefreshing = false;  // Finaliza la actualización
        }

        // Comando para ir al detalle del carrito seleccionado
        [RelayCommand]
        private async Task GoToDetalle()
        {
            if (_carritoSeleccionado != null)
            {
                // Navega a la página de detalle del carrito pasando el ApiService y el IdCarrito
                await Application.Current.MainPage.Navigation.PushAsync(new CarritoProductoPage(_apiService, _carritoSeleccionado.IdCarrito));
            }
        }
    }
}