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
    public class EventoPersistence : IGeralPersist, IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoPersistence(ProEventosContext context)
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

        public async Task<Evento> GetAllEventosByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lote)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            return await query.Where(e => e.Id == EventoId).FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lote)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            query = query.OrderBy(e => e.Id);

            return await query.Where(e => e.Tema.ToLower().Contains(tema)).ToArrayAsync();
        }

         public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos //cria a query e adiciona o lote e a rede social no select
            .Include(e => e.Lote)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante); //inclue os palestrantes

            query = query.OrderBy(e => e.Id); // ordena por id

            return await query.ToArrayAsync(); // traz todos eventos ordenados por id
        }
    }
}