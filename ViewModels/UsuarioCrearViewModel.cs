using AppHomeStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class UsuarioCrearViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string nombreUsuario;

        [ObservableProperty]
        public string correo;

        [ObservableProperty]
        public string numeroTelefonico;

        [ObservableProperty]
        public string nroDocumento;

        [ObservableProperty]
        public string clave;

        [ObservableProperty]
        public string confirmarClave;

        // Este es el comando que el botón ejecutará
        [RelayCommand]
        public async Task AddUserAsync()
        {
            if (!string.IsNullOrEmpty(NombreUsuario) || !string.IsNullOrEmpty(Clave))
            {
                if (Clave == ConfirmarClave)
                {
                    //await App.Current.MainPage.DisplayAlert("Mensaje", "El boton funicona.", "OK");

                    var response = await ApiService.CreateUserAsync(NombreUsuario, Correo, NumeroTelefonico, NroDocumento, clave);

                    if (response != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Exito", "Usuario creador correctamente.", "OK");
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudo crear el Usuario", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Las Claves no coinciden", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
            }
        }
    }
}