using AppHomeStore.Models;
using AppHomeStore.Utils;
using AppHomeStore.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace AppHomeStore.ViewModels;

public partial class ProductoListaViewModel : BaseViewModel
{

    [ObservableProperty] private ObservableCollection<Producto> _productos;
    [ObservableProperty] private Producto producto;
    [ObservableProperty] private bool isRefreshing;

    public Command CrearProductoCommand { get; }
    public Command OnVolverCommand { get; }

    public ProductoListaViewModel()
    {
      //  Title = Constants.AppName;

        Task.Run(async () => { await GetProductos(); }).Wait();
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
        await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(producto));
    
    }

    [RelayCommand]
    private async Task CreateProducto()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AgregarProductoPage());
    }

    [RelayCommand]
    private async Task OnVolver()
    {
        // await Shell.Current.GoToAsync("ProductoListaPage()");
        await Application.Current.MainPage.Navigation.PopAsync();

    }
}
