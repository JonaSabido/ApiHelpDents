using System;
using System.Collections.Generic;

#nullable disable

namespace ApiHelpDents.Domain.Entities
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Asesors = new HashSet<Asesor>();
        }

        public int IdEsp { get; set; }
        public string NombreEsp { get; set; }

        public virtual ICollection<Asesor> Asesors { get; set; }
    }
}
