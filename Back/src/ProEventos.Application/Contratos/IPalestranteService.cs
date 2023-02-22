using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrante(Palestrante model);

        Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model);

        Task<bool> DeletePalestrante(int palestranteId);

         Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos);
        
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);

        Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos);
    }
}