using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppHomeStore.Utils;
using CommunityToolkit.Mvvm.Input;

namespace AppHomeStore.ViewModels
{
    public partial class ModificarCarritoViewModel : BaseViewModel
    {
        public ModificarCarritoViewModel()
        {
            //Title = Constants.AppName;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task Grabar()
        {
            await Application.Current.MainPage.DisplayAlert("Carrito", "Carrito modificado", "Aceptar");
        }
    }
}
