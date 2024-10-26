using AppHomeStore.ViewModels;
using AppHomeStore.Views;
using AppHomeStore.Models;
using Microsoft.Extensions.Logging;

namespace AppHomeStore
{
    public static class MauiProgram
    {
       // private const string BaseAddress = "https://localhost:7269/api/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialDesignIcons");
                });

            //Inicializadores 'Singleton de la clase "Api- Service" '
            builder.Services.AddSingleton<ApiService>();

            //Inicializamos todas las partes de cada sección

            //Sección Login
            builder.Services.AddTransient<LoginViewModel>();

            //Sección Productos
            builder.Services.AddTransient<ProductoListaViewModel>();
            builder.Services.AddTransient<ProductoDetalleViewModel>();
            builder.Services.AddTransient<AgregarProductoViewModel>();
            builder.Services.AddTransient<ModificarProductoViewModel>();

            //Sección usuarios
            builder.Services.AddTransient<UsuarioListaViewModel>();
            builder.Services.AddTransient<UsuarioDetalleViewModel>();
            builder.Services.AddTransient<AgregarUsuarioViewModel>();
            builder.Services.AddTransient<ModificarUsuarioViewModel>();

            //Sección carrito
            builder.Services.AddTransient<CarritoListaViewModel>();
            builder.Services.AddTransient<AgregarCarritoViewModel>();
            builder.Services.AddTransient<ModificarCarritoViewModel>();

            //Sección detalle carrito
            builder.Services.AddTransient<CarritoProductoViewModel>();
           // builder.Services.AddTransient<AgregarCarritoProductoViewModel>();
            //builder.Services.AddTransient<ModificarCarritoProductoViewModel>();



            //Sección Login
         //   builder.Services.AddTransient<LoginPage>();

            //Sección Productos
            builder.Services.AddTransient<ProductoListaPage>();
            builder.Services.AddTransient<ProductoDetallePage>();
            builder.Services.AddTransient<AgregarProductoPage>();
            builder.Services.AddTransient<ModificarProductoPage>();

            //Sección Usuarios
            builder.Services.AddTransient<UsuarioListaPage>();
            builder.Services.AddTransient<UsuarioDetallePage>();
            builder.Services.AddTransient<AgregarUsuarioPage>();
            builder.Services.AddTransient<ModificarUsuarioPage>();

            //Sección Carrito
            builder.Services.AddTransient<CarritoListaPage>();
            builder.Services.AddTransient<AgregarCarritoPage>();
            builder.Services.AddTransient<ModificarCarritoPage>();

            //Sección Carrito Detalle
            builder.Services.AddTransient<CarritoProductoPage>();
           // builder.Services.AddTransient<AgregarCarritoProductoPage>();
           // builder.Services.AddTransient<ModificarCarritoProductoPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
