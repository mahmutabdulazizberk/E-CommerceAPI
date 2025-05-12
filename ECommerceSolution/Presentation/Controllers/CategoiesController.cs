using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.DTOs;
namespace Presentation.Controller;

[ApiController]
[Route("api/categories")]
public class CategoriesController(IServiceManager manager) : ControllerBase
{
    [HttpGet]
    public ActionResult GetAllCategories()
    {
        return Ok(manager.CategoryService.GetAllCategories());
    }

    [HttpGet("{id}")]
    public IActionResult GetOneCategory([FromRoute(Name = "id")] string id)
    {
        return Ok(manager.CategoryService.GetOneCategory(id));
    }

    [HttpPost]
    public IActionResult CreateOneCategory([FromBody] CategoryDTO categoryDto)
    {
        return Ok(manager.CategoryService.CreateOneCategory(categoryDto));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOneCategory([FromRoute(Name = "id")] string id, [FromBody] CategoryDTO categoryDTO)
    {
        return Ok(manager.CategoryService.UpdateOneCategory(id, categoryDTO));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOneCategory([FromRoute(Name = "id")] string id)
    {
        return Ok(manager.CategoryService.DeleteOneCategory(id));
    }
}
