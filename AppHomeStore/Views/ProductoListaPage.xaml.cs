using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class ProductoListaPage : ContentPage
{
    public ProductoListaPage(ProductoListaViewModel _viewModel)
    {
        BindingContext = _viewModel;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ProductoListaViewModel;

        if (vm != null)
        {
            await vm.GetProductosCommand.ExecuteAsync(null);
        }
    }
}