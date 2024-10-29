using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string NumeroTelefonico { get; set; }
        public string NroDocumento { get; set; }
        public string Clave { get; set; }
    }
}
