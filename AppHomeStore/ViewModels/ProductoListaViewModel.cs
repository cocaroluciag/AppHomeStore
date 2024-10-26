using AppHomeStore.Models;
using AppHomeStore.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class ProductoListaViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Producto> _productos;

        [ObservableProperty]
        private bool isRefreshing;

        [ObservableProperty]
        private Producto productoSeleccionado;

        public Command CrearProductoCommand { get; }
        public Command OnVolverCommand { get; }

        public ProductoListaViewModel()
        {
            // Llama directamente a GetProductos en el constructor
            Task.Run(async () => { await GetProductos(); }).Wait();
        }

        // Este método actualizará los productos cuando la selección cambie
        partial void OnProductoSeleccionadoChanged(Producto value)
        {
            if (value != null)
            {
                GoToDetalleCommand.Execute(null);  // Llama al comando cuando un producto es seleccionado
            }
        }

        // Método que obtiene la lista de productos de la API
        [RelayCommand]
        private async Task GetProductos()
        {
            IsBusy = IsRefreshing = true;

            var productos = await ApiService.GetProductosAsync();

            if (productos != null)
            {
                Productos = new ObservableCollection<Producto>(productos); // Actualiza la lista
            }

            IsBusy = IsRefreshing = false;
        }

        // Navega a la página de detalles de un producto
        [RelayCommand]
        private async Task GoToDetalle()
        {
            if (productoSeleccionado == null)
            {
                return;
            }

            // Navega a la página de detalles del producto seleccionado
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(productoSeleccionado), true);
        }

        public async Task ActualizarListaProductos()
        {
            var productosActualizados = await ApiService.GetProductosAsync();
            if (productosActualizados != null)
            {
                Productos = new ObservableCollection<Producto>(productosActualizados);
            }
        }

        // Método para crear un nuevo producto
        [RelayCommand]
        private async Task CreateProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AgregarProductoPage());
        }

        // Vuelve a la página anterior
        [RelayCommand]
        private async Task OnVolver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}