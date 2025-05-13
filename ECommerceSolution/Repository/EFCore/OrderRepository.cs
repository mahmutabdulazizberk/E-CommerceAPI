using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class OrderRepository(AppDbContext context):RepositoryBase<Order>(context),IOrderRepository
{
    public IQueryable<Order> GetOrdersByUserId(string userId) => FindByCondition(o => o.Userid == userId)
        .Include(o => o.Orderitems)
        .OrderByDescending(o => o.Createdat);

    public Order ByOrderId(string userId, string orderId) => context.Orders.FirstOrDefault(o => o.Userid == userId && o.Id == orderId);
    public void CreateOrder(Order order) => Create(order);
    public void UpdateOrder(Order order) => Update(order);
    public void DeleteOrder(Order order) => Delete(order);
}