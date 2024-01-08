using System.ComponentModel.DataAnnotations;

namespace Magic_API.Modelos.Dto
{
    public class MagicUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set;}
    }
}
