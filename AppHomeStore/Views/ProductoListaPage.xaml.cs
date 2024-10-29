using AppHomeStore.ViewModels;

namespace AppHomeStore.Views
{
    public partial class ProductoListaPage : ContentPage
    {
        public ProductoListaPage(ProductoListaViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ProductoListaViewModel;

            if (vm != null)
            {
                await vm.GetProductosCommand.ExecuteAsync(null);
            }
        }
    }
}