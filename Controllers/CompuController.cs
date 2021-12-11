using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using myWebAPI.Models;

namespace myWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ComputadoraController:Controller
    {
        private DatabaseContext _context;

        public ComputadoraController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Computadora>>> GetCompu()
        {
            var computadoras = await _context.Computadoras.ToListAsync();
            return computadoras; 
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Computadora>> GetCompuByID(int id)
        {
            var computadoras = await _context.Computadoras.FindAsync(id);
            if(computadoras==null)
            {
                return NotFound();
            }
            return computadoras; 
        }

        [HttpPost]
        public async Task<ActionResult<Computadora>> PostCompu(Computadora computadora)
        {
            _context.Computadoras.Add(computadora);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCompuByID", new{id=computadora.computadoraID}, computadora);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Computadora>> PutCompus(int id, Computadora computadora)
        {
            if(id != computadora.computadoraID)
            {
                return BadRequest();
            }
            _context.Entry(computadora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CompuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetMarcaByID", new{id=computadora.computadoraID}, computadora);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Computadora>> DeleteCompus(int id)
        {
            var compus = await _context.Computadoras.FindAsync(id);
            if(compus == null)
            {
                return NotFound();
            }

            _context.Computadoras.Remove(compus);
            await _context.SaveChangesAsync();

            return compus;
        }

        private bool CompuExists(int id)
        {
            return _context.Computadoras.Any(c=>c.computadoraID==id);
        }


    }
}