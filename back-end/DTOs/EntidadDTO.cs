using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.DTOs
{
    public class EntidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<EmpleadoDTO> Empleados { get; set; }

    }
}
