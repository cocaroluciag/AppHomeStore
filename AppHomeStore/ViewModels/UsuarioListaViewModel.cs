using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppHomeStore.ViewModels
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        private int idUsuario;
        private string nombreUsuario;
        private string correo;
        private string numeroTelefonico;
        private string nroDocumento;

        public int IdUsuario
        {
            get => idUsuario;
            set
            {
                idUsuario = value;
                OnPropertyChanged();
            }
        }

        public string NombreUsuario
        {
            get => nombreUsuario;
            set
            {
                nombreUsuario = value;
                OnPropertyChanged();
            }
        }

        public string Correo
        {
            get => correo;
            set
            {
                correo = value;
                OnPropertyChanged();
            }
        }

        public string NumeroTelefonico
        {
            get => numeroTelefonico;
            set
            {
                numeroTelefonico = value;
                OnPropertyChanged();
            }
        }

        public string NroDocumento
        {
            get => nroDocumento;
            set
            {
                nroDocumento = value;
                OnPropertyChanged();
            }
        }

        // Evento para notificar cambios
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        public async Task GoToDetailAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetallePage(usuarioSeleccionado), true);
        }
    }
}
