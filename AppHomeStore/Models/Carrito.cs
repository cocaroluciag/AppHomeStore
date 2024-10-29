using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; } // Identificador único del carrito
        public int IdUsuario { get; set; } // Relación 1:1 con Usuario
        public DateTime FechaCreacion { get; set; } // Fecha de creación del carrito
    }
}
