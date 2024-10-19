using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        MainViewModel viewModel = new MainViewModel();
        this.BindingContext = viewModel;
    }
}