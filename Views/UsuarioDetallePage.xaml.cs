using AppHomeStore.Models;
using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class UsuarioDetallePage : ContentPage
{
    public UsuarioDetallePage(Usuario param)
    {
        InitializeComponent();
        UsuarioDetalleViewModel vm = new UsuarioDetalleViewModel();
        BindingContext = vm;
        vm.Usuario = param;
    }
}