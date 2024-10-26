using AppHomeStore.Models;
using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class UsuarioEditarPage : ContentPage
{

    public UsuarioEditarPage(Usuario usuarioSeleccionado)
    {
        InitializeComponent();
        UsuarioEditarViewModel vm = new UsuarioEditarViewModel();
        BindingContext = vm;
        vm.Usuario = usuarioSeleccionado;
    }

}