using AppHomeStore.Models;
using AppHomeStore.ViewModels;

namespace AppHomeStore.Views
{
    public partial class ModificarProductoPage : ContentPage
    {
        public ModificarProductoPage(Producto productoSeleccionado, ApiService apiService) // Recibe un Producto
        {
            InitializeComponent();
            // Crear una instancia de ModificarProductoViewModel con los parámetros requeridos
            ModificarProductoViewModel viewModel = new ModificarProductoViewModel(productoSeleccionado, apiService);

            // Establecer el contexto de datos de la página en el ViewModel
            BindingContext = viewModel;
        }
    }
}