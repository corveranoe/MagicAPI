using System.ComponentModel.DataAnnotations;

namespace Magic_API.Modelos.Dto
{
    public class MagicCreateDto
    {

        [Required]
        [MaxLength(30)]
        public required string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set;}
    }
}
