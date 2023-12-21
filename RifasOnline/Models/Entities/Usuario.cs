using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RifasOnline.Models.Entities
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Correo { get; set; }

        public string? Clave { get; set; }

        [NotMapped]
        public string? ConfirmarClave { get; set; }
        public bool? Restablecer { get; set; }

        [NotMapped]
        public bool? Confirmado { get; set; }
        public string? Token { get; set; }
    }
}
