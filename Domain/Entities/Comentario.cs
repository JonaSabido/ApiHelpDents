using System;
using System.Collections.Generic;

#nullable disable

namespace ApiHelpDents.Domain.Entities
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public int ClaveUsuario { get; set; }
        public int ClaveAsesor { get; set; }
        public string Descripcion { get; set; }

        public virtual Asesor ClaveAsesorNavigation { get; set; }
        public virtual Usuario ClaveUsuarioNavigation { get; set; }
    }
}
