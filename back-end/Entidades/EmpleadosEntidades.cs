using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.Entidades
{
    public class EmpleadosEntidades
    {
        public int EmpleadoId { get; set; }
        public int EntidadId { get; set; }
        public Entidad Entidad { get; set; }
        public Empleado Empleado { get; set; }
        
    }
}
