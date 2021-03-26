using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.DTOs
{
    public class EmpleadoCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Nombre { get; set; }
 
    }
}
