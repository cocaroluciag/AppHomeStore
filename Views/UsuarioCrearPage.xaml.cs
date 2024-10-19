using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class UsuarioCrearPage : ContentPage
{
	public UsuarioCrearPage()
	{
		InitializeComponent();
		BindingContext = new UsuarioCrearViewModel();
	}
}