using CrudP.Entities;
using System.Text.Json.Serialization;

namespace CrudP.Entities
{
    public class ProductDescription
    {
            public int Id { get; set; }
            public string Description { get; set; }
            public int ProductId { get; set; } // Clave foránea para el Producto

            [JsonIgnore]  // O puedes usar [SwaggerSchema(ReadOnly = true)] si usas Swashbuckle.AspNetCore.Annotations
            public Product Product { get; set; }
        }

    }




