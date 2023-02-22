using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IGeralPersist, IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }


        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return  (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedeSocials);

            if(includeEventos)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(e => e.Evento);


            return await query.Where(p => p.Id == PalestranteId).FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedeSocials);

            if(includeEventos)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(e => e.Evento);

            query = query.OrderBy(p => p.Id);

            return await query.Where(p => p.Nome.ToLower().Contains(nome)).ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedeSocials);
            
            if(includeEventos)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(e => e.Evento);

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
    }
}