using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiHelpDents.Domain.Entities;
using ApiHelpDents.Domain.Interfaces;
using ApiHelpDents.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ApiHelpDents.Infraestructure.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {

        private readonly HelpDentsBDContext _context;

        public ComentarioRepository(HelpDentsBDContext context)
        {
            this._context = context;

        }

        public async Task<IQueryable<Comentario>> GetByIdAsesor(int id){

            var query = _context.Comentarios.AsQueryable();
            query = query.Where(x => x.ClaveAsesor == id);
            var result = await query.ToListAsync();
            return result.AsQueryable();

        }
        public async Task<IQueryable<Comentario>> GetAll()
        {
            
            var query = await _context.Comentarios.AsQueryable<Comentario>().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<int> Create(Comentario coment){

            var entity = coment;
            await _context.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows<=0){
                throw new Exception("No pudo realizarse el registro");
            }

            return entity.IdComentario;
        }

        public async Task<bool> Delete(int id){

            if(id<=0){
                throw new ArgumentException("Proceso Fallado");
            }

            var entity = await _context.Comentarios.FirstOrDefaultAsync(x => x.IdComentario == id);

            _context.Remove(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public bool Exist(Expression<Func<Comentario, bool>> expression)
        {
            return _context.Comentarios.Any(expression);
        }
    }
}