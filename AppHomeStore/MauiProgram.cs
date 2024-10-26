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
            builder.Services.AddTransient<EliminarProductoViewModel>();

            //Sección usuarios
            builder.Services.AddTransient<UsuarioListaViewModel>();
            builder.Services.AddTransient<UsuarioDetalleViewModel>();
            builder.Services.AddTransient<AgregarUsuarioViewModel>();
            builder.Services.AddTransient<ModificarUsuarioViewModel>();
            builder.Services.AddTransient<EliminarUsuarioViewModel>();

            //Sección carrito
            builder.Services.AddTransient<CarritoListaViewModel>();
            builder.Services.AddTransient<AgregarCarritoViewModel>();
            builder.Services.AddTransient<ModificarCarritoViewModel>();
            builder.Services.AddTransient<EliminarCarritoViewModel>();

            //Sección detalle carrito
            builder.Services.AddTransient<CarritoProductoViewModel>();
           // builder.Services.AddTransient<AgregarCarritoProductoViewModel>();
            //builder.Services.AddTransient<ModificarCarritoProductoViewModel>();
           // builder.Services.AddTransient<EliminarCarritoProductoViewModel>();



            //Sección Login
         //   builder.Services.AddTransient<LoginPage>();

            //Sección Productos
            builder.Services.AddTransient<ProductoListaPage>();
            builder.Services.AddTransient<ProductoDetallePage>();
            builder.Services.AddTransient<AgregarProductoPage>();
            builder.Services.AddTransient<ModificarProductoPage>();
            //builder.Services.AddTransient<EliminarProductoPage>();

            //Sección Usuarios
            builder.Services.AddTransient<UsuarioListaPage>();
            builder.Services.AddTransient<UsuarioDetallePage>();
            builder.Services.AddTransient<AgregarUsuarioPage>();
            builder.Services.AddTransient<ModificarUsuarioPage>();
            //builder.Services.AddTransient<EliminarUsuarioPage>();

            //Sección Carrito
            builder.Services.AddTransient<CarritoListaPage>();
            builder.Services.AddTransient<AgregarCarritoPage>();
            builder.Services.AddTransient<ModificarCarritoPage>();
           // builder.Services.AddTransient<EliminarCarritoPage>();


            //Sección Carrito Detalle
            builder.Services.AddTransient<CarritoProductoPage>();
           // builder.Services.AddTransient<AgregarCarritoProductoPage>();
           // builder.Services.AddTransient<ModificarCarritoProductoPage>();
           // builder.Services.AddTransient<EliminarCarritoProductoPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
