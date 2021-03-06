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
    public class AsesorRepository : IAsesorRepository
    {

        private readonly HelpDentsBDContext _context;

        public AsesorRepository(HelpDentsBDContext context)
        {
            this._context = context;

        }

        public async Task<Asesor> GetById(int id){

            var entity = await _context.Asesors.FirstOrDefaultAsync(x => x.IdAsesor == id);
            return entity;
        }
        public async Task<IQueryable<Asesor>> GetAll()
        {
            
            var query = await _context.Asesors.AsQueryable<Asesor>().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<IQueryable<Asesor>> GetByName(string name){

            if(name == null){
                return new List<Asesor>().AsQueryable();
            }

            var query = _context.Asesors.AsQueryable();
            
            query = query.Where(x => x.ClaveUsuarioNavigation.Nombres.Contains(name));
            

            var result = await query.ToListAsync();

            return result.AsQueryable();
        }

        public async Task<IQueryable<Asesor>> GetByFilter(Asesor asesor)
        {
            if(asesor == null)
                return new List<Asesor>().AsQueryable();

            var query = _context.Asesors.AsQueryable();

            if(!string.IsNullOrEmpty(asesor.ClaveUsuarioNavigation.Nombres))
                query = query.Where(x => x.ClaveUsuarioNavigation.Nombres .Contains(asesor.ClaveUsuarioNavigation.Nombres));
            
            if(!string.IsNullOrEmpty(asesor.ClaveEspNavigation.NombreEsp))
                query = query.Where(x => x.ClaveEspNavigation.NombreEsp == asesor.ClaveEspNavigation.NombreEsp);

            if(!string.IsNullOrEmpty(asesor.ClaveTurnoNavigation.NombreTurno))
                query = query.Where(x => x.ClaveTurnoNavigation.NombreTurno == asesor.ClaveTurnoNavigation.NombreTurno);

            if(asesor.Costo >= 0){
                query = query.Where(x => x.Costo == asesor.Costo);

            }
            
            
            var result = await query.ToListAsync();

            return result.AsQueryable().AsNoTracking();
        }
        public async Task<int> Create(Asesor asesor){

            var entity = asesor;
            await _context.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows<=0){
                throw new Exception("No pudo realizarse el registro");
            }

            return entity.IdAsesor;
        }

        public async Task<bool> Update(int id, Asesor asesor){

            if(id<=0 || asesor == null){
                throw new ArgumentException("Falta informaci??n para continuar con el proceso de modificaci??n...");
            }

            var entity = await GetById(id);

            entity.ClaveEsp = asesor.ClaveEsp;
            entity.ClaveTurno = asesor.ClaveTurno;
            entity.Costo = asesor.Costo;
            entity.Telefono = asesor.Telefono;
            entity.Facebook = asesor.Facebook;
            entity.Instagram = asesor.Instagram;
            entity.Youtube = asesor.Youtube;
            entity.Twitter = asesor.Twitter;
            entity.Descripcion = asesor.Descripcion;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id){

            if(id<=0){
                throw new ArgumentException("Proceso Fallado");
            }

            var entity = await GetById(id);

            _context.Remove(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public bool Exist(Expression<Func<Asesor, bool>> expression)
        {
            return _context.Asesors.Any(expression);
        }
    }
}