using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Asegúrate de crear una instancia de ApiService y pasarla al MainViewModel
        var apiService = new ApiService(); // Inicializa el servicio API

        // Pasa la instancia de apiService al constructor de MainViewModel
        BindingContext = new MainViewModel(apiService);
    }
}
