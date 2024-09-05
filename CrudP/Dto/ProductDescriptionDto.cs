namespace CrudP.Dto
{
    public class ProductDescriptionDto
    {
        public string Cargo { get; set; }
        public int ProductId { get; set; } // Para relacionar la descripción con el producto
        
    }
}
