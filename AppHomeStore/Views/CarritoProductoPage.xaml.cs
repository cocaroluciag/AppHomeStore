using AppHomeStore.ViewModels;

namespace AppHomeStore.Views
{
    public partial class CarritoProductoPage : ContentPage
    {
        public CarritoProductoViewModel ViewModel { get; }

        public CarritoProductoPage(ApiService apiService, int idCarrito)
        {
            InitializeComponent();
            ViewModel = new CarritoProductoViewModel(apiService, idCarrito); // Aseg�rate de pasar los par�metros aqu�
            BindingContext = ViewModel;
        }
    }
}