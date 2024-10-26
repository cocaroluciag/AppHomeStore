using System.Threading.Tasks;
using AppHomeStore.Models;
using AppHomeStore.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace AppHomeStore.ViewModels;

public partial class ProductoDetalleViewModel : BaseViewModel
{
    [ObservableProperty]
    Producto producto;  // Aquí se usa la clase Producto
    public ICommand OnVolverCommand { get; }
    public ProductoDetalleViewModel()
    {
        // Title = "Detalle del Producto";
        OnVolverCommand = new Command(Volver);
    }

    [RelayCommand]
    private async void Volver()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

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
            await ApiService.EliminarProducto(Producto.IdProducto);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
