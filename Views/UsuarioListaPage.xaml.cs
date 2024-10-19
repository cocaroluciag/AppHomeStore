using AppHomeStore.Models;
using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class UsuarioListaPage : ContentPage
{
    public UsuarioListaPage()
    {
        InitializeComponent();
        BindingContext = new UsuarioViewModel();
    }   
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var vm= BindingContext as UsuarioViewModel;
        if(vm != null)
        {
            await vm.LoadUsuariosCommand.ExecuteAsync(null);
        }
    }
}
