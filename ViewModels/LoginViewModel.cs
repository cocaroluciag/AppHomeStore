using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppHomeStore.Views;
using System.Net;
using AppHomeStore.Models.Dto;

namespace AppHomeStore.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string nombreUsuario;

        [ObservableProperty]
        private string clave;

        [ObservableProperty]
        private bool autenticado;

        public LoginViewModel()
        {
            Title = "Iniciar Sesion";
        }
        /*[RelayCommand]
        public async Task LoginAsync() 
        {
            if (!IsBusy)// si la app esta ocupada con otra tarea, aunque le des al boton no hará nada
            {
                try
                { //metemos todo en un trycatch por si hay algun error, asi no se cae la app
                    UsuarioLoginDto loginDto = new UsuarioLoginDto()
                    {
                        NombreUsuario = NombreUsuario,
                        Clave = Clave
                    };
                    IsBusy = true; //La app comienza a estar ocupada con esta tarea
                    var response = await ApiService.ValidarCredenciales(NombreUsuario, Clave);

                    if (response != null)
                    {// si la respuesta no es null es porque el login funciona y te lleva al main
                        IsBusy = false;
                        await Application.Current.MainPage.Navigation.PushAsync(new UsuarioListaPage());
                    }
                    else
                    {//si la respuesta es nulla hubo un error en ApiService
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error!", "Credenciales invalidas", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    // si hay algun error lo mostrará con una alaerta
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                    IsBusy = false;
                }
                finally
                {
                    IsBusy = false; 
                }
            }
        }*/

        [RelayCommand]
        public async Task LoginAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                // asignamos objeto con datos del usuario-establecimiento logueados
                if (NombreUsuario != string.Empty && Clave != String.Empty)
                {
                    var apiClient = new ApiService();

                    LoginResponseDto login = await apiClient.ValidarCredenciales(NombreUsuario, Clave);



                    if (login != null)
                    {
                        Application.Current.MainPage = new NavigationPage(new MainPage());

                        // TODO: recuperar datos de usuario login
                        Transport.IdUsuario = login.idUsuario;
                        Transport.NombreUsuario = login.NombreUsuario;
                        Transport.CorreoUsuario = login.Correo;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales ingresadas no son válidas", "Aceptar");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales son Obligatorias. Verifique!", "Aceptar");
                }

                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Cerrar()
        {
            bool Confirmacion = await Application.Current.MainPage.DisplayAlert(
                       "Confirmar",
                       "¿Estás seguro de que deseas cerrar la aplicación?",
                       "Sí",
                       "No");

            if (Confirmacion)
            {
                // Lógica para cerrar la aplicación
                Application.Current.Quit();
            }
        }
    }
}
