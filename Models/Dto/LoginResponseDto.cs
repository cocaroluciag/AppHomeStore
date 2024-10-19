using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.Models.Dto
{
    public class LoginResponseDto
    {
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public bool Autenticado { get; set; }
        public string Correo { get; set; }
        public string NumeroTelefonico { get; set; }
        public string NroDocumento { get; set; }
    }
}
