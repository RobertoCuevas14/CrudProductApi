using Microsoft.AspNetCore.Mvc;
using CrudP.Dto;
using CrudP.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _productRepository.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] ProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest();
        }

        var product = new Product
        {
            Name = productDto.Name,
            LastName = productDto.LastName,
            Carnet = productDto.Carnet,
            Cargo = productDto.Cargo?.Select(d => new ProductDescription
            {
                Cargo = d.Cargo,
                ProductId = d.ProductId
            }).ToList()
        };

        _productRepository.AddProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")] 
    public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        var existingProduct = _productRepository.GetProductById(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        // Actualizar las propiedades del producto
        existingProduct.Name = productDto.Name;
        existingProduct.LastName = productDto.LastName;
        existingProduct.Carnet = productDto.Carnet;

        // Manejar las descripciones (Cargo)
        foreach (var descDto in productDto.Cargo)
        {
            var existingDescription = existingProduct.Cargo
                .FirstOrDefault(d => d.ProductId == id); // Usar ProductId para identificar la relación

            if (existingDescription != null)
            {
                // Actualizar descripción existente si `Cargo` tiene un valor
                if (!string.IsNullOrWhiteSpace(descDto.Cargo))
                {
                    existingDescription.Cargo = descDto.Cargo;
                }
            }
            else
            {
                // Agregar nueva descripción si no existe y `Cargo` no es nulo o vacío
                if (!string.IsNullOrWhiteSpace(descDto.Cargo))
                {
                    existingProduct.Cargo.Add(new ProductDescription
                    {
                        Cargo = descDto.Cargo,
                        ProductId = id // Relacionar con el producto
                    });
                }
            }
        }

        // Opcionalmente, eliminar descripciones que ya no están en el DTO
        var descriptionsToRemove = existingProduct.Cargo
            .Where(d => !productDto.Cargo.Any(dto => dto.Cargo == d.Cargo)) // Comparar por `Cargo` si no hay un identificador único
            .ToList();

        foreach (var descToRemove in descriptionsToRemove)
        {
            existingProduct.Cargo.Remove(descToRemove);
        }

        // Actualizar el producto en la base de datos
        _productRepository.UpdateProduct(existingProduct);

        return NoContent();
    }





    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
        return NoContent();
    }
}
