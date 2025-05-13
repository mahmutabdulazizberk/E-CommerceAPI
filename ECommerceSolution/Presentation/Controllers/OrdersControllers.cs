using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Service.DTOs;

namespace Presentation.Controller;


[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController(IServiceManager manager) : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(manager.OrderService.GetUserOrders());
    }
    [HttpGet ("{orderId}")]
    public IActionResult GetOrder([FromRoute(Name="orderId")] string orderId)
    {
        return Ok(manager.OrderService.GetUserOrder(orderId));
    }

    [HttpPost]
    public IActionResult CreateOrder()
    {
        return Ok(manager.OrderService.CreateOrderFromCart());
    }
}
