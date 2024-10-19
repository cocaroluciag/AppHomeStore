using AppHomeStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class UsuarioEditarViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [ObservableProperty]
        Usuario _Usuario;
        public UsuarioEditarViewModel()
        {
            Title = "Editar usuario";
        }

        [RelayCommand]
        public async Task EditarUserAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = false;
                    var result = await ApiService.UpdateUserAsync(Usuario.idUsuario, Usuario);
                    if (result != null)
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta!", "Usuario modificado correctamente", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", "ex.Message", "Ok");

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
