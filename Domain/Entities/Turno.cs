using System;
using System.Collections.Generic;

#nullable disable

namespace ApiHelpDents.Domain.Entities
{
    public partial class Turno
    {
        public Turno()
        {
            Asesors = new HashSet<Asesor>();
        }

        public int IdTurno { get; set; }
        public string NombreTurno { get; set; }

        public virtual ICollection<Asesor> Asesors { get; set; }
    }
}
