using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence;
using ProEventos.Domain;
using System.Linq;
using ProEventos.Persistence.Contexto;
using ProEventos.Application.Contratos;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

         [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);

                if(eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);

            }catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Eroo ao tentar recuperar eventos. erro: {ex.Message}");
            }        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByIdAsync(id, true);

                if(evento == null) return NotFound("Evento não encontrado");

                return Ok(evento);

            }catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento. Erro{ex.Message}");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                var eventoReturn = await _eventoService.AddEventos(evento);

                if(eventoReturn == null) return BadRequest("Erro ao tentar adicionar evento");

                return Ok(eventoReturn);
                
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao inserir evento. Erro {ex.Message} ");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento evento)
        {
            try
            {
                var eventoUpdate = await _eventoService.UpdateEventos(id, evento);

                if(eventoUpdate == null) return BadRequest("Erro ao tentar alterar evento");

                return Ok(eventoUpdate);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar evento. Erro {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _eventoService.DeleteEventos(id);

                if(!result) return BadRequest("Erro ao deletar evento");

                return Ok("Evento deletado com sucesso!");

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento. Erro {ex.Message}");
            }
        }
    } 
}