using AppHomeStore.ViewModels;

namespace AppHomeStore.Views
{
    public partial class CarritoProductoPage : ContentPage
    {
        public CarritoProductoViewModel ViewModel { get; }

        public CarritoProductoPage(ApiService apiService, int idCarrito)
        {
            InitializeComponent();
            ViewModel = new CarritoProductoViewModel(apiService, idCarrito); // Asegúrate de pasar los parámetros aquí
            BindingContext = ViewModel;
        }
    }
}