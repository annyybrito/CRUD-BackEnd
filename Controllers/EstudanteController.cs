using CRUD_Estudantes_Inspirali.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Estudantes_Inspirali.Controllers
{
    [Route("api/estudantes")]
    [ApiController]
    public class EstudanteController : ControllerBase
    {
        private readonly EstudanteDBContext _context;

        public EstudanteController(EstudanteDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudante>>> GetEstudantes()
        {
            var estudantes = await _context.Estudantes
                .OrderByDescending(e => e.DataUltimaEdicao) 
                .ToListAsync();

            return estudantes;
        }

        [HttpGet("buscar-por-nome")]
        public async Task<ActionResult<IEnumerable<Estudante>>> GetEstudantesByNome([FromQuery] string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("O parâmetro 'nome' não pode ser nulo ou vazio.");
            }

            var estudantes = await _context.Estudantes
                .Where(e => EF.Functions.ILike(e.Nome, $"%{nome}%"))
                .ToListAsync();

            if (estudantes == null || estudantes.Count == 0)
            {
                return NotFound($"Nenhum estudante encontrado com o nome contendo '{nome}'.");
            }

            return estudantes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estudante>> GetEstudante(int id)
        {
            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null)
            {
                return NotFound(); // Retorna 404 Not Found se o estudante não for encontrado
            }

            return estudante;
        }

        [HttpPost]
        public async Task<ActionResult<Estudante>> PostEstudante(Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                estudante.DataUltimaEdicao = DateTime.Now; 
                _context.Estudantes.Add(estudante);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEstudante), new { id = estudante.Id }, estudante);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudante(int id, Estudante estudante)
        {
            if (id != estudante.Id)
            {
                return BadRequest(); // Retorna 400 Bad Request se o ID não corresponder
            }

            estudante.DataUltimaEdicao = DateTime.Now; 
            _context.Entry(estudante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudanteExists(id))
                {
                    return NotFound(); // Retorna 404 Not Found se o estudante não existir
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna 204 No Content se a atualização for bem-sucedida
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudante(int id)
        {
            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null)
            {
                return NotFound(); // Retorna 404 Not Found se o estudante não for encontrado
            }

            _context.Estudantes.Remove(estudante);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
        }

        private bool EstudanteExists(int id)
        {
            return _context.Estudantes.Any(e => e.Id == id);
        }
    }
}
