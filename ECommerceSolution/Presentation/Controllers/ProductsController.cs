using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.DTOs;
namespace Presentation.Controller;

[ApiController]
[Route("api/products")]
public class ProductsController(IServiceManager manager): ControllerBase
{
    [HttpGet]
    public ActionResult GetAllProducts()
    {
        return Ok(manager.ProductService.GetAllProducts());
    }

    [HttpGet("category/{categoryId}")]
    public IActionResult GetProductsByCategory([FromRoute] string categoryId)
    {
        return Ok(manager.ProductService.GetProductsByCategory(categoryId));
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct([FromRoute(Name = "id")] string id)
    {
        return Ok(manager.ProductService.GetOneProduct(id));
    }

    [HttpPost]
    public IActionResult CreateOneProduct([FromBody] ProductDTO productDto)
    {
        return Ok(manager.ProductService.CreateOneProduct(productDto));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOneProduct([FromRoute(Name = "id")] string id, [FromBody] ProductDTO productDto)
    {
        return Ok(manager.ProductService.UpdateOneProduct(id,productDto));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOneProduct([FromRoute(Name = "id")] string id)
    {
        return Ok(manager.ProductService.DeleteOneProduct(id));
    }
}