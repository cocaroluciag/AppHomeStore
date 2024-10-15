using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Input;
using System;
using AppHomeStore.Models;

namespace AppHomeStore.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string nombreUsuario;
        private string clave;
        private bool autenticado;

        public string NombreUsuario
        {
            get => nombreUsuario;
            set
            {
                nombreUsuario = value;
                OnPropertyChanged();
            }
        }

        public string Clave
        {
            get => clave;
            set
            {
                clave = value;
                OnPropertyChanged();
            }
        }

        public bool Autenticado
        {
            get => autenticado;
            set
            {
                autenticado = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        public async Task LoginAsync()
        {
            var loginDto = new LoginResponseDto
            {
                NombreUsuario = NombreUsuario,
                Clave = Clave
            };

            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://localhost:7269/api/usuario", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponseDto>(responseContent);
                    Autenticado = result.Autenticado;
                }
                else
                {
                    Autenticado = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
