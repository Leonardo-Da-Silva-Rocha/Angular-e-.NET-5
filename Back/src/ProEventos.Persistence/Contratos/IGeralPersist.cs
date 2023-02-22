using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IGeralPersist
    {
        // Itens Gerais
        // Todo adicional, alterar e deletar serão feitos utilizando esses metodos
        // classe criada com a finalidade de ser generica para não repetir codigo
        void Add<T>(T entity) where T: class;

        void Update<T>(T entity) where T: class;

        void Delete<T>(T entity) where T:class;

        void DeleteRange<T>(T[] entity) where T:class;

        Task<bool> SaveChangesAsync();
    }
}