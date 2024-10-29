using AppHomeStore.Models;
using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class ProductoDetallePage : ContentPage
{
    public ProductoDetallePage(Producto param)
    {
        InitializeComponent();

        // Crear la instancia del ViewModel pasando el producto como parámetro
        ProductoDetalleViewModel vm = new ProductoDetalleViewModel(param);
        this.BindingContext = vm; // Asigna el ViewModel como contexto de enlace
    }
}