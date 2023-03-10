using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.SaveChangesAsync())
                    return await _eventoPersist.GetAllEventosByIdAsync(model.Id, false);

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEventos(int eventoId, Evento model)
        {
           try
           {
                var evento = await _eventoPersist.GetAllEventosByIdAsync(eventoId, false);
                
                if(evento == null) return null;
                 
                model.Id = evento.Id;

                _geralPersist.Update(model);

                if(await _geralPersist.SaveChangesAsync())
                    return await _eventoPersist.GetAllEventosByIdAsync(model.Id, false);

                return null;
           }
           catch(Exception ex)
           {
                throw new Exception(ex.Message);
           }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetAllEventosByIdAsync(eventoId, false);

                if(evento == null) throw new Exception("Evento para delete não encontrado");

                _geralPersist.Delete<Evento>(evento);

                if(await _geralPersist.SaveChangesAsync())
                    return true;

                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);

                if(eventos == null) return null;

                return eventos;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetAllEventosByIdAsync(int EventoId, bool includePalestrantes)
        {
            try
            {
                var evento = await _eventoPersist.GetAllEventosByIdAsync(EventoId, includePalestrantes);

                if(evento == null) return null;

                return evento;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var evento = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if(evento == null) return null;

                return evento;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}