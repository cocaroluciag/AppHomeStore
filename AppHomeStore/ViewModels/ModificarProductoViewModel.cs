using AppHomeStore.Utils;
using CommunityToolkit.Mvvm.Input;

namespace AppHomeStore.ViewModels;

public partial class ModificarProductoViewModel : BaseViewModel
{
    public ModificarProductoViewModel()
    {
      //  Title = Constants.AppName;
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task Grabar()
    {
        await Application.Current.MainPage.DisplayAlert("Producto", "Producto modificado", "Aceptar");
    }
}
