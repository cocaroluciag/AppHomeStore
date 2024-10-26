using System;
using System.Threading.Tasks;
using AppHomeStore.Models;
using AppHomeStore.Views;
using AppHomeStore.Utils;
using CommunityToolkit.Mvvm.Input;

namespace AppHomeStore.ViewModels
{
    public partial class ProductoDetalleViewModel : BaseViewModel
    {
        public ProductoDetalleViewModel()
        {
          //  Title = Constants.AppName;
        }

        // Propiedades del producto para enlazar con el XAML
        public string Imagen => Producto?.Imagen;  // Imagen del producto
        public string Nombre => Producto?.NombreProducto;  // Nombre del producto
        public string Descripcion => Producto?.Descripcion;  // Descripción del producto
        public string Categoria => Producto?.Categoria;  // Categoría del producto
        public int Stock => Producto?.Stock ?? 0;  // Stock disponible
        public decimal Precio => Producto?.Precio ?? 0;  // Precio del producto

        public Producto Producto { get; set; }
       // public Command EditarProductoCommand { get; }
       // public Command EliminarProductoCommand { get; }
        public Command OnVolverCommand { get; }


        [RelayCommand]
        private async Task EditarProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ModificarProductoPage());
        }

        [RelayCommand]
        private async Task EliminarProducto()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Desea eliminar este producto?", "Sí", "No");

            if (confirm)
            {
                // Lógica para eliminar el producto de la API
                await ApiService.EliminarProducto(Producto.IdProducto);

                // Regresar a la lista de productos
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                // Si el usuario selecciona "No", simplemente regresar a la vista de detalles del producto
                return;
            }
        }

        [RelayCommand]
        private async Task OnVolver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        // Constructor
        public ProductoDetalleViewModel(Producto producto)
        {
            Producto = producto;
        }
    }
}
