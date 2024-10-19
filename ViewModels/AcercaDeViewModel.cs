using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.ViewModels
{
    public partial class AcercaDeViewModel : BaseViewModel
    {
        public AcercaDeViewModel()
        {
            Title = "Acerda del programador";
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
