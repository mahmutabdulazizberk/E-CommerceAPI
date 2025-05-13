using Entity.Models;

namespace Repository.Contracts;

public interface IOrderItemRepository: IRepositoryBase<Orderitem>
{
    IQueryable<Orderitem> GetItemsByOrderId(string orderId);
    void CreateOrderItem(Orderitem item);
    void UpdateOrderItem(Orderitem item);
    void DeleteOrderItem(Orderitem item);
}