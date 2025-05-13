using System.Security.Claims;
using Entity.Extension;
using Entity.Models;
using Entity.Result;
using Microsoft.AspNetCore.Http;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class OrderManager(IRepositoryManager manager, IHttpContextAccessor httpContextAccessor) : IOrderService
{
    public Result<Order> CreateOrderFromCart()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        var cart = manager.Cart.GetByUserId(userId);
        if (cart == null) throw new ApiException("Cart not found", "CART_NOT_FOUND", 404);

        if (!cart.Cartitems.Any()) throw new ApiException("Cart is empty", "CART_EMPTY", 400);

        var order = new Order()
        {
            Id = Guid.NewGuid().ToString(),
            Userid = userId,
            Totalamount = cart.Cartitems.Sum(b => b.Quantity * b.Product.Price),
            Status = "Pending",
        };
        manager.Order.CreateOrder(order);

        foreach (var item in cart.Cartitems)
        {
            var orderItem = new Orderitem
            {
                Id = Guid.NewGuid().ToString(),
                Orderid = order.Id,
                Productid = item.Productid,
                Quantity = item.Quantity,
                Unitprice = item.Product.Price,
            };
            manager.OrderItem.CreateOrderItem(orderItem);
        }
        foreach (var orderItem in cart.Cartitems.ToList())
        {
            manager.CartItem.DeleteCartItem(orderItem);
        }

        manager.Save();
        return Result<Order>.SuccessResult(order, "Order created successfully");
    }
    public Result<IEnumerable<Order>> GetUserOrders()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId==null) throw new UnauthorizedAccessException();

        var orders = manager.Order
            .GetOrdersByUserId(userId)
            .ToList();
        if (!orders.Any()) throw new ApiException("No orders found for this user", "ORDERS_NOT_FOUND", 404);


        return Result<IEnumerable<Order>>.SuccessResult(orders, "Orders retrieved successfully");
    }
}