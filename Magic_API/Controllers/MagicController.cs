using AutoMapper;
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
        private readonly IMapper _mapper;
        public MagicController(ILogger<MagicController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<IEnumerable<MagicDto>>> GetMagics()
        {
            _logger.LogInformation("Todos los registros se estan obteniendo");
            IEnumerable<Magic> magicList = await _db.Magics.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MagicDto>>(magicList));
        }

        [HttpGet("id:int", Name = "GetMagic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MagicDto>> GetMagic(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer el registro con Id " + id);
                return BadRequest();
            }
            //var Magic = MagicStore.MagicList.FirstOrDefault(m => m.Id == id);
            var magic = await _db.Magics.FirstOrDefaultAsync(m => m.Id == id);

            if (magic == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MagicDto>(magic));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MagicDto>> CrearMagic([FromBody] MagicCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Magics.FirstOrDefaultAsync(m => m.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Nombre_Existente", "La Magic con ese nombre ya existe");  
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            Magic modelo = _mapper.Map<Magic>(createDto);

            //if (magicDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            //Magic modelo = new()
            //{
            //    //Id = magicDto.Id,
            //    Nombre = magicDto.Nombre,
            //    Detalle = magicDto.Detalle,
            //    ImagenUrl = magicDto.ImagenUrl,
            //    Ocupantes = magicDto.Ocupantes,
            //    Tarifa = magicDto.Tarifa,
            //    MetrosCuadrados = magicDto.MetrosCuadrados,
            //    Amenidad = magicDto.Amenidad
            //};

            await _db.Magics.AddAsync(modelo);
            await _db.SaveChangesAsync();

            //magicDto.Id = MagicStore.MagicList.OrderByDescending(m => m.Id).FirstOrDefault().Id + 1;
            //MagicStore.MagicList.Add(magicDto);

            return CreatedAtRoute("GetMagic", new { id = modelo.Id }, modelo);
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMagic(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var magic = await _db.Magics.FirstOrDefaultAsync(v => v.Id == id);
            if (magic == null)
            {
                return NotFound();
            }
            _db.Magics.Remove(magic);
            await _db.SaveChangesAsync();
            //MagicStore.MagicList.Remove(magic);
            return NoContent();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMagic(int id, [FromBody] MagicUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.Id)
            {
                return BadRequest();
            }

            Magic modelo = _mapper.Map<Magic>(updateDto);

            //Magic modelo = new()
            //{
            //    Id = magicDto.Id,
            //    Nombre = magicDto.Nombre,
            //    Detalle = magicDto.Detalle,
            //    ImagenUrl = magicDto.ImagenUrl,
            //    Ocupantes = magicDto.Ocupantes,
            //    Tarifa = magicDto.Tarifa,
            //    MetrosCuadrados = magicDto.MetrosCuadrados,
            //    Amenidad = magicDto.Amenidad
            //};
            _db.Magics.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
            //var magic = MagicStore.MagicList.FirstOrDefault(m => m.Id == id);
            //magic.Nombre = magicDto.Nombre;
            //magic.Ocupantes = magicDto.Ocupantes;
            //magic.MetrosCuadrados = magicDto.MetrosCuadrados;
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMagic(int id, JsonPatchDocument<MagicUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var magic = MagicStore.MagicList.FirstOrDefault(m => m.Id == id);
            var magic = await _db.Magics.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            MagicUpdateDto magicDto = _mapper.Map<MagicUpdateDto>(magic);

            //MagicUpdateDto magicDto = new()
            //{
            //    Id = magic.Id,
            //    Nombre = magic.Nombre,
            //    Detalle = magic.Detalle,
            //    ImagenUrl = magic.ImagenUrl,
            //    Ocupantes = magic.Ocupantes,
            //    Tarifa = magic.Tarifa,
            //    MetrosCuadrados = magic.MetrosCuadrados,
            //    Amenidad = magic.Amenidad
            //};

            if (magic == null) return BadRequest();

            patchDto.ApplyTo(magicDto, ModelState);

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            Magic modelo = _mapper.Map<Magic>(magicDto);

            //Magic modelo = new()
            //{
            //    Id = magicDto.Id,
            //    Nombre = magicDto.Nombre,
            //    Detalle = magicDto.Detalle,
            //    ImagenUrl = magicDto.ImagenUrl,
            //    Ocupantes = magicDto.Ocupantes,
            //    Tarifa = magicDto.Tarifa,
            //    MetrosCuadrados = magicDto.MetrosCuadrados,
            //    Amenidad = magicDto.Amenidad
            //};

            _db.Magics.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent(); 
        }
    }
}
