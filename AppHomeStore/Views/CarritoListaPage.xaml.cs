using AppHomeStore.ViewModels;

namespace AppHomeStore.Views
{
    public partial class CarritoListaPage : ContentPage
    {
        public CarritoListaViewModel ViewModel { get; }

        public CarritoListaPage(CarritoListaViewModel viewModel) // Cambiar el constructor
        {
            InitializeComponent();
            ViewModel = viewModel; // Asignar el ViewModel
            BindingContext = ViewModel;
        }
    }
}