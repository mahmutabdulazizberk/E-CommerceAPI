using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Service.DTOs;

namespace Presentation.Controller;

public class OrderControllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController(IServiceManager manager) : ControllerBase
    {
        [HttpGet]    // GET /api/orders
        public IActionResult GetOrders()
        {
            return Ok(manager.OrderService.GetUserOrders());
        }

        [HttpPost]
        public IActionResult CreateOrder()
        {
            return Ok(manager.OrderService.CreateOrderFromCart());
        }
    }
}