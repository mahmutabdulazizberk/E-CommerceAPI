using Entity.Models;
using Entity.Result;

namespace Service.Contracts;

public interface IOrderService
{
    Result<Order> CreateOrderFromCart();
    Result<IEnumerable<Order>> GetUserOrders();
    Result<Order> GetUserOrder(string orderId);
}