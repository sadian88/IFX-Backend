using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.Entidades
{
    public class Entidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Nombre { get; set; }
       
        public List<EmpleadosEntidades> EmpleadosEntidades { get; set; }
    }
}
