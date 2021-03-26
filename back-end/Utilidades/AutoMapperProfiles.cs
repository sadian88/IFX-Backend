using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using IFXApi.DTOs;
using IFXApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Empleado, EmpleadoDTO>().ReverseMap();

            CreateMap<Entidad, EntidadDTO>().ReverseMap();

            CreateMap<EmpleadoCreacionDTO, Empleado>().ReverseMap();    

            CreateMap<EntidadCreacionDTO, Entidad>()
            .ForMember(x => x.EmpleadosEntidades, opciones => opciones.MapFrom(MapearEntidadesEmpleados));


            

            CreateMap<Entidad, EntidadDTO>()
                .ForMember(x => x.Empleados, options => options.MapFrom(MapearEmpleadosEntidades));

            CreateMap<IdentityUser, UsuarioDTO>();
        }

        
        private List<EmpleadosEntidades> MapearEntidadesEmpleados(EntidadCreacionDTO entidadCreacionDTO,
            Entidad entidad)
        {
            var resultado = new List<EmpleadosEntidades>();

            if (entidadCreacionDTO.EmpleadosIds == null) { return resultado; }

            foreach (var id in entidadCreacionDTO.EmpleadosIds)
            {
                resultado.Add(new EmpleadosEntidades() { EmpleadoId = id });
            }

            return resultado;
        }

        private List<EmpleadoDTO> MapearEmpleadosEntidades(Entidad entidad, EntidadDTO entidadDTO)
        {
            var resultado = new List<EmpleadoDTO>();

            if (entidad.EmpleadosEntidades != null)
            {
                foreach (var emp in entidad.EmpleadosEntidades)
                {
                    resultado.Add(new EmpleadoDTO() { Id = emp.EmpleadoId, Nombre = emp.Empleado.Nombre });
                }
            }

            return resultado;
        }
    }
}
