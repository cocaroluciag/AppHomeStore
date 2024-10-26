using AppHomeStore.Models;
using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class ProductoDetallePage : ContentPage
{
    public ProductoDetallePage(Producto param)
    {
        InitializeComponent();

        ProductoDetalleViewModel vm = new ProductoDetalleViewModel();
        this.BindingContext = vm;
        vm.Producto = param;  // Asignas el producto pasado como parámetro
    }
}