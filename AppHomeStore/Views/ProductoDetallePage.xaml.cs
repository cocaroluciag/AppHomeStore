using AppHomeStore.ViewModels;
using AppHomeStore.Models;

namespace AppHomeStore.Views;
public partial class ProductoDetallePage : ContentPage
{
    ProductoDetalleViewModel viewModel;
    public ProductoDetallePage(Producto producto)
	{
        InitializeComponent();
        BindingContext = new ProductoDetalleViewModel(producto);
       // ProductoDetalleViewModel vm = new ProductoDetalleViewModel();
    }
}