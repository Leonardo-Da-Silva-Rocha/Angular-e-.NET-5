using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IPalestrantePersist _palestrantePersist;
        public PalestranteService(IGeralPersist geralPersist, IPalestrantePersist palestrantePersist)
        {
            _geralPersist = geralPersist;
            _palestrantePersist = palestrantePersist;
        }

        public async Task<Palestrante> AddPalestrante(Palestrante model)
        {
            try
            {
                _geralPersist.Add<Palestrante>(model);

                if(await _geralPersist.SaveChangesAsync())
                    return await _palestrantePersist.GetAllPalestranteByIdAsync(model.Id, false);

                return null;    

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public async Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model)
        {
            try
            {
                var palestrante = _palestrantePersist.GetAllPalestranteByIdAsync(palestranteId, false);

                if(palestrante == null) return null;

                model.Id = palestrante.Id;

                _geralPersist.Update<Palestrante>(model);

                if(await _geralPersist.SaveChangesAsync()) 
                    return await _palestrantePersist.GetAllPalestranteByIdAsync(model.Id, false);
                
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePalestrante(int palestranteId)
        {
            try
            {
                var palestrante = await _palestrantePersist.GetAllPalestranteByIdAsync(palestranteId, false);

                if(palestrante == null) throw new Exception("Palestrante não encontrado para deleção");

                _geralPersist.Delete<Palestrante>(palestrante);

                return await _geralPersist.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos)
        {
            throw new NotImplementedException();
        }

       
    }
}