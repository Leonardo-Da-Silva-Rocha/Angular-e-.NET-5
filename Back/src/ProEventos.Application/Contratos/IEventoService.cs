using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);

        Task<Evento> UpdateEventos(int eventoId,Evento model);

        Task<bool> DeleteEventos(int eventoId);

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);

        Task<Evento> GetAllEventosByIdAsync(int EventoId, bool includePalestrantes);
    }
}