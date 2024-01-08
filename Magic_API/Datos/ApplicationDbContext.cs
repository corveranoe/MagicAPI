using Magic_API.Modelos;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Magic_API.Datos
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Magic> Magics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Magic>().HasData(
                new Magic()
                {
                    Id = 1,
                    Nombre = "Noe Corvera",
                    Detalle = "Desarrollo de una API y consumirla",
                    ImagenUrl = "",
                    Amenidad = "otros",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 37,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Magic()
                {
                    Id = 2,
                    Nombre = "Danely Alas",
                    Detalle = "Creacion de una API",
                    ImagenUrl = "",
                    Amenidad = "otros",
                    Ocupantes = 4,
                    MetrosCuadrados = 30,
                    Tarifa = 20,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
           );
        }
    }
}
