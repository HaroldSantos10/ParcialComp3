using System.ComponentModel.DataAnnotations;


namespace myWebAPI.Models
{
    public class Marca
    {
        [Key]
        public int MarcaID {get; set;}
        public string name {get; set;}
        public string descripcion {get; set;}
        public string pais {get; set;}
    }


}

