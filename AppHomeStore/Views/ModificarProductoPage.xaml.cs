using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class ModificarProductoPage : ContentPage
{
    ModificarProductoViewModel viewModel;
    public ModificarProductoPage()
    {
        InitializeComponent();
        BindingContext = viewModel = new ModificarProductoViewModel();
    }
}