using AppHomeStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppHomeStore.Utils;
using System.Collections.ObjectModel;

namespace AppHomeStore.ViewModels;

public partial class AgregarProductoViewModel : BaseViewModel
{
    [ObservableProperty] private string nombre;
    [ObservableProperty] private string descripcion;
    [ObservableProperty] private float precio;
    [ObservableProperty] private int stock;
    [ObservableProperty] List<Valor> listaCategorias;
    [ObservableProperty] private Valor categoriaSeleccionada;
    [ObservableProperty] private string imagen;

    public AgregarProductoViewModel()
    {
       // Title = Constants.AppName;
        listaCategorias = GetCategoriasValues();
    }


    [RelayCommand]
    private async Task Cancelar()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task GrabarProducto()
    {
        // Validar que todos los campos estén completos
        if (string.IsNullOrWhiteSpace(Nombre) ||
            string.IsNullOrWhiteSpace(Descripcion) ||
            Precio <= 0 ||
            Stock < 0 ||
            CategoriaSeleccionada == null ||
            string.IsNullOrWhiteSpace(Imagen))
        {
            await Application.Current.MainPage.DisplayAlert("Advertencia", "Por favor, complete todos los campos.", "Aceptar");
            return; // Salir del método si hay campos vacíos
        }

        var registro = new CrearProductoDto
        {
            NombreProducto = this.Nombre,
            Descripcion = this.Descripcion,
            Precio = Convert.ToDecimal(this.Precio),
            Stock = this.Stock,
            Categoria = this.CategoriaSeleccionada.Key.ToString(), // Convertir a string
            Imagen = this.Imagen
        };

        try
        {
            await ApiService.CrearProductoAsync(registro);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Se guardó el nuevo producto.", "Aceptar");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
        }

        await Application.Current.MainPage.Navigation.PopAsync();
    }

    private List<Valor> GetCategoriasValues()
    {
        // TODO: reemplazar por lista de valores de la base de datos
        var categoriasValues = new List<Valor>()
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

        return categoriasValues;
    }
}
