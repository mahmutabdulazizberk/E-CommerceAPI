using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class OrderItemRepository(AppDbContext context):RepositoryBase<Orderitem>(context),IOrderItemRepository
{
    public IQueryable<Orderitem> GetItemsByOrderId(string orderId) =>
        FindByCondition(oi => oi.Orderid == orderId).Include(oi => oi.Product);

    public void CreateOrderItem(Orderitem item) => Create(item);
    public void UpdateOrderItem(Orderitem item) => Update(item);
    public void DeleteOrderItem(Orderitem item) => Delete(item);
}