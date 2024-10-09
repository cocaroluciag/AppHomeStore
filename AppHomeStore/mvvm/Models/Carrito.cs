using AppHomeStore.Models;

namespace AppHomeStore.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedad de navegación
        public Usuario Usuario { get; set; }
    }
}
