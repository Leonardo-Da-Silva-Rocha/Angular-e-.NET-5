using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence;
using ProEventos.Domain;
using System.Linq;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly ProEventosContext _context;
        
        public EventoController(ProEventosContext context)
        {
            _context = context;
        }

         [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;             
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _context.Eventos.FirstOrDefault(x => x.Id == id);             
        }

        [HttpPost]
        public string Post(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
            return "ok";
        }
    } 
}