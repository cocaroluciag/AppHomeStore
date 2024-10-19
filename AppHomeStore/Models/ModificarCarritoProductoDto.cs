using System.ComponentModel.DataAnnotations;

namespace AppHomeStore.Models
{
    public class ModificarCarritoProductoDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
    }
}
