
using Microsoft.EntityFrameworkCore;
using myWebAPI.Models;

namespace myWebAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }

        public DbSet<Computadora> Computadoras { get; set;}
        public DbSet<Marca> Marcas {get; set;}
        
    }
}