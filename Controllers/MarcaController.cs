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
    /// Web API para gestionar marcas de computadoras
    /// </summary>

    [ApiController]
    [Route("[controller]")]
    
    public class MarcaController:Controller
    {
        private DatabaseContext _context;

        public MarcaController(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las Marcas
        /// </summary>
        /// <remarks>
        /// Obtiene todas las marcas que han sido registradas
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>
        [HttpGet]
        public async Task<ActionResult<List<Marca>>> GetMarca()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return marcas; 
        }
        

        /// <summary>
        /// Obtiene las marcas por id
        /// </summary>
        /// <remarks>
        /// Para obtener los datos de las marcas se debe espedificar el id
        /// </remarks>
        /// <param name ="id">Id (idSMarca) del objeto</param> 
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>

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


        /// <summary>
        /// Registra la información
        /// </summary>
        /// <remarks>
        /// Se encarga de hacer los registros con la información de cada marca
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>
        [HttpPost]

        public async Task<ActionResult<Marca>> PostMarca(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMarcaByID", new{id=marca.MarcaID}, marca);
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

        /// <summary>
        /// Elimina registros 
        /// </summary>
        /// <remarks>
        /// Permite eliminar los registros indicados mediante el id
        /// </remarks>   
        /// <response code= "200">OK. Devuelve el objeto solicitado</response>
        /// <response code= "404">NOT FOUND. No se ha encontrado el objeto solicitado</response>
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