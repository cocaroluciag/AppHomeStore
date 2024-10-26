using AppHomeStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using AppHomeStore.Views;
using System.Windows.Input;

namespace AppHomeStore.ViewModels
{
    public partial class ProductoDetalleViewModel : BaseViewModel
    {
        [ObservableProperty]
        Producto _Producto;

        public ICommand OnVolverCommand { get; }
        public ICommand EliminarProductoCommand { get; }

        private List<Valor> listaCategorias;

        public ProductoDetalleViewModel(Producto producto)
        {
            this.Producto = producto;
            listaCategorias = GetCategoriasValues(); // Obtener la lista de categorías
            OnVolverCommand = new Command(Volver);
            EliminarProductoCommand = new RelayCommand(EliminarProducto);
        }

        // Obtener el nombre de la categoría desde la lista basada en la clave
        public string CategoriaNombre => listaCategorias.FirstOrDefault(c => c.Key.ToString() == Producto.Categoria)?.Value;


        private List<Valor> GetCategoriasValues()
        {
            return new List<Valor>
            {
                new Valor { Key = 1, Value = "TERMOS/COMBOS MATEROS" },
                new Valor { Key = 2, Value = "ARTICULOS DE BAZAR" },
                new Valor { Key = 3, Value = "ARTICULOS DE BLANQUERIA" },
                new Valor { Key = 4, Value = "PRENDAS" },
                new Valor { Key = 5, Value = "ACCESORIOS" },
                new Valor { Key = 6, Value = "COSMÉTICOS" },
                new Valor { Key = 7, Value = "INFANTIL" },
                new Valor { Key = 8, Value = "DECORACIÓN" }
            };
        }

        [RelayCommand]
        private async void Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task EditarProducto()
        {
            var apiService = new ApiService(); // Instancia de ApiService
            await Application.Current.MainPage.Navigation.PushAsync(new ModificarProductoPage(Producto, apiService));
        }

        // [RelayCommand]
        private async void EliminarProducto()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Desea eliminar este producto?", "Sí", "No");

            if (confirm)
            {
                // Llamada al API para eliminar el producto
                var eliminado = await ApiService.EliminarProductoAsync(Producto.IdProducto);

                if (eliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Producto eliminado correctamente", "OK");
                    // Regresar a la página anterior
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al eliminar el producto", "OK");
                }
            }
        }
    }
}