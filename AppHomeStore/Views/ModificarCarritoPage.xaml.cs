using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;
public partial class ModificarCarritoPage : ContentPage
{
    ModificarCarritoViewModel viewModel;
    public ModificarCarritoPage()
    {
      InitializeComponent();
      BindingContext = viewModel = new ModificarCarritoViewModel();
    }
}