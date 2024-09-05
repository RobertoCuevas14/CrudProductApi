using System.ComponentModel.DataAnnotations.Schema;

namespace CrudP.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Product { get; set; }

        public  string LastName { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Carnet { get; set; }  // Especificar la precisi�n y escala aqu�

        public ICollection<ProductDescription> Cargo { get; set; }
    }
}