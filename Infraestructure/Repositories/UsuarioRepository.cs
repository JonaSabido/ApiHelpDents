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
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly HelpDentsBDContext _context;

        public UsuarioRepository(HelpDentsBDContext context)
        {
            this._context = context;

        }

        public async Task<Usuario> GetById(int id){

            var entity = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
            return entity;
        }
        public async Task<IQueryable<Usuario>> GetAll()
        {
            
            var query = await _context.Usuarios.AsQueryable<Usuario>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<int> Create(Usuario user){

            var entity = user;
            await _context.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows<=0){
                throw new Exception("No pudo realizarse el registro");
            }

            return entity.IdUsuario;
        }

        public async Task<bool> Update(int id, Usuario user){

            if(id<=0 || user == null){
                throw new ArgumentException("Falta información para continuar con el proceso de modificación...");
            }

            var entity = await GetById(id);

            entity.Nombres = user.Nombres;
            entity.Apellidos = user.Apellidos;
            entity.Contraseña = user.Contraseña;

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

        public bool Exist(Expression<Func<Usuario, bool>> expression)
        {
            return _context.Usuarios.Any(expression);
        }
    }
}