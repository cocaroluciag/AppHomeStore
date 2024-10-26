using AppHomeStore.ViewModels;
using AppHomeStore.Models;

namespace AppHomeStore.Views
{
    public partial class AgregarCarritoPage : ContentPage
    {
        private readonly ApiService _apiService;
        //public int IdUsuario _idUsuario 

        public AgregarCarritoViewModel ViewModel { get; }

        // Constructor que recibe ApiService y idUsuario
        public AgregarCarritoPage(ApiService apiService, int idUsuario)
        {
            InitializeComponent();
            _apiService = apiService;
            ViewModel = new AgregarCarritoViewModel(_apiService, idUsuario);
            BindingContext = ViewModel; // Estableciendo el contexto de enlace
        }
    }
}