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
            Price = productDto.Price,
            Descriptions = productDto.Descriptions?.Select(d => new ProductDescription
            {
                Description = d.Description,
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
        existingProduct.Price = productDto.Price;

        // Manejar las descripciones
        foreach (var descDto in productDto.Descriptions)
        {
            var existingDescription = existingProduct.Descriptions
                .FirstOrDefault(d => d.Id == descDto.ProductId);

            if (existingDescription != null)
            {
                // Si la descripción ya existe, actualízala
                existingDescription.Description = descDto.Description;
            }
            else
            {
                // Si no existe, agrega una nueva descripción
                existingProduct.Descriptions.Add(new ProductDescription
                {
                    Description = descDto.Description,
                    ProductId = id
                });
            }
        }

        // Opcionalmente, eliminar descripciones que ya no están en el DTO
        var descriptionsToRemove = existingProduct.Descriptions
            .Where(d => !productDto.Descriptions.Any(dto => dto.ProductId == d.Id))
            .ToList();

        foreach (var descToRemove in descriptionsToRemove)
        {
            existingProduct.Descriptions.Remove(descToRemove);
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
