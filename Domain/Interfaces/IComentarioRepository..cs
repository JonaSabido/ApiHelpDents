using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApiHelpDents.Domain.Entities;


namespace ApiHelpDents.Domain.Interfaces
{
    public interface IComentarioRepository
    {
        Task<IQueryable<Comentario>> GetAll();
        Task<IQueryable<Comentario>> GetByIdAsesor(int idAsesor);
        Task<int> Create(Comentario comentario);
        Task<bool> Delete(int id);
        bool Exist(Expression<Func<Comentario, bool>> expression);
    }
}