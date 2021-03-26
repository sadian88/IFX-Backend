using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IFXApi.DTOs;
using IFXApi.Entidades;
using IFXApi.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.Controllers
{
    [ApiController]
    [Route("api/entidades")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class EntidadesController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public EntidadesController(ApplicationDbContext context,
                IMapper mapper,
                UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EntidadDTO>>> Todos()
        {
            var entidades = await context.Entidad.ToListAsync();
            return mapper.Map<List<EntidadDTO>>(entidades);
        }

        //[HttpGet("{id:int}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<List<EntidadDTO>>> Get(int id)
        //{
        //    var entidades = await context.Entidad.Where( x  => x.Id == id).ToListAsync();
        //    return mapper.Map<List<EntidadDTO>>(entidades);

        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Post([FromForm] EntidadCreacionDTO entidadCreacionDTO)
        {
            var entidad = mapper.Map<Entidad>(entidadCreacionDTO);
          
            context.Add(entidad);
            await context.SaveChangesAsync();
            return entidad.Id;
        }


        [HttpGet("PostGet")]
        public async Task<ActionResult<EntidadesPostGetDTO>> PostGet()
        {

            var empleados = await context.Empleado.ToListAsync();

            
            var empleadosDTO = mapper.Map<List<EmpleadoDTO>>(empleados);

            return new EntidadesPostGetDTO() { Empleados = empleadosDTO };
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<EntidadDTO>> Get(int id)
        {
            var entidad = await context.Entidad
                .Include(x => x.EmpleadosEntidades).ThenInclude(x => x.Empleado)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null) { return NotFound(); }

            var dto = mapper.Map<EntidadDTO>(entidad);
           
            return dto;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entidad = await context.Entidad.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            context.Remove(entidad);
            await context.SaveChangesAsync();
            return NoContent();
        }


        //[HttpGet("PutGet/{id:int}")]
        //public async Task<ActionResult<EntidadesPutGetDTO>> PutGet(int id)
        //{
        //    var entidadActionResult = await Get(id);
        //    if (entidadActionResult.Result is NotFoundResult) { return NotFound(); }

        //    var entidad = entidadActionResult.Value;

        //    var empleadosSeleccionadosIds = entidad.Select(x => x.Id).ToList();
        //    var generosNoSeleccionados = await context.Generos
        //        .Where(x => !generosSeleccionadosIds.Contains(x.Id))
        //        .ToListAsync();

        //    var cinesSeleccionadosIds = entidad.Cines.Select(x => x.Id).ToList();
        //    var cinesNoSeleccionados = await context.Cines
        //        .Where(x => !cinesSeleccionadosIds.Contains(x.Id))
        //        .ToListAsync();

        //    var generosNoSeleccionadosDTO = mapper.Map<List<GeneroDTO>>(generosNoSeleccionados);
        //    var cinesNoSeleccionadosDTO = mapper.Map<List<CineDTO>>(cinesNoSeleccionados);

        //    var respuesta = new PeliculasPutGetDTO();
        //    respuesta.Pelicula = pelicula;
        //    respuesta.GenerosSeleccionados = pelicula.Generos;
        //    respuesta.GenerosNoSeleccionados = generosNoSeleccionadosDTO;


        //    return respuesta;
        //}
    }
}
