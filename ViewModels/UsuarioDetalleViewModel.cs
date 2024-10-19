using AppHomeStore.Models;
using AppHomeStore.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class UsuarioDetalleViewModel : BaseViewModel
    {
        [ObservableProperty]
        Usuario _usuario;

        
        public UsuarioDetalleViewModel()
        {
            Title = "Detalle del usuario";
        }
        [RelayCommand]
        public async Task GoToEditUserAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioEditarPage(Usuario), true);

        }
        [RelayCommand]
        public async Task DeleteUsuarioAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    bool Confirmacion = await Application.Current.MainPage.DisplayAlert(
                       "Confirmar",
                       "¿Estás seguro de que deseas borrar el usuario?",
                       "Sí",
                       "No");

                    if (Confirmacion)
                    {
                        var response = ApiService.DeleteUserAsync(Usuario.idUsuario);
                        if (response != null)
                        {
                            IsBusy = false;
                            await App.Current.MainPage.DisplayAlert("Bien!", "Se borró el usuario correctamente", "Ok");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            IsBusy = false;
                            await App.Current.MainPage.DisplayAlert("Error!", "No se pudo borrar el usuario", "Ok");
                        }
                        IsBusy = false;

                    }

                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error Fatal!", $"Ha ocurrido un error: {ex.Message}", "Ok");

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        
        [RelayCommand]
        private async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}