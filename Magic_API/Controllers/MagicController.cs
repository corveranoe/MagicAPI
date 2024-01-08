using Magic_API.Datos;
using Magic_API.Modelos;
using Magic_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Magic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagicController : ControllerBase
    {
        private readonly ILogger<MagicController> _logger;
        private readonly ApplicationDbContext _db;
        public MagicController(ILogger<MagicController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MagicDto>> GetMagics()
        {
            _logger.LogInformation("Todos los registros se estan obteniendo");
            return Ok(_db.Magics.ToList());
        }

        [HttpGet("id:int", Name = "GetMagic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MagicDto> GetMagic(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer el registro con Id " + id);
                return BadRequest();
            }
            //var Magic = MagicStore.MagicList.FirstOrDefault(m => m.Id == id);
            var magic = _db.Magics.FirstOrDefault(m => m.Id == id);

            if (magic == null)
            {
                return NotFound();
            }

            return Ok(magic);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MagicDto> CrearMagic([FromBody] MagicDto magicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Magics.FirstOrDefault(m => m.Nombre.ToLower() == magicDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Nombre_Existente", "La Magic con ese nombre ya existe");  
                return BadRequest(ModelState);
            }

            if (magicDto == null)
            {
                return BadRequest(magicDto);
            }
            if (magicDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Magic modelo = new()
            {
                //Id = magicDto.Id,
                Nombre = magicDto.Nombre,
                Detalle = magicDto.Detalle,
                ImagenUrl = magicDto.ImagenUrl,
                Ocupantes = magicDto.Ocupantes,
                Tarifa = magicDto.Tarifa,
                MetrosCuadrados = magicDto.MetrosCuadrados,
                Amenidad = magicDto.Amenidad
            };

            _db.Magics.Add(modelo);
            _db.SaveChanges();

            //magicDto.Id = MagicStore.MagicList.OrderByDescending(m => m.Id).FirstOrDefault().Id + 1;
            //MagicStore.MagicList.Add(magicDto);

            return CreatedAtRoute("GetMagic", new { id = magicDto.Id }, magicDto);
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMagic(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var magic = _db.Magics.FirstOrDefault(v => v.Id == id);
            if (magic == null)
            {
                return NotFound();
            }
            _db.Magics.Remove(magic);
            _db.SaveChanges();
            //MagicStore.MagicList.Remove(magic);
            return NoContent();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMagic(int id, [FromBody] MagicDto magicDto)
        {
            if (magicDto==null || id!= magicDto.Id)
            {
                return BadRequest();
            }
            Magic modelo = new()
            {
                Id = magicDto.Id,
                Nombre = magicDto.Nombre,
                Detalle = magicDto.Detalle,
                ImagenUrl = magicDto.ImagenUrl,
                Ocupantes = magicDto.Ocupantes,
                Tarifa = magicDto.Tarifa,
                MetrosCuadrados = magicDto.MetrosCuadrados,
                Amenidad = magicDto.Amenidad
            };
            _db.Magics.Update(modelo);
            _db.SaveChanges();
            return NoContent();
            //var magic = MagicStore.MagicList.FirstOrDefault(m => m.Id == id);
            //magic.Nombre = magicDto.Nombre;
            //magic.Ocupantes = magicDto.Ocupantes;
            //magic.MetrosCuadrados = magicDto.MetrosCuadrados;
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMagic(int id, JsonPatchDocument<MagicDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var magic = MagicStore.MagicList.FirstOrDefault(m => m.Id == id);
            var magic = _db.Magics.AsNoTracking().FirstOrDefault(m => m.Id == id);
            MagicDto magicDto = new()
            {
                Id = magic.Id,
                Nombre = magic.Nombre,
                Detalle = magic.Detalle,
                ImagenUrl = magic.ImagenUrl,
                Ocupantes = magic.Ocupantes,
                Tarifa = magic.Tarifa,
                MetrosCuadrados = magic.MetrosCuadrados,
                Amenidad = magic.Amenidad
            };
            if (magic == null) return BadRequest();

            patchDto.ApplyTo(magicDto, ModelState);

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            Magic modelo = new()
            {
                Id = magicDto.Id,
                Nombre = magicDto.Nombre,
                Detalle = magicDto.Detalle,
                ImagenUrl = magicDto.ImagenUrl,
                Ocupantes = magicDto.Ocupantes,
                Tarifa = magicDto.Tarifa,
                MetrosCuadrados = magicDto.MetrosCuadrados,
                Amenidad = magicDto.Amenidad
            };

            _db.Magics.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
