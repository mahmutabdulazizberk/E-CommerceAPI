using Entity.Models;

namespace Repository.Contracts;

public interface IOrderRepository:IRepositoryBase<Order>
{
    IQueryable<Order> GetOrdersByUserId(string userId);
    void CreateOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
}