using System;
using System.Threading.Tasks;
using AppHomeStore.Views;
using AppHomeStore.Utils;
using AppHomeStore.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppHomeStore.ViewModels
{
    public partial class CarritoProductoViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<CarritoProducto> _productosEnCarrito;

        public Carrito Carrito { get; set; }
        public Command OnVolverCommand { get; }
        public Command AgregarProductoCommand { get; } // Nuevo comando para agregar productos

        public CarritoProductoViewModel(ApiService apiService, int idCarrito)
        {
            _apiService = apiService;
            // Cargar productos en el carrito al inicializar
            LoadCarrito(idCarrito);
        }
        public CarritoProductoViewModel()
        {
            
        }

        private async void LoadCarrito(int idCarrito)
        {
            // Obtener los detalles del carrito desde el API
            ProductosEnCarrito = new ObservableCollection<CarritoProducto>(await _apiService.ObtenerDetalleCarritoPorId(idCarrito));
        }

        private async Task GetCarritoProductos(int idCarrito)
        {
            var carritoProductos = await _apiService.ObtenerDetalleCarritoPorId(idCarrito); // Llama al método de la API
            if (carritoProductos != null)
            {
                ProductosEnCarrito = new ObservableCollection<CarritoProducto>(carritoProductos);
            }
        }

        [RelayCommand]
        private async Task EditarCarrito()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ModificarCarritoPage());
        }

        [RelayCommand]
        private async Task EliminarCarrito()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Desea eliminar este Carrito?", "Sí", "No");

            if (confirm)
            {
                // Lógica para eliminar el producto de la API
                await _apiService.BorrarDetalleCarrito(Carrito.IdCarrito);

                // Regresar a la lista de productos
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                return; // Si el usuario selecciona "No", simplemente regresar a la vista de detalles del producto
            }
        }

        [RelayCommand]
        private async Task OnVolver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

