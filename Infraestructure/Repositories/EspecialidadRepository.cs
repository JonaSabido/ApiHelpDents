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
    public class EspecialidadRepository : IEspecialidadRepository
    {

        private readonly HelpDentsBDContext _context;

        public EspecialidadRepository (HelpDentsBDContext context)
        {
            this._context = context;

        }
        public async Task<Especialidad> GetById(int id){

            var entity = await _context.Especialidads.FirstOrDefaultAsync(x => x.IdEsp == id);
            return entity;
        }

        public async Task<IQueryable<Especialidad>> GetAll()
        {
            
            var query = await _context.Especialidads.AsQueryable<Especialidad>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<int> Create(Especialidad esp){

            var entity = esp;
            await _context.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows<=0){
                throw new Exception("No pudo realizarse el registro");
            }

            return entity.IdEsp;
        }

        public async Task<bool> Delete(int id){

            if(id<=0){
                throw new ArgumentException("Proceso Fallado");
            }

            var entity = await _context.Especialidads.FirstOrDefaultAsync(x => x.IdEsp == id);

            _context.Remove(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public bool Exist(Expression<Func<Especialidad, bool>> expression)
        {
            return _context.Especialidads.Any(expression);
        }
    }
}