using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RifasOnline.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Correo { get; set; }

        public string? Clave { get; set; }
    }
}
