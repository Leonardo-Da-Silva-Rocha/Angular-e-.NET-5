using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public interface IProEventosPersistence
    {
        // Itens Gerais
        // Todo adicional, alterar e deletar serão feitos utilizando esses metodos
        // classe criada com a finalidade de ser generica para não repetir codigo
        
        void Add<T>(T entity) where T: class;

        void Update<T>(T entity) where T: class;

        void Delete<T>(T entity) where T:class;

        void DeleteRange<T>(T[] entity) where T:class;

        Task<bool> SaveChangesAsync();

        //EVENTOS
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);

        Task<Evento[]> GetAllEventosAsync(string tema, bool includePalestrantes);

        Task<Evento> GetAllEventosByIdAsync(int EventoId, bool includePalestrantes);

        //PALESTRANTES
        Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos);

        Task<Palestrante[]> GetAllPalestrantesAsync(string nome, bool includeEventos);

        Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos);
    }
}