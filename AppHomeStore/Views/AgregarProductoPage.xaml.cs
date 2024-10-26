using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class AgregarProductoPage : ContentPage
{
    AgregarProductoViewModel viewModel;
    public AgregarProductoPage()
    {
        InitializeComponent();
        BindingContext = viewModel = new AgregarProductoViewModel();
    }
}