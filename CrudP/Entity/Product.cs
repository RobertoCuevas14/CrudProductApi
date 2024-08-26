using System.ComponentModel.DataAnnotations.Schema;

namespace CrudP.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Especificar la precisi�n y escala aqu�

        public ICollection<ProductDescription> Descriptions { get; set; }
    }
}