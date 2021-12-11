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
    /// <summary>
    /// Web API para gestionar modelos de computadoras
    /// </summary>
    
    [ApiController]
    [Route("[controller]")]

    public class ComputadoraController:Controller
    {
        private DatabaseContext _context;

        public ComputadoraController(DatabaseContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obtiene todas los modelos de computadoras
        /// </summary>
        /// <remarks>
        /// Obtiene todas las computadoras que han sido registradas
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>
        [HttpGet]
        public async Task<ActionResult<List<Computadora>>> GetCompu()
        {
            var computadoras = await _context.Computadoras.ToListAsync();
            return computadoras; 
        }
        
        /// <summary>
        /// Obtiene las computadoras por id
        /// </summary>
        /// <remarks>
        /// Para obtener los datos de las computadoras se debe espedificar el id
        /// </remarks>
        /// <param name ="id">Id (idSComputadora) del objeto</param> 
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>

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

        /// <summary>
        /// Registra la información
        /// </summary>
        /// <remarks>
        /// Se encarga de hacer los registros con la información de cada computadora
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>
        [HttpPost]
        public async Task<ActionResult<Computadora>> PostCompu(Computadora computadora)
        {
            _context.Computadoras.Add(computadora);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCompuByID", new{id=computadora.computadoraID}, computadora);
        }

        /// <summary>
        /// Cambia datos en los registros
        /// </summary>
        /// <remarks>
        /// Permite actualizar la información de los registros ya realizados
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>

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
        
        /// <summary>
        /// Elimina registros 
        /// </summary>
        /// <remarks>
        /// Permite eliminar los registros indicados mediante el id
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>
            
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