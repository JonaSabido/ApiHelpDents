using System;
using System.Collections.Generic;

#nullable disable

namespace ApiHelpDents.Domain.Entities
{
    public partial class Asesor
    {
        public Asesor()
        {
            Comentarios = new HashSet<Comentario>();
        }

        public int IdAsesor { get; set; }
        public int ClaveUsuario { get; set; }
        public int ClaveEsp { get; set; }
        public int ClaveTurno { get; set; }
        public double? Costo { get; set; }
        public string Telefono { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Twitter { get; set; }
        public string Descripcion { get; set; }
        public virtual Especialidad ClaveEspNavigation { get; set; }
        public virtual Turno ClaveTurnoNavigation { get; set; }
        public virtual Usuario ClaveUsuarioNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
