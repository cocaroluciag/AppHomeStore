using AppHomeStore.ViewModels;

namespace AppHomeStore.Views;

public partial class AcercaDePage : ContentPage
{
	public AcercaDePage()
	{
		InitializeComponent();
		BindingContext = new AcercaDeViewModel();
	}
}