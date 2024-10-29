using AppHomeStore.Views;

namespace AppHomeStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ListaProductos", typeof(ProductoListaPage));
            Routing.RegisterRoute("DetalleProducto", typeof(ProductoDetallePage));
            Routing.RegisterRoute("Carrito", typeof(CarritoListaPage));
            Routing.RegisterRoute("Usuarios", typeof(UsuarioListaPage));
           // Routing.RegisterRoute("Acerca", typeof(AcercaPage));
        }
    }
}
