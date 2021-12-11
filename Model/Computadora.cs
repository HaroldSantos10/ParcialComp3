using System.ComponentModel.DataAnnotations;


namespace myWebAPI.Models
{
    public class Computadora
    {
        [Key]
        public int computadoraID {get; set;}
        public string modelo {get; set;}
        public string color {get; set;}
        public int precio {get; set;}
        
        public int MarcaId {get; set;}
        //public Marca Marca {get; set;}
    }
}