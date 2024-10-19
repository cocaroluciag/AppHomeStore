using System;
using System.Threading.Tasks;
using AppHomeStore.Views;
using AppHomeStore.Utils;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using AppHomeStore.Models;

namespace AppHomeStore.ViewModels
{
    public partial class AgregarCarritoViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        [ObservableProperty]
        private int _idCarrito;
        private int _idUsuario;  // El ID del usuario actual

        [ObservableProperty] private int _idProducto;
        [ObservableProperty] private int _cantidad;

        public Command AgregarProductoCommand { get; }


        public AgregarCarritoViewModel(ApiService apiService, int idUsuario)
        {
            _apiService = apiService;
            _idUsuario = idUsuario;

            AgregarProductoCommand = new Command(async () => await AgregarProductoAlCarrito());
        }

        [RelayCommand]
        private async Task AgregarProductoAlCarrito()
        {
            // Verifica que el carrito existe
            int idCarrito = await ObtenerIdCarritoPorUsuario(_idUsuario);

            if (idCarrito == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No existe un carrito para este usuario.", "OK");
                return;
            }

            // Verifica si el producto existe y si hay suficiente stock

            bool productoExiste = await _apiService.ExisteProducto(IdProducto, Cantidad);
            if (!productoExiste)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El producto no existe o no hay suficiente stock.", "OK");
                return;
            }

            // Agrega el producto al carrito
            var resultado = await _apiService.CrearDetalleCarrito(new CrearCarritoProductoDto
            {
                IdProducto = IdProducto,
                IdCarrito = idCarrito,
                Cantidad = Cantidad
            });

            if (resultado)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Producto agregado al carrito", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el producto al carrito", "OK");
            }
        }

        private async Task<int> ObtenerIdCarritoPorUsuario(int idUsuario)
        {
            var carrito = await _apiService.ObtenerCarritoPorUsuario(idUsuario);
            return carrito?.IdCarrito ?? 0;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}