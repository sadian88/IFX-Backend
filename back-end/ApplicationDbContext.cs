using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IFXApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFXApi
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EmpleadosEntidades>()
               .HasKey(x => new { x.EmpleadoId, x.EntidadId });

          

            base.OnModelCreating(modelBuilder);
        }

       
        public DbSet<Entidad> Entidad { get; set; }

        public DbSet<Empleado> Empleado { get; set; }
     
        public DbSet<EmpleadosEntidades> EmpleadosEntidades { get; set; }
        
        
    }
}
