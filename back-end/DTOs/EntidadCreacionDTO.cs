using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IFXApi.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi.DTOs
{
    public class EntidadCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 300)]
        public string Nombre { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> EmpleadosIds { get; set; }
    }
}
