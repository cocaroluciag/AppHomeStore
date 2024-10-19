using AppHomeStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppHomeStore.Views;

namespace AppHomeStore.ViewModels
{
    public partial class UsuarioViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Usuario> usuarios; // Colección de usuarios

        [ObservableProperty]
        public Usuario usuarioSeleccionado; // Usuario seleccionado

        [ObservableProperty]
        bool _IsRefreshing;

        public UsuarioViewModel()
        {
            Title = "Lista de Usuarios";
            Usuarios = new ObservableCollection<Usuario>(); // Inicializa la colección
        }
        [RelayCommand]
        public async Task LoadUsuarios()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    var usuariosDesdeApi = await ApiService.GetUsersAsync();
                    foreach (var usuario in usuariosDesdeApi)
                    {
                        Usuarios.Add(usuario); // Agrega cada usuario a la colección
                    }
                    IsBusy = false;
                        
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            
        }

        // Comando para crear un usuario nuevo
        [RelayCommand]
        public async Task CrearUsuarioAsync()
        {
            // Navegar a la página de creación de usuario usando la ruta registrada en AppShell.xaml
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioCrearPage());
        }

        // Comando para navegar al detalle del usuario seleccionado
        [RelayCommand]
        public async Task GoToDetail()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetallePage(usuarioSeleccionado), true);
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
