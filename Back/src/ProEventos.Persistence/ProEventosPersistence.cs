using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
namespace ProEventos.Persistence
{
    public class ProEventosPersistence : IProEventosPersistence
    {

        private readonly ProEventosContext _context;

        public ProEventosPersistence(ProEventosContext context)
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

            return await query.Where(e => e.Tema.Contains(tema)).ToArrayAsync();
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

            return await query.Where(p => p.Nome.Contains(nome)).ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos //cria a query e adiciona o lote e a rede social no select
            .Include(e => e.Lote)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante); //inclue os palestrantes

            query = query.OrderBy(e => e.Id); // ordena por id

            return await query.ToArrayAsync(); // traz todos eventos ordenados por id
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(string nome, bool includeEventos)
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