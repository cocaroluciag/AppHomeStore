using AppHomeStore.Models;
using AppHomeStore.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels;

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
        Task.Run(async () => { await GetProductos(); }).Wait();
    }

    partial void OnProductoSeleccionadoChanged(Producto value)
    {
        if (value != null)
        {
            GoToDetalleCommand.Execute(null);  // Llama al comando cuando un producto es seleccionado
        }
    }

    [RelayCommand]
    private async Task GetProductos()
    {
        IsBusy = IsRefreshing = true;

        var productos = await ApiService.GetProductosAsync();

        if (productos != null)
        {
            Productos = new ObservableCollection<Producto>(productos);
        }

        IsBusy = IsRefreshing = false;
    }

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

    [RelayCommand]
    private async Task CreateProducto()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AgregarProductoPage());
    }

    [RelayCommand]
    private async Task OnVolver()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}