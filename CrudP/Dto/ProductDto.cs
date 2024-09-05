namespace CrudP.Dto
{
 
        public class ProductDto
        {
            public string Name { get; set; }
            public  string LastName { get; set; }
            public decimal Carnet { get; set; }

            public List<ProductDescriptionDto> Cargo { get; set; }

    }
}
