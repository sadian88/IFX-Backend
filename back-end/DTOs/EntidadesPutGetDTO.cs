using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.DTOs
{
    public class EntidadesPutGetDTO
    {
        public EntidadDTO Entidad { get; set; }
        public List<EmpleadoDTO> EmpleadoSeleccionados { get; set; }
        public List<EmpleadoDTO> EmpleadoNoSeleccionados { get; set; }
    }
}
