using Microsoft.AspNetCore.Mvc;
using CrudP.Dto;
using CrudP.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProductDescriptionController : ControllerBase
{
    private readonly IProductDescriptionRepository _descriptionRepository;

    public ProductDescriptionController(IProductDescriptionRepository descriptionRepository)
    {
        _descriptionRepository = descriptionRepository;
    }

    [HttpGet("{productId}")]
    public IActionResult GetDescriptionsByProductId(int productId)
    {
        var descriptions = _descriptionRepository.GetDescriptionsByProductId(productId);
        return Ok(descriptions);
    }

    [HttpPost]
    public IActionResult AddDescription([FromBody] ProductDescriptionDto descriptionDto)
    {
        if (descriptionDto == null)
        {
            return BadRequest();
        }

        var description = new ProductDescription
        {
            Cargo = descriptionDto.Cargo,
            ProductId = descriptionDto.ProductId
        };

        _descriptionRepository.AddDescription(description);
        return Ok(description);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDescription(int id, [FromBody] ProductDescriptionDto descriptionDto)
    {
        var existingDescription = _descriptionRepository.GetDescriptionsByProductId(descriptionDto.ProductId).FirstOrDefault(d => d.Id == id);
        if (existingDescription == null)
        {
            return NotFound();
        }

        existingDescription.Cargo = descriptionDto.Cargo;

        _descriptionRepository.UpdateDescription(existingDescription);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDescription(int id)
    {
        _descriptionRepository.DeleteDescription(id);
        return NoContent();
    }
}
