using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string NumeroTelefonico { get; set; }
        public string NroDocumento { get; set; }

        [Required]
        public string Clave { get; set; }

        //public Carrito Carrito { get; set; }
    }
}
