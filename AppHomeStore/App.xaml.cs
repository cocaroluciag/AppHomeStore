using AppHomeStore.Views;

namespace AppHomeStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new ProductoListaPage(new ViewModels.ProductoListaViewModel()));
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new LoginPage());
        }
    }
}
