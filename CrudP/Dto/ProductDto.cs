namespace CrudP.Dto
{
 
        public class ProductDto
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public List<ProductDescriptionDto> Descriptions { get; set; }
        }
}
