using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IFXApi.DTOs;
using IFXApi.Entidades;
using IFXApi.Filtros;
using IFXApi.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ILogger<EmpleadosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EmpleadosController(
            ILogger<EmpleadosController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EmpleadoDTO>>> Todos()
        {
            var empleados = await context.Empleado.ToListAsync();
            return mapper.Map<List<EmpleadoDTO>>(empleados);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int Id)
        {
            var genero = await context.Empleado.FirstOrDefaultAsync(x => x.Id == Id);

            if (genero == null)
            {
                return NotFound();
            }

            return mapper.Map<EmpleadoDTO>(genero);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] EmpleadoCreacionDTO empleadoCreacionDTO)
        {
            var emp = mapper.Map<Empleado>(empleadoCreacionDTO);
            context.Add(emp);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] EmpleadoCreacionDTO empleadoCreacionDTO)
        {
            var empleado = await context.Empleado.FirstOrDefaultAsync(x => x.Id == Id);

            if (empleado == null)
            {
                return NotFound();
            }

            empleado = mapper.Map(empleadoCreacionDTO, empleado);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Empleado.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Empleado() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

 
    }
}
