using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.DTOs;

namespace Presentation.Controller;

[ApiController]
[Route("api/carts")]
[Authorize]
public class CartsController(IServiceManager manager) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCart()
    {
        return Ok(manager.CartService.GetUserCart());
    }

    [HttpPost("items")]
    public IActionResult AddItemToCart([FromBody] CartItemDTO cartItemDto)
    {
        return Ok(manager.CartService.AddItemToCart(cartItemDto));
    }

    [HttpPut("items/{itemId}")]
    public IActionResult ChangeQuantity( [FromRoute] string itemId, [FromBody] CartItemQuantityDTO cartItemQuantityDto)
    {
        return Ok(manager.CartService.UpdateCartItemQuantity(itemId, cartItemQuantityDto));
    }


    [HttpDelete("items/{itemId}")]
    public IActionResult RemoveItem([FromRoute] string itemId)
    {
        return Ok(manager.CartService.DeleteCartItem(itemId));
    }
}