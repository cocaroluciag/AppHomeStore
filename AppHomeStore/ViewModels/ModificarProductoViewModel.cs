using AppHomeStore.Models;
using AppHomeStore.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AppHomeStore.ViewModels
{
    public partial class ModificarProductoViewModel : BaseViewModel
    {
        [ObservableProperty] 
        Producto _Producto;

        [ObservableProperty] private string nombre;
        [ObservableProperty] private string descripcion;
        [ObservableProperty] private decimal precio;
        [ObservableProperty] private int stock;
        [ObservableProperty] private List<Valor> listaCategorias;
        [ObservableProperty] private Valor categoriaSeleccionada;
        [ObservableProperty] private string imagen;
        [ObservableProperty] private int stockOriginal;

        private readonly ApiService _apiService;

        public ModificarProductoViewModel(Producto productoSeleccionado, ApiService apiService)
        {
            _apiService = apiService;

            Nombre = productoSeleccionado.NombreProducto;
            Descripcion = productoSeleccionado.Descripcion;
            Precio = productoSeleccionado.Precio;
            Stock = productoSeleccionado.Stock;
            StockOriginal = productoSeleccionado.Stock;
            Imagen = productoSeleccionado.Imagen;

            ListaCategorias = GetCategoriasValues();
            CategoriaSeleccionada = ListaCategorias.FirstOrDefault(c => c.Key.ToString() == productoSeleccionado.Categoria);
        }

        [RelayCommand]
        private async Task ModificarProductoAsync()
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Descripcion) ||
                Precio <= 0 ||
                Stock < 0 ||
                CategoriaSeleccionada == null ||
                string.IsNullOrWhiteSpace(Imagen))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Por favor, complete todos los campos.", "Aceptar");
                return;
            }

            if (Stock > StockOriginal)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "El stock no puede ser mayor que el stock registrado.", "Aceptar");
                return;
            }

            var registro = new ModificarProductoDto
            {
                //IdProducto = producto.IdProducto, // Asegúrate de tener el Id del producto original
                NombreProducto = Nombre,
                Descripcion = Descripcion,
                Precio = Precio,
                Stock = Stock,
                Categoria = CategoriaSeleccionada.Key.ToString(),
                Imagen = Imagen
            };

            try
            {
                await _apiService.ModificarProductoAsync(registro);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Producto modificado con éxito.", "Aceptar");

                if (Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault() is ProductoListaPage listaPage
                    && listaPage.BindingContext is ProductoListaViewModel listaViewModel)
                {
                    await listaViewModel.ActualizarListaProductos();
                }

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"ERROR al modificar el producto: {ex.Message}", "Aceptar");
            }
        }

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
        private async Task CancelarAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}