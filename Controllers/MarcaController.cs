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
    
    public class MarcaController:Controller
    {
        private DatabaseContext _context;

        public MarcaController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Marca>>> GetMarca()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return marcas; 
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarcaByID(int id)
        {
            var marcas = await _context.Marcas.FindAsync(id);
            if(marcas==null)
            {
                return NotFound();
            }
            return marcas; 
        }

        [HttpPost]

        public async Task<ActionResult<Marca>> PostMarca(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMarcaByID", new{id=marca.MarcaID}, marca);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Marca>> PutMarcas(int id, Marca marca)
        {
            if(id != marca.MarcaID)
            {
                return BadRequest();
            }
            _context.Entry(marca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!MarcaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetMarcaByID", new{id=marca.MarcaID}, marca);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Marca>> DeleteMarcas(int id)
        {
            var marcas = await _context.Marcas.FindAsync(id);
            if(marcas == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marcas);
            await _context.SaveChangesAsync();

            return marcas;
        }

        private bool MarcaExists(int id)
        {
            return _context.Marcas.Any(m=>m.MarcaID==id);
        }



    }
}